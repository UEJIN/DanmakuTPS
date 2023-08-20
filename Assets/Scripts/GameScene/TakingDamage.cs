using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class TakingDamage : MonoBehaviourPunCallbacks
{
    [SerializeField] Image hpBar;
    [SerializeField] GameObject deadText; //Inspectorから紐づけ
    [SerializeField] TextMeshProUGUI killText; //Inspectorから紐づけ
    [SerializeField] TextMeshProUGUI respawnText; //Inspectorから紐づけ

    private float hp;
    public float startHp = 100;
    public bool isInDamageZone = false;
    float timeCount = 0; // 経過時間
    AudioSource dieSound; //AudioSourceを宣言
    AudioSource damageSound;

    // Start is called before the first frame update
    void Start()
    {
        hp = startHp;
        hpBar.fillAmount = hp / startHp;
        dieSound = GameObject.Find("KillSound").GetComponent<AudioSource>(); //シーンにあるオブジェクトを探し、コンポーネントを取得
        damageSound = GameObject.Find("DamageSound").GetComponent<AudioSource>();
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
                this.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 5f);
            }
        }
    }


    [PunRPC]
    public void TakeDamage(float _damage, PhotonMessageInfo info) //Photonのもつinfoも渡す
    {
        if (hp > 0f)
        {

            if (_damage > 0)　　//ダメージ処理
            {
                hp -= _damage;
                hpBar.fillAmount = hp / startHp;

                damageSound.Play();

                if (hp <= 0f)　　//死んだら
                {
                    deadText.SetActive(true);
                    Die();
                    dieSound.Play(); //死亡のお知らせ音を再生
                    if (photonView.IsMine)
                    {
                        killText.GetComponent<TextMeshProUGUI>().text = "You were killed by " + info.Sender.NickName; //自分の画面にやられたことを表示
                    }
                }
            }
            if(_damage<0)　　//回復処理
            {
                if (hp - _damage > startHp)
                {
                    hp = startHp;       //上限以上は回復しない
                    hpBar.fillAmount = hp / startHp;
                }
                if(hp - _damage <= startHp)
                {
                    hp -= _damage;
                    hpBar.fillAmount = hp / startHp;
                }
                
            }

        }
    }

    void Die()
    {
        if (photonView.IsMine)
        {
           StartCoroutine(Respawn());
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
        int randomPoint = Random.Range(-20, 20);
        transform.position = new Vector3(randomPoint, randomPoint, 0); //ランダムな場所へ移動
        transform.GetComponent<MovementController>().enabled = true; //動ける状態
        transform.GetComponent<Shoot>().enabled = true; //撃てる状態
        photonView.RPC("RegainHP", RpcTarget.AllBuffered); //RPCでメソッドを呼ぶ

    }

    [PunRPC]
    public void RegainHP()
    {
        hp = startHp; //回復
        hpBar.fillAmount = hp / startHp; //HPBarに反映
        deadText.SetActive(false); //死亡非表示
    }
}