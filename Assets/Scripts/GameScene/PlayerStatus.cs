using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;


public class PlayerStatus : MonoBehaviourPunCallbacks //, IPunOwnershipCallbacks
{
    public int shotLv_voltex;//サーバーの値が変更されたらここを更新して保持しておく
    public int shotLv_circle;
    public int shotLv_random;
    AudioSource itemGetSound; //AudioSourceを宣言
    public float nowHP;
    public float maxHP;
    [SerializeField] Image hpBar;

    // Start is called before the first frame update
    void Start()
    {
        //サーバーのデータを取得して初期化
        shotLv_voltex = photonView.Owner.GetShotLv_voltex();
        shotLv_circle = photonView.Owner.GetShotLv_circle();
        shotLv_random = photonView.Owner.GetShotLv_random();
        nowHP = photonView.Owner.GetNowHP();
        maxHP = 100f;
        hpBar.fillAmount = nowHP / maxHP;
        itemGetSound = GameObject.Find("ItemGetSound").GetComponent<AudioSource>(); //シーンにあるオブジェクトを探し、コンポーネントを取得

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("shotLv_voltex=" + shotLv_voltex);
        Debug.Log("shotLv_circle=" + shotLv_circle);
        Debug.Log("shotLv_random=" + shotLv_random);
        if (photonView.IsMine) //このオブジェクトが自分ならば
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PhotonNetwork.LocalPlayer.AddScore(10);
                PhotonNetwork.LocalPlayer.AddShotLv_circle(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PhotonNetwork.LocalPlayer.AddScore(10);
                PhotonNetwork.LocalPlayer.AddShotLv_voltex(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PhotonNetwork.LocalPlayer.AddScore(10);
                PhotonNetwork.LocalPlayer.AddShotLv_random(1);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Item")  //"Item"タグを持っていたら
        {
            
            if (photonView.IsMine) //このオブジェクトが自分ならば
            {
                itemGetSound.Play();
                string name = collision.GetComponent<SpriteRenderer>().sprite.name; //Itemの名前を取得

                //アイテム取るたびスコア10アップ
                PhotonNetwork.LocalPlayer.AddScore(10);

                if (name == collision.GetComponent<ItemManager>().sprites[0].name)
                {
                    this.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, -5f, PhotonNetwork.LocalPlayer.ActorNumber); //自分HP5回復
                    Debug.Log("HP 5UP"); //デバッグで確認
                }
                if (name == collision.GetComponent<ItemManager>().sprites[1].name)
                {
                    PhotonNetwork.LocalPlayer.AddShotLv_circle(1);
                }
                if (name == collision.GetComponent<ItemManager>().sprites[2].name)
                {
                    PhotonNetwork.LocalPlayer.AddShotLv_voltex(1);
                }
                if (name == collision.GetComponent<ItemManager>().sprites[3].name)
                {
                    PhotonNetwork.LocalPlayer.AddShotLv_random(1);
                }
            }

            Destroy(collision.gameObject);      //Item削除

        }
    }


    //誰かのカスタムプロパティ（ステータス）が変更されたら呼ばれる
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        // カスタムプロパティが更新されたプレイヤーのプレイヤー名とIDをコンソールに出力する
        Debug.Log($"{targetPlayer.NickName}({targetPlayer.ActorNumber})");

        // 更新されたプレイヤーのカスタムプロパティのペアをコンソールに出力する
        foreach (var prop in changedProps)
        {
            Debug.Log($"{prop.Key}: {prop.Value}");
        }

        //このスクリプトがアタッチされたプレイヤーが更新されたら
        if(targetPlayer == photonView.Owner)
        {


            foreach (var prop in changedProps)
            {
                switch(prop.Key)
                {
                    case "s_v":
                        shotLv_voltex = targetPlayer.GetShotLv_voltex();
                        break;
                    case "s_c":
                        shotLv_circle = targetPlayer.GetShotLv_circle();
                        break;
                    case "s_r":
                        shotLv_random = targetPlayer.GetShotLv_random();
                        break;
                    case "n_h":
                        nowHP = targetPlayer.GetNowHP();
                        hpBar.fillAmount = nowHP / maxHP;
                        break;

                }

            }
        }

    }
}
