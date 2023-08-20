using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shoot : MonoBehaviourPunCallbacks
{
    [SerializeField] Camera fpsCamera;
    float timeCount = 0; // 経過時間
    float shotAngle = 0; // 発射角度
    [SerializeField] GameObject shotBullet; // 発射する弾
    AudioSource shootSound; //AudioSourceを宣言

    //public int shotType = 0;


    void Awake()
    {
        shootSound = GameObject.Find("ShotSound").GetComponent<AudioSource>(); //シーンにあるオブジェクトを探し、コンポーネントを取得
        //shotType = 3;
    }

    //void Start()
    //{}

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
        timeCount += Time.deltaTime;

        // 0.1秒を超えているか
        if (timeCount > 0.2f)
        {
            timeCount = 0; // 再発射のために時間をリセット

            // GameObjectを新たに生成する
            // 第一引数：生成するGameObject
            // 第二引数：生成する座標
            // 第三引数：生成する角度
            // 戻り値：生成したGameObject
            //if (this.gameObject.GetComponent<PhotonView>().IsMine)
            //{
            //    GameObject createObject = PhotonNetwork.Instantiate("Bullet", transform.position, Quaternion.identity);
            //    // 生成したGameObjectに設定されている、Bulletスクリプトを取得する
            //    Bullet bulletScript = createObject.GetComponent<Bullet>();
            //    // BulletスクリプトのInitを呼び出す
            //    bulletScript.Init(shotAngle, 3);
            //}


            //Debug.Log("shotLv_voltex=" + transform.GetComponent<PlayerStatus>().shotLv_voltex);
            //Debug.Log("shotLv_circle=" + transform.GetComponent<PlayerStatus>().shotLv_circle);
            //Debug.Log("shotLv_random=" + transform.GetComponent<PlayerStatus>().shotLv_random);

            //switch (shotType)
            //{
            //    case 1:
            //        //4way cross
            //        InstBullet(0, transform.position,3);
            //        InstBullet(90, transform.position,3);
            //        InstBullet(180, transform.position,3);
            //        InstBullet(270, transform.position,3);
            //        break;

            //    case 2:
            //        //3way horizontal
            //        InstBullet(270, transform.position,3);
            //        InstBullet(270, transform.position + new Vector3(1,0,0),3);
            //        InstBullet(270, transform.position + new Vector3(-1, 0, 0), 3);
            //        break;

            //    case 3:
            //        //1way uzumaki
            //        shotAngle += 20;
            //        InstBullet(shotAngle, transform.position, 6);
            //        break;

            //}
            if (transform.GetComponent<PlayerStatus>().shotLv_circle > 0)
            {

                switch (transform.GetComponent<PlayerStatus>().shotLv_circle)
                {
                    case 1:
                        //4way cross
                        InstBullet(0, transform.position, 3);
                        InstBullet(90, transform.position, 3);
                        InstBullet(180, transform.position, 3);
                        InstBullet(270, transform.position, 3);
                        break;

                    case 2:
                        //8way cross
                        InstBullet(0, transform.position, 3);
                        InstBullet(90, transform.position, 3);
                        InstBullet(180, transform.position, 3);
                        InstBullet(270, transform.position, 3);
                        InstBullet(45, transform.position, 3);
                        InstBullet(135, transform.position, 3);
                        InstBullet(225, transform.position, 3);
                        InstBullet(315, transform.position, 3);
                        break;

                    case 3:
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
                        break;

                    case 4:
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
                        //8way cross
                        //InstBullet(0, transform.position, 5);
                        //InstBullet(90, transform.position, 5);
                        //InstBullet(180, transform.position, 5);
                        //InstBullet(270, transform.position, 5);
                        //InstBullet(45, transform.position, 5);
                        //InstBullet(135, transform.position, 5);
                        //InstBullet(225, transform.position, 5);
                        //InstBullet(315, transform.position, 5);
                        break;

                }
            }

            if (transform.GetComponent<PlayerStatus>().shotLv_voltex > 0)
            {

                switch (transform.GetComponent<PlayerStatus>().shotLv_voltex)
                {
                    case 1:
                        //1way uzumaki
                        shotAngle += 20;
                        InstBullet(shotAngle, transform.position, 6);
                        break;

                    case 2:
                        //1way uzumaki
                        shotAngle += 20;
                        InstBullet(shotAngle, transform.position, 6);
                        InstBullet(shotAngle + 180, transform.position, 6);
                        break;

                    case 3:
                        //1way uzumaki
                        shotAngle += 20;
                        InstBullet(shotAngle, transform.position, 6);
                        InstBullet(shotAngle + 120, transform.position, 6);
                        InstBullet(shotAngle + 240, transform.position, 6);
                        break;

                    case 4:
                        //1way uzumaki
                        shotAngle += 20;
                        InstBullet(shotAngle, transform.position, 6);
                        InstBullet(shotAngle + 90, transform.position, 6);
                        InstBullet(shotAngle + 180, transform.position, 6);
                        InstBullet(shotAngle + 270, transform.position, 6);
                        break;

                }
            }

            if (transform.GetComponent<PlayerStatus>().shotLv_random > 0)
            {

                switch (transform.GetComponent<PlayerStatus>().shotLv_random)
                {
                    case 1:
                        //random

                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);

                        break;

                    case 2:
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        break;

                    case 3:
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        break;

                    case 4:
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        break;


                }
            }




        }


    }

    private void InstBullet(float shotAngle, Vector3 offsetPos, float shotSpeed )
    {
        if (this.gameObject.GetComponent<PhotonView>().IsMine)
        {
            GameObject createObject = PhotonNetwork.Instantiate("Bullet", offsetPos, Quaternion.identity);
            // 生成したGameObjectに設定されている、Bulletスクリプトを取得する
            Bullet bulletScript = createObject.GetComponent<Bullet>();
            // BulletスクリプトのInitを呼び出す
            bulletScript.Init(shotAngle, shotSpeed);
            //自分の攻撃の色を変える
            createObject.GetComponent<SpriteRenderer>().color = new Color(107, 162, 255);

            photonView.RPC("ShootEffect", RpcTarget.AllBuffered); //全プレイヤーに反映させるため、RPCメソッドに渡す

        }
    }

    [PunRPC] //RPCメソッド
    public void ShootEffect()
    {
        shootSound.Play(); //銃声を再生
    }

}