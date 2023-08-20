using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerStatus : MonoBehaviourPunCallbacks
{
    public int shotLv_voltex;
    public int shotLv_circle;
    public int shotLv_random;
    AudioSource itemGetSound; //AudioSourceを宣言

    // Start is called before the first frame update
    void Start()
    {
        shotLv_voltex = 0;
        shotLv_circle = 1;
        shotLv_random = 0;
        itemGetSound = GameObject.Find("ItemGetSound").GetComponent<AudioSource>(); //シーンにあるオブジェクトを探し、コンポーネントを取得
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("shotLv_voltex=" + shotLv_voltex);
        Debug.Log("shotLv_circle=" + shotLv_circle);
        Debug.Log("shotLv_random=" + shotLv_random);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Enemy")   //記述済み
        //{
        //    SubHP(1);               //記述済み			
        //}

        if (photonView.IsMine) //このオブジェクトが自分ならば
        {
            if (collision.tag == "Item")    //"Item"タグを持っていたら
            {
                string name = collision.GetComponent<SpriteRenderer>().sprite.name; //Itemの名前を取得


                if (name == collision.GetComponent<ItemManager>().sprites[0].name)
                {
                    this.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, -5f); //自分HP5回復
                    Debug.Log("HP 5UP"); //デバッグで確認
                }
                if (name == collision.GetComponent<ItemManager>().sprites[1].name)
                {
                    shotLv_circle += 1;
                }
                if (name == collision.GetComponent<ItemManager>().sprites[2].name)
                {
                    shotLv_voltex += 1;
                }
                if (name == collision.GetComponent<ItemManager>().sprites[3].name)
                {
                    shotLv_random += 1;
                }

                //switch (name)    //取得したnameを振り分け
                //{
                //    case "ItemA":               //name がItemA の場合
                //        this.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, -5f); //自分HP5回復
                //        Debug.Log("HP 1UP"); //デバッグで確認
                //        break;
                //    case "ItemB":
                //        Debug.Log("パワーショット");
                //        break;
                //    case "ItemC":
                //        Debug.Log("シールド");
                //        break;
                //    case "ItemD":
                //        Debug.Log("スピードアップ");
                //        break;
                //    case "ItemE":
                //        Debug.Log("全回復");
                //        break;
                //    case "ItemF":
                //        Debug.Log("ザコ一掃");
                //        break;
                //}

                itemGetSound.Play();
                Destroy(collision.gameObject);      //Item削除
            }
        }
    }

}
