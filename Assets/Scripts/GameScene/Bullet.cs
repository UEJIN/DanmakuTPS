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

    [SerializeField] bool isBeam = false; // 消えるまでの秒数
    [SerializeField] bool isRotate = false; // 消えるまでの秒数
    [SerializeField] float rotateSpeed = 3f; // 消えるまでの秒数

    [SerializeField, Tooltip("追従")]
    private bool isFollow = false;
     
    [SerializeField, Tooltip("ターゲットオブジェクト")]
    private GameObject TargetObject;

    [SerializeField, Tooltip("初期位置")]
    private Vector3 initialPosition;

    [SerializeField, Tooltip("回転軸")]
    private Vector3 RotateAxis;

    [SerializeField, Tooltip("速度係数")]
    private float SpeedFactor = 0.1f;

    float nowLifeTime;

    //いずれ弾に所有者の情報を持たせて同期は切りたい（プレイヤーのみ同期する。）

    void Start()
    {

        SetDirection();

        nowLifeTime = lifeTime;
        initialPosition = this.transform.position;

        // lifetime秒後に削除
        Destroy(gameObject, lifeTime);

    }
    void Update()
    {
        nowLifeTime -= Time.deltaTime;

        // 毎フレーム、弾を移動させる
        transform.position += velocity * Time.deltaTime;

        //ビーム（伸びる）
        if(isBeam)
        {

            if (nowLifeTime > lifeTime/2)
            {
                GetComponent<SpriteRenderer>().size += new Vector2(0, speed * Time.deltaTime * 10f);

            }
            else if(GetComponent<SpriteRenderer>().size.x>0)
            {
                GetComponent<SpriteRenderer>().size -= new Vector2(speed * Time.deltaTime * 1f, 0);
            }

        }

        //回転
        if(isRotate)
        {
            Rotation();
        }

        //追従
        if(isFollow && ownerID!=0 && PhotonView.Find(ownerID * 1000 + 1) != null) //NPCの場合は無効
        {
            GameObject owner = PhotonView.Find(ownerID * 1000 + 1).gameObject;
            transform.parent = owner.transform;
        }

    }

    //発射方向と画像の表示角度の初期設定
    public void SetDirection()
    {
        // X方向の移動量を設定する
        velocity.x = speed * Mathf.Cos(angle * Mathf.Deg2Rad);

        // Y方向の移動量を設定する
        velocity.y = speed * Mathf.Sin(angle * Mathf.Deg2Rad);

        // 弾の向きを設定する
        float zAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(0, 0, zAngle + spriteAngle);
    }

    //初期位置を中心に回転する。
    public void Rotation()
    {
        RotateAxis = new Vector3(0, 0, 1);

        // 指定オブジェクトを中心に回転する
        this.transform.RotateAround(
            initialPosition,
            RotateAxis,
            360.0f / (1.0f / SpeedFactor) * Time.deltaTime
            );

        //オブジェクトの向きを追従させる
        this.transform.Rotate(0, 0, -(360.0f / (1.0f / SpeedFactor) * Time.deltaTime)/2);

    }

  
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
        if (other.gameObject.CompareTag("Wall") && !isBeam)
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

}