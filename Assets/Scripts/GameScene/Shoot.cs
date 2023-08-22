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
    public int ownerID;
    [SerializeField] GameObject bullet_offline_prefab; 

    void Awake()
    {
        shootSound = GameObject.Find("ShotSound").GetComponent<AudioSource>(); //シーンにあるオブジェクトを探し、コンポーネントを取得
        //bullet_offline_prefab = GameObject.Find("Bullet_offline");

        //shotType = 3;
    }

    void Start()
    {
        ownerID = photonView.Owner.ActorNumber;
        Debug.Log("shoot owner ID = " + ownerID);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.GetComponent<PlayerStatus>().shotLv_circle += 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.GetComponent<PlayerStatus>().shotLv_voltex += 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.GetComponent<PlayerStatus>().shotLv_random += 1;
        }

        // 前フレームからの時間の差を加算
        timeCount_voltex += Time.deltaTime;
        timeCount_circle += Time.deltaTime;
        timeCount_random += Time.deltaTime;

            //Debug.Log("shotLv_voltex=" + transform.GetComponent<PlayerStatus>().shotLv_voltex);
            //Debug.Log("shotLv_circle=" + transform.GetComponent<PlayerStatus>().shotLv_circle);
            //Debug.Log("shotLv_random=" + transform.GetComponent<PlayerStatus>().shotLv_random);

            //円形
            if (transform.GetComponent<PlayerStatus>().shotLv_circle > 0)
            {

                switch (transform.GetComponent<PlayerStatus>().shotLv_circle)
                {
                    case 1:
                        //4way cross
                        if (timeCount_circle > 0.5f)
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
                        if (timeCount_circle > 0.5f)
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
                        if (timeCount_circle > 0.5f)
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
                        if (timeCount_circle > 0.5f)
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
                        if (timeCount_circle > 0.5f)
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

                switch (transform.GetComponent<PlayerStatus>().shotLv_voltex)
                {
                    case 1:
                        //1way uzumaki
                        if (timeCount_voltex > 0.2f)
                        {
                            timeCount_voltex = 0;   
                            shotAngle += 20;
                            InstBullet(shotAngle, transform.position, 6);
                        }
                        break;

                    case 2:
                        //1way uzumaki
                        if (timeCount_voltex > 0.2f)
                        {
                            timeCount_voltex = 0; 
                            shotAngle += 20;
                            InstBullet(shotAngle, transform.position, 6);
                            InstBullet(shotAngle + 180, transform.position, 6);
                        }
                        break;

                    case 3:
                        //1way uzumaki
                        if (timeCount_voltex > 0.2f)
                        {
                            timeCount_voltex = 0;
                            shotAngle += 20;
                            InstBullet(shotAngle, transform.position, 6);
                            InstBullet(shotAngle + 120, transform.position, 6);
                            InstBullet(shotAngle + 240, transform.position, 6);
                        }
                        break;

                    case 4:
                        //1way uzumaki
                        if (timeCount_voltex > 0.2f)
                        {
                            timeCount_voltex = 0; 
                            shotAngle += 20;
                            InstBullet(shotAngle, transform.position, 6);
                            InstBullet(shotAngle + 90, transform.position, 6);
                            InstBullet(shotAngle + 180, transform.position, 6);
                            InstBullet(shotAngle + 270, transform.position, 6);
                        }
                        break;

                    default:
                        //1way uzumaki
                        if (timeCount_voltex > 0.2f)
                        {
                            timeCount_voltex = 0; 
                            shotAngle += 20;
                            InstBullet(shotAngle, transform.position, 6);
                            InstBullet(shotAngle + 90, transform.position, 6);
                            InstBullet(shotAngle + 180, transform.position, 6);
                            InstBullet(shotAngle + 270, transform.position, 6);
                        }
                        break;

                }
            }

            //ランダム
            if (transform.GetComponent<PlayerStatus>().shotLv_random > 0)
            {

                switch (transform.GetComponent<PlayerStatus>().shotLv_random)
                {
                    case 1:
                        //random
                        if (timeCount_random > 0.5f)
                        {
                            timeCount_random = 0; 
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                        }
                        break;

                    case 2:
                        if (timeCount_random > 0.5f)
                        {
                            timeCount_random = 0;
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                        }
                        break;

                    case 3:
                        if (timeCount_random > 0.5f)
                        {
                            timeCount_random = 0;
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                        }
                        break;

                    case 4:
                        if (timeCount_random > 0.5f)
                        {
                            timeCount_random = 0;
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                        }
                        break;

                    default:
                        if (timeCount_random > 0.5f)
                        {
                            timeCount_random = 0; 
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                        }
                        break;


                }
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

        GameObject createObject = Instantiate(bullet_offline_prefab, offsetPos, Quaternion.identity);
        // 生成したGameObjectに設定されている、Bulletスクリプトを取得する
        Bullet bulletScript = createObject.GetComponent<Bullet>();
        // BulletスクリプトのInitを呼び出す
        bulletScript.Init(shotAngle, shotSpeed, ownerID);

        if (this.gameObject.GetComponent<PhotonView>().IsMine)
        {
            //自分の攻撃の色を変える
            createObject.GetComponent<SpriteRenderer>().color = new Color(107, 162, 255);
            ShootEffect();
        }
    }

    public void ShootEffect()
    {
        shootSound.Play(); //銃声を再生
    }

}