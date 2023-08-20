using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SafeZoneManager : MonoBehaviour
{

    public float limitTime = 500;



    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            transform.localScale -= new Vector3(0.001f, 0.001f, 0);
        }

    }


    void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PhotonView>().IsMine)
            //衝突したオブジェクトにPlayerタグ付けがあり、なおかつそれが自分のプレイヤーである場合
            {
                Debug.Log("OnTriggerEnter2D : " + other);
            other.gameObject.GetComponent<TakingDamage>().isInDamageZone = false;


                //other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 10f);
                ////RPCを介して、相手オブジェクトのメソッドを呼ぶ（10fというfloat値を渡す）

        }
       
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PhotonView>().IsMine)
        //衝突したオブジェクトにPlayerタグ付けがあり、なおかつそれが自分のプレイヤーである場合
        {
            Debug.Log("OnTriggerExit2D " + other);
            other.gameObject.GetComponent<TakingDamage>().isInDamageZone = true;


            //other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 10f);
            ////RPCを介して、相手オブジェクトのメソッドを呼ぶ（10fというfloat値を渡す）

        }

    }

}
