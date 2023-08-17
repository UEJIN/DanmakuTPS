using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    [SerializeField] float angle; // 角度
    [SerializeField] float speed; // 速度
    Vector3 velocity; // 移動量

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
    public void Init(float input_angle, float input_speed)
    {
        angle = input_angle;
        speed = input_speed;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("衝突" + other.gameObject.name); //衝突したオブジェクトの名前を出力

        //if (other.gameObject.tag == "Tilemap_holl")
        //{
        //    //Destroy(this.gameObject);
        //    gameOverText.SetActive(true);
        //}

        //if (other.gameObject.tag == "Enemy")
        //{
        //    //Destroy(this.gameObject);
        //    gameOverText.SetActive(true);
        //}


        //if (this.gameObject.GetComponent<PhotonView>().IsMine)
        //{
        //    if (other.gameObject.CompareTag("Player") && !other.gameObject.GetComponent<PhotonView>().IsMine)
        //    //衝突したオブジェクトにPlayerタグ付けがあり、なおかつそれが自分のプレイヤーでない場合
        //    {
        //        Destroy(this.gameObject);
        //        other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 10f);
        //        //RPCを介して、相手オブジェクトのメソッドを呼ぶ（10fというfloat値を渡す）

        //    }
        //}
        if (!this.gameObject.GetComponent<PhotonView>().IsMine)　//相手の球が
        {
            if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PhotonView>().IsMine)
            //衝突したオブジェクトにPlayerタグ付けがあり、なおかつそれが自分のプレイヤーの場合
            {
                Destroy(this.gameObject);
                other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 10f);
                //RPCを介して、自分のオブジェクトのメソッドを呼ぶ（10fというfloat値を渡す）

            }
        }


    }


}