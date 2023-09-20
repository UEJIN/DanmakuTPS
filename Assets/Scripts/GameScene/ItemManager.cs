using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviourPunCallbacks
{
    Rigidbody2D itemRd2d;     //リジッドボディ型の変数を宣言
    [SerializeField] SpriteRenderer itemRenderer;    //SpriteRenderer型の変数を宣言
    public Sprite[] sprites;        //アイテム画像を入れる配列を宣言
    public Sprite[] ultSprites;        //アイテム画像を入れる配列を宣言

    int itemID;

    ExitGames.Client.Photon.Hashtable roomHash;



    private void Awake()
    {
        
        //ルーム入室時のアイテム状態同期
        //サーバーに保存されてるアイテムIDを取得して同期する
        itemID = (PhotonNetwork.CurrentRoom.CustomProperties[this.gameObject.GetComponent<PhotonView>().ViewID.ToString()] is int value) ? value : 0;
        //IDをもとにスプライトを設定
        if (CompareTag("Item"))
        {
            itemRenderer.sprite = sprites[itemID];
        }
        if (CompareTag("UltItem"))
        {
            itemRenderer.sprite = ultSprites[itemID];
        }
    }

    void Start()
    {
        itemRd2d = GetComponent<Rigidbody2D>(); //
        itemRenderer = GetComponent<SpriteRenderer>();  //変数データを取得

    }

    private void FixedUpdate()
    {

    }


    public void Init(int input_itemID)
    {
        //アイテム召喚時の初期設定

        if (CompareTag("Item"))
        {
            itemRenderer.sprite = sprites[input_itemID];
            Debug.Log("ViewID:" + this.gameObject.GetComponent<PhotonView>().ViewID + "itemID:" + input_itemID);
        }
        if (CompareTag("UltItem"))
        {
            itemRenderer.sprite = ultSprites[input_itemID];
            Debug.Log("ViewID:" + this.gameObject.GetComponent<PhotonView>().ViewID + "ultitemID:" + input_itemID);
        }
       
        //ルームプロパティ設定。アイテムのIDをルームに保存してほかプレイヤーと動悸させる。
        roomHash = new ExitGames.Client.Photon.Hashtable();
        roomHash.Add(this.gameObject.GetComponent<PhotonView>().ViewID.ToString(), input_itemID);
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        //壁に当たると消える
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }

    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        // 更新されたルームのカスタムプロパティのペアをコンソールに出力する
        foreach (var prop in propertiesThatChanged)
        {
            Debug.Log($"{prop.Key}: {prop.Value}");
        }
    }


}
