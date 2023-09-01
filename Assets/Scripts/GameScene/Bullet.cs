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

    [SerializeField] float spriteAngle; // 画像の初期角度
    [SerializeField] float damage; // 角度
    [SerializeField] float default_speed; // 速度
    [SerializeField] float lifeTime = 5f; // 消えるまでの秒数

    //いずれ弾に所有者の情報を持たせて同期は切りたい（プレイヤーのみ同期する。）

    void Start()
    {
        // X方向の移動量を設定する
        velocity.x = speed * Mathf.Cos(angle * Mathf.Deg2Rad);

        // Y方向の移動量を設定する
        velocity.y = speed * Mathf.Sin(angle * Mathf.Deg2Rad);

        // 弾の向きを設定する
        float zAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(0, 0, zAngle+ spriteAngle);

        // 5秒後に削除
        Destroy(gameObject, lifeTime);

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
        speed = default_speed + input_speed;
        ownerID = input_ownerID;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("other：" + other);

        //壁に当たると消える
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        
        //相手の球が
        if (PhotonNetwork.LocalPlayer.ActorNumber != ownerID)　
        {
            //衝突したオブジェクトにPlayerタグ付けがあり、なおかつそれが自分のプレイヤーの場合
            if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                //RPCを介して、自分のオブジェクトのメソッドを呼ぶ
                other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage, ownerID);

                Debug.Log("被弾の所有者："+ ownerID);

            }
        }

        //NPC以外のたまがNPCに当たると
        if (ownerID != 0 && other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage, ownerID);
            //Destroy(this.gameObject);
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