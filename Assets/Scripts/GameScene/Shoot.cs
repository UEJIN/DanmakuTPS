using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shoot : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    float timeCount = 0; // 経過時間
    float shotAngle = 0; // 発射角度
    [SerializeField] GameObject shotBullet; // 発射する弾

    public int shotType = 0;



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            shotType = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            shotType = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            shotType = 3;
        }

        // 前フレームからの時間の差を加算
        timeCount += Time.deltaTime;

        // 0.1秒を超えているか
        if (timeCount > 0.1f)
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

            switch(shotType)
            {
                case 1:
                    //4way cross
                    InstBullet(0, transform.position);
                    InstBullet(90, transform.position);
                    InstBullet(180, transform.position);
                    InstBullet(270, transform.position);
                    break;

                case 2:
                    //3way horizontal
                    InstBullet(270, transform.position);
                    InstBullet(270, transform.position + new Vector3(1,0,0));
                    InstBullet(270, transform.position + new Vector3(-1, 0, 0));
                    break;

                case 3:
                    //1way uzumaki
                    shotAngle += 20;
                    InstBullet(shotAngle, transform.position);
                    break;

            }






        }


    }

    private void InstBullet(float shotAngle, Vector3 offsetPos )
    {
        if (this.gameObject.GetComponent<PhotonView>().IsMine)
        {
            GameObject createObject = PhotonNetwork.Instantiate("Bullet", offsetPos, Quaternion.identity);
            // 生成したGameObjectに設定されている、Bulletスクリプトを取得する
            Bullet bulletScript = createObject.GetComponent<Bullet>();
            // BulletスクリプトのInitを呼び出す
            bulletScript.Init(shotAngle, 6);
        }
    }


}