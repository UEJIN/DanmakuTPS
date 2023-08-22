using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviourPunCallbacks
{ 
    [SerializeField] float angle; // 角度
    [SerializeField] float speed; // 速度
    Vector3 velocity; // 移動量
    [SerializeField] int ownerID;

    
    //いずれ弾に所有者の情報を持たせて同期は切りたい（プレイヤーのみ同期する。）

    void Start()
    {
        // X方向の移動量を設定する
        velocity.x = speed * Mathf.Cos(angle * Mathf.Deg2Rad);

        // Y方向の移動量を設定する
        velocity.y = speed * Mathf.Sin(angle * Mathf.Deg2Rad);

        // 弾の向きを設定する
        float zAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(0, 0, zAngle);

        // 5秒後に削除
        Destroy(gameObject, 5.0f);

    }
    void Update()
    {
        // 毎フレーム、弾を移動させる
        transform.position += velocity * Time.deltaTime;
    }

    // !!追加!!
    // 角度と速度を設定する関数
    public void Init(float input_angle, float input_speed, int input_ownerID)
    {
        angle = input_angle;
        speed = input_speed;
        ownerID = input_ownerID;
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if (PhotonNetwork.LocalPlayer.ActorNumber != ownerID)　//相手の球が
        {
            if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PhotonView>().IsMine)
            //衝突したオブジェクトにPlayerタグ付けがあり、なおかつそれが自分のプレイヤーの場合
            {
                //RPCを介して、自分のオブジェクトのメソッドを呼ぶ（10fというfloat値を渡す）
                //other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 10f, ownerID);
                other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 10f, ownerID);

                //もし

                Debug.Log("被弾の所有者："+ ownerID);

                //this.gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer); //受けた側で削除するには所有権を貰う必要がある
                //Destroy(this.gameObject);

            }
        }


        //if (!(this.gameObject.GetComponent<PhotonView>().IsMine && other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PhotonView>().IsMine))//自分に自分の玉が当たった場合
        //{
        //    Destroy(this.gameObject);
        //}

    }

    //// IPunOwnershipCallbacks.OnOwnershipTransferedを実装
    //void IPunOwnershipCallbacks.OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    //{
    //    // ネットワークオブジェクトを削除
    //    if (targetView.IsMine)
    //    {
    //        PhotonNetwork.Destroy(targetView.gameObject);
    //        Debug.Log("権限移譲");
    //    }
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