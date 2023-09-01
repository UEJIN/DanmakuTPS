using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shoot : MonoBehaviourPunCallbacks
{
    [SerializeField] Camera fpsCamera;
    //float timeCount = 0; // 経過時間
    float shotAngle = 0; // 発射角度
    [SerializeField] GameObject shotBullet; // 発射する弾
    AudioSource shootSound; //AudioSourceを宣言
    float timeCount_circle = 0;
    float timeCount_voltex = 0;
    float timeCount_random = 0;
    float timeCount_aim = 0;
    public int ownerID;
    [SerializeField] GameObject bullet_offline_prefab;
    [SerializeField] GameObject bullet_bolt_prefab;
    [SerializeField] GameObject bullet_aim_prefab;
    [SerializeField] float shot_voltex_speed = 2;
    [SerializeField] float shot_aim_speed = 4;

    private GameObject bulletPrefab;
    Vector3 previousPos;
    float angle= -90;

    void Awake()
    {
        shootSound = GameObject.Find("ShotSound").GetComponent<AudioSource>(); //シーンにあるオブジェクトを探し、コンポーネントを取得

    }

    void Start()
    {
        //玉の所有者のIDを設定する
        if (this.gameObject.tag == "Player")
        {
            ownerID = photonView.Owner.ActorNumber;
        }
        else
        {
            ownerID = 0; //NPCなら0にしとく
        }
        Debug.Log("shoot owner ID = " + ownerID);

        previousPos = transform.position;
    }

    private void FixedUpdate()
    {
        //プレイヤーの進行方向の取得
        Vector3 diff = previousPos - transform.position;
        Vector3 axis = Vector3.Cross(transform.up, diff);
        if (diff.magnitude > 0.01f) //移動してないときは角度を取得しない
        {
            angle = Vector3.Angle(transform.up, diff) * (axis.z < 0 ? -1 : 1)  -90;
        }
        else
        {
            angle = Random.Range(0f,180f);
        }
        previousPos = transform.position;

        //Debug.Log("★angle:"+ angle);
        //Debug.Log("★diff:" + diff);
        //Debug.Log("★axis:" + axis);
        //Debug.Log("★diff.magnitude:" + diff.magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        // 前フレームからの時間の差を加算
        timeCount_voltex += Time.deltaTime;
        timeCount_circle += Time.deltaTime;
        timeCount_random += Time.deltaTime;
        timeCount_aim += Time.deltaTime;

        //Debug.Log("shotLv_voltex=" + transform.GetComponent<PlayerStatus>().shotLv_voltex);
        //Debug.Log("shotLv_circle=" + transform.GetComponent<PlayerStatus>().shotLv_circle);
        //Debug.Log("shotLv_random=" + transform.GetComponent<PlayerStatus>().shotLv_random);

        //円形
        if (transform.GetComponent<PlayerStatus>().shotLv_circle > 0)
        {
            bulletPrefab = bullet_offline_prefab;

            switch (transform.GetComponent<PlayerStatus>().shotLv_circle)
            {
                case 1:
                    //4way cross
                    if (timeCount_circle > 1f)
                    {
                        timeCount_circle = 0;
                        InstBullet(0, transform.position, 3);
                        InstBullet(90, transform.position, 3);
                        InstBullet(180, transform.position, 3);
                        InstBullet(270, transform.position, 3);
                    }
                    break;

                case 2:
                    //8way cross
                    if (timeCount_circle > 1f)
                    {
                        timeCount_circle = 0;
                        InstBullet(0, transform.position, 3);
                        InstBullet(90, transform.position, 3);
                        InstBullet(180, transform.position, 3);
                        InstBullet(270, transform.position, 3);
                        InstBullet(45, transform.position, 3);
                        InstBullet(135, transform.position, 3);
                        InstBullet(225, transform.position, 3);
                        InstBullet(315, transform.position, 3);
                    }
                    break;

                case 3:
                    if (timeCount_circle > 1f)
                    {
                        timeCount_circle = 0; 
                        InstBullet(0, transform.position, 3);
                        InstBullet(90, transform.position, 3);
                        InstBullet(180, transform.position, 3);
                        InstBullet(270, transform.position, 3);
                        InstBullet(45, transform.position, 3);
                        InstBullet(135, transform.position, 3);
                        InstBullet(225, transform.position, 3);
                        InstBullet(315, transform.position, 3);
                        InstBullet(67.5f, transform.position, 3);
                        InstBullet(157.5f, transform.position, 3);
                        InstBullet(247.5f, transform.position, 3);
                        InstBullet(337.5f, transform.position, 3);
                    }
                    break;

                case 4:
                    if (timeCount_circle > 1f)
                    {
                        timeCount_circle = 0; 
                        InstBullet(0, transform.position, 3);
                        InstBullet(90, transform.position, 3);
                        InstBullet(180, transform.position, 3);
                        InstBullet(270, transform.position, 3);
                        InstBullet(45, transform.position, 3);
                        InstBullet(135, transform.position, 3);
                        InstBullet(225, transform.position, 3);
                        InstBullet(315, transform.position, 3);
                        InstBullet(67.5f, transform.position, 3);
                        InstBullet(157.5f, transform.position, 3);
                        InstBullet(247.5f, transform.position, 3);
                        InstBullet(337.5f, transform.position, 3);
                        InstBullet(22.5f, transform.position, 3);
                        InstBullet(112.5f, transform.position, 3);
                        InstBullet(202.5f, transform.position, 3);
                        InstBullet(292.5f, transform.position, 3);
                    }
                    break;

                default:
                    if (timeCount_circle > 1f)
                    {
                        timeCount_circle = 0; 
                        InstBullet(0, transform.position, 3);
                        InstBullet(90, transform.position, 3);
                        InstBullet(180, transform.position, 3);
                        InstBullet(270, transform.position, 3);
                        InstBullet(45, transform.position, 3);
                        InstBullet(135, transform.position, 3);
                        InstBullet(225, transform.position, 3);
                        InstBullet(315, transform.position, 3);
                        InstBullet(67.5f, transform.position, 3);
                        InstBullet(157.5f, transform.position, 3);
                        InstBullet(247.5f, transform.position, 3);
                        InstBullet(337.5f, transform.position, 3);
                        InstBullet(22.5f, transform.position, 3);
                        InstBullet(112.5f, transform.position, 3);
                        InstBullet(202.5f, transform.position, 3);
                        InstBullet(292.5f, transform.position, 3);
                    }
                    break;
            }
        }

        //渦巻き
        if (transform.GetComponent<PlayerStatus>().shotLv_voltex > 0)
        {
            //float shot_voltex_speed = 2;
            bulletPrefab = bullet_offline_prefab;

            switch (transform.GetComponent<PlayerStatus>().shotLv_voltex)
            {
                case 1:
                    //1way uzumaki
                    if (timeCount_voltex > 0.2f)
                    {
                        timeCount_voltex = 0;   
                        shotAngle += 20;
                        InstBullet(shotAngle, transform.position, shot_voltex_speed);
                    }
                    break;

                case 2:
                    //1way uzumaki
                    if (timeCount_voltex > 0.2f)
                    {
                        timeCount_voltex = 0; 
                        shotAngle += 20;
                        InstBullet(shotAngle, transform.position, shot_voltex_speed);
                        InstBullet(shotAngle + 180, transform.position, shot_voltex_speed);
                    }
                    break;

                case 3:
                    //1way uzumaki
                    if (timeCount_voltex > 0.2f)
                    {
                        timeCount_voltex = 0;
                        shotAngle += 20;
                        InstBullet(shotAngle, transform.position, shot_voltex_speed);
                        InstBullet(shotAngle + 120, transform.position, shot_voltex_speed);
                        InstBullet(shotAngle + 240, transform.position, shot_voltex_speed);
                    }
                    break;

                case 4:
                    //1way uzumaki
                    if (timeCount_voltex > 0.2f)
                    {
                        timeCount_voltex = 0; 
                        shotAngle += 20;
                        InstBullet(shotAngle, transform.position, shot_voltex_speed);
                        InstBullet(shotAngle + 90, transform.position, shot_voltex_speed);
                        InstBullet(shotAngle + 180, transform.position, shot_voltex_speed);
                        InstBullet(shotAngle + 270, transform.position, shot_voltex_speed);
                    }
                    break;

                default:
                    //1way uzumaki
                    if (timeCount_voltex > 0.2f)
                    {
                        timeCount_voltex = 0; 
                        shotAngle += 20;
                        InstBullet(shotAngle, transform.position, shot_voltex_speed);
                        InstBullet(shotAngle + 90, transform.position, shot_voltex_speed);
                        InstBullet(shotAngle + 180, transform.position, shot_voltex_speed);
                        InstBullet(shotAngle + 270, transform.position, shot_voltex_speed);
                    }
                    break;

            }
        }

        //ランダム
        if (transform.GetComponent<PlayerStatus>().shotLv_random > 0)
        {
            float shot_random_speed = 4;
            bulletPrefab = bullet_bolt_prefab;

            switch (transform.GetComponent<PlayerStatus>().shotLv_random)
            {
                case 1:
                    //random
                    if (timeCount_random > 0.5f)
                    {
                        timeCount_random = 0; 
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                    }
                    break;

                case 2:
                    if (timeCount_random > 0.5f)
                    {
                        timeCount_random = 0;
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                    }
                    break;

                case 3:
                    if (timeCount_random > 0.5f)
                    {
                        timeCount_random = 0;
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                    }
                    break;

                case 4:
                    if (timeCount_random > 0.5f)
                    {
                        timeCount_random = 0;
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                    }
                    break;

                default:
                    if (timeCount_random > 0.5f)
                    {
                        timeCount_random = 0; 
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                        InstBullet(Random.Range(0, 360), transform.position, shot_random_speed);
                    }
                    break;


            }
        }

        //狙い撃ち
        if (transform.GetComponent<PlayerStatus>().shotLv_aim >= 0)
        {
            //float shot_aim_speed = 4;
            bulletPrefab = bullet_aim_prefab;

            if (timeCount_aim > 1f - 0.05f * transform.GetComponent<PlayerStatus>().shotLv_aim)
            {
                timeCount_aim = 0;
                InstBullet(angle, transform.position, shot_aim_speed);
            }


            //switch (transform.GetComponent<PlayerStatus>().shotLv_aim)
            //{

            //    case 0:
            //        //random
            //        if (timeCount_aim > 0.5f)
            //        {
            //            timeCount_aim = 0;
            //            InstBullet(angle, transform.position, shot_aim_speed);
            //        }
            //        break;

            //    default:
            //        //random
            //        if (timeCount_aim > 0.5f)
            //        {
            //            timeCount_aim = 0;
            //            InstBullet(angle, transform.position, shot_aim_speed);
            //        }
            //        break;

            //}
        }

    }

    //private void InstBullet(float shotAngle, Vector3 offsetPos, float shotSpeed )
    //{
    //    if (this.gameObject.GetComponent<PhotonView>().IsMine)
    //    {
    //        GameObject createObject = PhotonNetwork.Instantiate("Bullet", offsetPos, Quaternion.identity);
    //        // 生成したGameObjectに設定されている、Bulletスクリプトを取得する
    //        Bullet bulletScript = createObject.GetComponent<Bullet>();
    //        // BulletスクリプトのInitを呼び出す
    //        bulletScript.Init(shotAngle, shotSpeed, ownerID);
    //        //自分の攻撃の色を変える
    //        createObject.GetComponent<SpriteRenderer>().color = new Color(107, 162, 255);

    //        //photonView.RPC("ShootEffect", RpcTarget.AllBuffered); //全プレイヤーに反映させるため、RPCメソッドに渡す
    //        ShootEffect();
    //    }
    //}

    ////[PunRPC] //RPCメソッド
    //public void ShootEffect()
    //{
    //    shootSound.Play(); //銃声を再生
    //}

    private void InstBullet(float shotAngle, Vector3 offsetPos, float shotSpeed)
    {

        GameObject createObject = Instantiate(bulletPrefab, offsetPos, Quaternion.identity);
        // 生成したGameObjectに設定されている、Bulletスクリプトを取得する
        Bullet bulletScript = createObject.GetComponent<Bullet>();
        // BulletスクリプトのInitを呼び出す
        bulletScript.Init(shotAngle, shotSpeed, ownerID);

        //自プレイヤーの玉なら
        if (this.gameObject.GetComponent<PhotonView>().IsMine && this.gameObject.tag == "Player")
        {
            //自分の攻撃の色を変える
            createObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 120);
            ShootEffect();
        }
    }

    public void ShootEffect()
    {
        shootSound.Play(); //銃声を再生
    }

}