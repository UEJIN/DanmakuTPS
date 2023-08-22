using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerStatus : MonoBehaviourPunCallbacks //, IPunOwnershipCallbacks
{
    public int shotLv_voltex;
    public int shotLv_circle;
    public int shotLv_random;
    AudioSource itemGetSound; //AudioSourceを宣言
    public float nowHP;
    public float maxHP = 100;

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

        if (collision.tag == "Item")  //"Item"タグを持っていたら
        {

                string name = collision.GetComponent<SpriteRenderer>().sprite.name; //Itemの名前を取得


                if (name == collision.GetComponent<ItemManager>().sprites[0].name)
                {
                    this.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, -5f, PhotonNetwork.LocalPlayer.ActorNumber); //自分HP5回復
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

            if (photonView.IsMine) //このオブジェクトが自分ならば
            {
                itemGetSound.Play();
                //collision.gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer); //受けた側で削除するには所有権を貰う必要がある

            }

            Destroy(collision.gameObject);      //Item削除

        }
    }

    //// IPunOwnershipCallbacks.OnOwnershipTransferedを実装
    //void IPunOwnershipCallbacks.OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    //{
    //    // ネットワークオブジェクトを削除

    //    PhotonNetwork.Destroy(targetView.gameObject);
    //    Debug.Log("権限移譲");
    //}

    //// 以下のメソッドも実装しないとエラーが出る
    //void IPunOwnershipCallbacks.OnOwnershipTransferFailed(PhotonView targetView, Player previousOwner)
    //{
    //    Debug.Log("権限移譲失敗");
    //}

    //void IPunOwnershipCallbacks.OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    //{

    //}

}
