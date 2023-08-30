using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class TakingDamage : MonoBehaviourPunCallbacks
{
    //[SerializeField] Image hpBar;
    [SerializeField] GameObject deadText; //Inspectorから紐づけ
    [SerializeField] TextMeshProUGUI killText; //Inspectorから紐づけ
    [SerializeField] TextMeshProUGUI respawnText; //Inspectorから紐づけ

    private float hp;
    private float startHp ;
    public bool isInDamageZone = false;
    float timeCount = 0; // 経過時間
    AudioSource dieSound; //AudioSourceを宣言
    AudioSource damageSound;

    // Start is called before the first frame update

    private void Awake()
    {
        dieSound = GameObject.Find("KillSound").GetComponent<AudioSource>(); //シーンにあるオブジェクトを探し、コンポーネントを取得
        damageSound = GameObject.Find("DamageSound").GetComponent<AudioSource>();
    }

    void Start()
    {
        
        //hp = photonView.Owner.GetNowHP();
        //hpBar.fillAmount = hp / startHp;

    }
    public void Update()
    {
        if(isInDamageZone)
        {
            //Debug.Log("Time.time % 10 = " + Time.time % 10);
            // 前フレームからの時間の差を加算
            timeCount += Time.deltaTime;

            // 0.1秒を超えているか
            if (timeCount > 1f)
            {
                timeCount = 0;
                //Debug.Log("重病経過");
                this.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 5f, PhotonNetwork.LocalPlayer.ActorNumber);
            }
        }
    }


    [PunRPC]
    public void TakeDamage(float _damage, int attackerID, PhotonMessageInfo info) //Photonのもつinfoも渡す
    {
        //if (!PhotonNetwork.InRoom) { return; }  //
        Debug.Log("call TakeDamage:damage="+ _damage+", sender="+ info.Sender.NickName);
        
        //現状のHPを取得
        hp = this.gameObject.GetComponent<PlayerStatus>().nowHP;
        startHp = this.gameObject.GetComponent<PlayerStatus>().maxHP;

        if (hp > 0f)
        {

            if (_damage > 0)　　//ダメージ処理
            {
                hp -= _damage;
                //hpBar.fillAmount = hp / startHp;

                if (hp <= 0f)　　//死んだら
                {

                    deadText.SetActive(true);
                    dieSound.Play(); //死亡のお知らせ音を再生

                    if (photonView.IsMine) //自分で読んだら
                    {
                        if (this.gameObject.tag == "Player") //ダメージ食らうのが自プレイヤーなら
                        {
                            PhotonNetwork.LocalPlayer.SetNowHP(0);
                            killText.GetComponent<TextMeshProUGUI>().text = "You were killed by " + info.Sender.NickName; //自分の画面にやられたことを表示
                                                                                                                          //PhotonNetwork.LocalPlayer.AddScore(10);
                        }
                        else  //ダメージ食らうのがNPCなら
                        {
                            this.gameObject.GetComponent<PlayerStatus>().nowHP = 0;
                            this.gameObject.GetComponent<PlayerStatus>().hpBar.fillAmount = this.gameObject.GetComponent<PlayerStatus>().nowHP / this.gameObject.GetComponent<PlayerStatus>().maxHP;
                        }                            
                        
                        if (attackerID != 0) //NPCからの攻撃じゃなかったらスコア処理
                        {
                            Player player = PhotonNetwork.LocalPlayer;
                            player.Get(attackerID).AddScore(50); //攻撃した人にスコア
                            player.Get(attackerID).AddKillCount(1); //攻撃した人にキル+1

                            if(player.Get(attackerID).GetNowHP() + 50 > 100) //HP50足して100超えるなら
                            {
                                player.Get(attackerID).SetNowHP(100); //100にする
                            }
                            else
                            {
                                player.Get(attackerID).AddNowHP(50);　//50回復
                            }
                        }

                        Die();
                    }
                }
                else　//死ななかったら
                {
                    damageSound.Play();

                    if (this.gameObject.tag == "Player")
                    {
                        if (photonView.IsMine) //自分で読んだら
                        {
                            PhotonNetwork.LocalPlayer.SetNowHP(hp);
                        }
                    }
                    else
                    {
                        this.gameObject.GetComponent<PlayerStatus>().nowHP = hp;
                        this.gameObject.GetComponent<PlayerStatus>().hpBar.fillAmount = this.gameObject.GetComponent<PlayerStatus>().nowHP / this.gameObject.GetComponent<PlayerStatus>().maxHP;
                    }
                }
            }
            if(_damage<0)　　//回復処理
            {
                if (hp - _damage > startHp)
                {
                    hp = startHp;       //上限以上は回復しない
                    PhotonNetwork.LocalPlayer.SetNowHP(hp);
                    //hpBar.fillAmount = hp / startHp;
                }
                if(hp - _damage <= startHp)
                {
                    hp -= _damage;
                    PhotonNetwork.LocalPlayer.SetNowHP(hp);
                    //hpBar.fillAmount = hp / startHp;
                }
                
            }

        }
    }

    void Die()
    {
        if (photonView.IsMine)
        {
 
            int i = 0;
            //while (i < PhotonNetwork.LocalPlayer.GetShotLv_voltex()) //0になるまで繰り返す
            while (i < this.gameObject.GetComponent<PlayerStatus>().shotLv_circle) //0になるまで繰り返す
            {
                GameObject itemObj = PhotonNetwork.Instantiate("Item", new Vector2(transform.localPosition.x + CircleHorizon(1f, 2.0f).x, transform.localPosition.y + CircleHorizon(1f, 2.0f).y), Quaternion.identity);
                ItemManager itemManager = itemObj.GetComponent<ItemManager>();
                itemManager.Init(1);

                //itemObj.transform.parent = GameManager.itemParent.transform;
                i++;
            }

            i = 0;
            //while (i < PhotonNetwork.LocalPlayer.GetShotLv_circle()) //0になるまで繰り返す
            while (i < this.gameObject.GetComponent<PlayerStatus>().shotLv_voltex) //0になるまで繰り返す
            {
                GameObject itemObj = PhotonNetwork.Instantiate("Item", new Vector2(transform.localPosition.x + CircleHorizon(1f, 2.0f).x, transform.localPosition.y + CircleHorizon(1f, 2.0f).y), Quaternion.identity);
                ItemManager itemManager = itemObj.GetComponent<ItemManager>();
                itemManager.Init(2);
                //itemObj.transform.parent = itemParent.transform;
                i++;
            }

            i = 0;
            //while (i < PhotonNetwork.LocalPlayer.GetShotLv_random()) //0になるまで繰り返す
            while (i < this.gameObject.GetComponent<PlayerStatus>().shotLv_random) //0になるまで繰り返す
            {
                GameObject itemObj = PhotonNetwork.Instantiate("Item", new Vector2(transform.localPosition.x + CircleHorizon(1f, 2.0f).x, transform.localPosition.y + CircleHorizon(1f, 2.0f).y), Quaternion.identity);
                ItemManager itemManager = itemObj.GetComponent<ItemManager>();
                itemManager.Init(3);
                //itemObj.transform.parent = itemParent.transform;
                i++;
            }
            i = 0;

            if (this.gameObject.tag == "Player")
            {
                StartCoroutine(Respawn());
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator Respawn()
    {
        float respawnTime = 5.0f;
        while (respawnTime > 0F) //0になるまで繰り返す
        {
            yield return new WaitForSeconds(1f);
            respawnTime -= 1.0f;
            transform.GetComponent<MovementController>().enabled = false; //動けない状態
            transform.GetComponent<Shoot>().enabled = false; //撃てない状態
            respawnText.GetComponent<TextMeshProUGUI>().text = "Respawning at: " + respawnTime.ToString();
        }
        killText.GetComponent<TextMeshProUGUI>().text = "";
        respawnText.GetComponent<TextMeshProUGUI>().text = "";
        //int randomPoint = Random.Range(-20, 20);
        transform.position = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), 0); //ランダムな場所へ移動
        transform.GetComponent<MovementController>().enabled = true; //動ける状態
        transform.GetComponent<Shoot>().enabled = true; //撃てる状態
        photonView.RPC("RegainHP", RpcTarget.AllBuffered); //RPCでメソッドを呼ぶ

    }

    [PunRPC]
    public void RegainHP()
    {
        //hp = startHp; //回復
        //hpBar.fillAmount = hp / startHp; //HPBarに反映
        if (photonView.IsMine) //自分で読んだら
        {
            
            
            //ステータスリセット
            PhotonNetwork.LocalPlayer.AddShotLv_voltex(- PhotonNetwork.LocalPlayer.GetShotLv_voltex());
            PhotonNetwork.LocalPlayer.AddShotLv_circle(- PhotonNetwork.LocalPlayer.GetShotLv_circle());
            PhotonNetwork.LocalPlayer.AddShotLv_random(- PhotonNetwork.LocalPlayer.GetShotLv_random());
            PhotonNetwork.LocalPlayer.AddKillCount(-PhotonNetwork.LocalPlayer.GetKillCount());
            PhotonNetwork.LocalPlayer.SetNowHP(startHp);
            PhotonNetwork.LocalPlayer.SetScore(0);
        }
        deadText.SetActive(false); //死亡非表示
    }

    public Vector3 CircleHorizon(float min, float max)
    {

        var angle = Random.Range(0, 360);
        var radius = Random.Range(min, max);
        var rad = angle * Mathf.Deg2Rad;
        var px = Mathf.Cos(rad) * radius;
        var py = Mathf.Sin(rad) * radius;
        return new Vector3(px, py, 0);
    }
}