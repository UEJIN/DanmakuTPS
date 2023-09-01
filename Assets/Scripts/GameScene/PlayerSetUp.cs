using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetUp : MonoBehaviourPunCallbacks
{

    [SerializeField] public GameObject FPSCamera;
    //[SerializeField] Text playerNameText;
    [SerializeField] public TextMeshProUGUI playerNameText;
    [SerializeField] public GameObject miniMapMarker;
    //AudioSource itemGetSound; //AudioSourceを宣言

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.Owner.IsLocal && playerNameText.text != "NPC") //このオブジェクトが自分ならば
        {
            transform.GetComponent<MovementController>().enabled = true; //MovementController.csを有効にする
            FPSCamera.GetComponent<Camera>().enabled = true; //FPSCameraのCameraコンポーネントを有効にする
            transform.GetComponent<PlayerStatus>().enabled = true; //

            miniMapMarker.GetComponent<SpriteRenderer>().color = new Color(0f, 255f, 0f);   //自機のマーカーを緑にする
            //itemGetSound = GameObject.Find("ItemGetSound").GetComponent<AudioSource>(); //シーンにあるオブジェクトを探し、コンポーネントを取得

        }
        else
        {
            //transform.GetComponent<MovementController>().enabled = false;
            FPSCamera.GetComponent<Camera>().enabled = false;
            //transform.GetComponent<PlayerStatus>().enabled = false; //
        }

        if (playerNameText != null && this.gameObject.tag == "Player") //Textオブジェクトが空でなければ
        {
            playerNameText.text = photonView.Owner.NickName; //ログインした名前を代入
        }
    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //if (collision.tag == "Enemy")   //記述済み
    //    //{
    //    //    SubHP(1);               //記述済み			
    //    //}

    //    if (photonView.IsMine) //このオブジェクトが自分ならば
    //    {
    //        if (collision.tag == "Item")    //"Item"タグを持っていたら
    //        {
    //            string name = collision.GetComponent<SpriteRenderer>().sprite.name; //Itemの名前を取得
    //            switch (name)    //取得したnameを振り分け
    //            {
    //                case "ItemA":               //name がItemA の場合
    //                    this.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, -5f); //自分HP5回復
    //                    Debug.Log("HP 1UP"); //デバッグで確認
    //                    break;           
    //                case "ItemB":
    //                    Debug.Log("パワーショット");
    //                    break;
    //                case "ItemC":
    //                    Debug.Log("シールド");
    //                    break;
    //                case "ItemD":
    //                    Debug.Log("スピードアップ");
    //                    break;
    //                case "ItemE":
    //                    Debug.Log("全回復");
    //                    break;
    //                case "ItemF":
    //                    Debug.Log("ザコ一掃");
    //                    break;
    //            }

    //            itemGetSound.Play();
    //            Destroy(collision.gameObject);      //Item削除
    //        }
    //    }
    //}

}