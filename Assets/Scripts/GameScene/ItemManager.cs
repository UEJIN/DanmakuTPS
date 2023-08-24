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
    int itemID;

    ExitGames.Client.Photon.Hashtable roomHash;
    private void Awake()
    {
        int itemID = (PhotonNetwork.CurrentRoom.CustomProperties[this.gameObject.GetComponent<PhotonView>().ViewID.ToString()] is int value) ? value : 0;
        itemRenderer.sprite = sprites[itemID];
    }

    void Start()
    {
        itemRd2d = GetComponent<Rigidbody2D>(); //
        itemRenderer = GetComponent<SpriteRenderer>();  //変数データを取得
        //int itemID = (PhotonNetwork.CurrentRoom.CustomProperties[this.gameObject.GetComponent<PhotonView>().ViewID.ToString()] is int value) ? value : 0;
        //Init(itemID);

        



        //int num = Random.Range(0, sprites.Length);
        //itemRenderer.sprite = sprites[Random.Range(0, sprites.Length)];           //0番目の画像を指定

        //int item = Random.Range(0, 100);                //Itemを乱数で決定
        //if (item < 30)                                  //乱数が30未満なら
        //{
        //    itemRenderer.sprite = sprites[0];           //0番目の画像を指定
        //}
        //else if (item < 50)
        //{
        //    itemRenderer.sprite = sprites[1];
        //}
        //else if (item < 70)
        //{
        //    itemRenderer.sprite = sprites[2];
        //}
        //else if (item < 85)
        //{
        //    itemRenderer.sprite = sprites[3];
        //}
        //else if (item < 95)
        //{
        //    itemRenderer.sprite = sprites[4];
        //}
        //else
        //{
        //    itemRenderer.sprite = sprites[5];
        //}
    }

    private void FixedUpdate()
    {
        //if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(this.gameObject.GetComponent<PhotonView>().ViewID.ToString(), out object setObject))
        //{
        //    itemID = (int)setObject;
        //}

        //Debug.Log("start item id =" + itemID);
    }


    public void Init(int input_itemID)
    {
        itemRenderer.sprite = sprites[input_itemID];
        Debug.Log("ViewID:"+this.gameObject.GetComponent<PhotonView>().ViewID +"itemID:"+ input_itemID);
        
        //ルームプロパティ設定
        roomHash = new ExitGames.Client.Photon.Hashtable();
        roomHash.Add(this.gameObject.GetComponent<PhotonView>().ViewID.ToString(), input_itemID);
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("other：" + other);

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
