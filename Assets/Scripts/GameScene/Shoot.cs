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

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0)) //左クリックした場合
        //{
        //    RaycastHit _hit; //Rayが衝突した情報を宣言
        //    Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f)); //カメラから画面中心に向けて飛ばすRayを定義

        //    if (Physics.Raycast(ray, out _hit, 100)) //Rayが何かオブジェクトに衝突した場合
        //    {
        //        Debug.Log(_hit.collider.gameObject.name); //衝突したオブジェクトの名前を出力

        //        if (_hit.collider.gameObject.CompareTag("Player") && !_hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
        //        //衝突したオブジェクトにPlayerタグ付けがあり、なおかつそれが自分のプレイヤーでない場合
        //        {
        //            //_hit.collider.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 10f); 
        //            //RPCを介して、相手オブジェクトのメソッドを呼ぶ（10fというfloat値を渡す）
        //        }
        //    }
        //}

        // 前フレームからの時間の差を加算
        timeCount += Time.deltaTime;

        // 0.1秒を超えているか
        if (timeCount > 0.1f)
        {
            timeCount = 0; // 再発射のために時間をリセット

            //shotAngle += 20;
            //shotAngle = 0;

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

            if(Time.time%10 >= 5)
            {
                //4way cross
                InstBullet(0, transform.position);
                InstBullet(90, transform.position);
                InstBullet(180, transform.position);
                InstBullet(270, transform.position);
            }
            else
            {
            //3way horizontal
            InstBullet(270, transform.position);
            InstBullet(270, transform.position + new Vector3(1,0,0));
            InstBullet(270, transform.position + new Vector3(-1, 0, 0));

            }




        }


    }

    private void InstBullet(int shotAngle, Vector3 offsetPos )
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