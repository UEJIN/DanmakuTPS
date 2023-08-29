using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; //Photonサーバーの情報を使用するため
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks //Photon viewやPunを使用するため
{
    [SerializeField] GameObject playerPrefab;
    //GameObject itemObj;
    [SerializeField] public GameObject itemParent;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] public GameObject[] statusObjects;
    [SerializeField] public GameObject killCountObject;
    [SerializeField] public GameObject npcParent;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected) //サーバーに接続していたら
        {

            if (playerPrefab != null)
            {
                int randomPoint = Random.Range(-20, 20);
                PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(randomPoint, randomPoint), Quaternion.identity); //Photonを介した生成
            }
        }
    }


    void FixedUpdate()
    {
        timerText.text = Time.time.ToString("0.0");

        //ホストなら
        if (PhotonNetwork.IsMasterClient)
        {
            //フィールドのアイテムが5こ以下ならランダムスポーン
            if (itemParent.transform.childCount < 5)
            {
                GameObject itemObj = PhotonNetwork.Instantiate("Item", new Vector2(Random.Range(-20, 20), Random.Range(-20, 20)), Quaternion.identity);
                ItemManager itemManager = itemObj.GetComponent<ItemManager>();
                itemManager.Init(Random.Range(0, itemManager.sprites.Length));
                itemObj.transform.parent = itemParent.transform;
            }

            //プレイヤー数が５以下ならNPCスポーン
            if(npcParent.transform.childCount + PhotonNetwork.PlayerList.Length < 10)
            {
                GameObject npc = PhotonNetwork.InstantiateRoomObject(playerPrefab.name, new Vector2(Random.Range(-20, 20), Random.Range(-20, 20)), Quaternion.identity);
                npc.tag = "Enemy";

                PlayerSetUp playerSetUp = npc.GetComponent<PlayerSetUp>();
                playerSetUp.transform.GetComponent<MovementController>().enabled = false;
                playerSetUp.FPSCamera.GetComponent<Camera>().enabled = false;
                playerSetUp.miniMapMarker.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f);   //自機のマーカーを赤にする
                playerSetUp.playerNameText.text = "NPC";

                PlayerStatus playerStatus = npc.GetComponent<PlayerStatus>();
                playerStatus.shotLv_voltex = Random.Range(0,3);
                playerStatus.shotLv_circle = Random.Range(0, 3);
                playerStatus.shotLv_random = Random.Range(0, 3);
                playerStatus.hpBar.fillAmount = playerStatus.nowHP / playerStatus.maxHP;

                npc.transform.parent = npcParent.transform;
            }

        }

        statusObjects[0].GetComponent<TextMeshProUGUI>().text = "SCORE : " + PhotonNetwork.LocalPlayer.GetScore().ToString();
        statusObjects[1].GetComponent<TextMeshProUGUI>().text = "CIRCLE Shot Lv "+PhotonNetwork.LocalPlayer.GetShotLv_circle().ToString();
        statusObjects[2].GetComponent<TextMeshProUGUI>().text = "VOLTEX Shot Lv "+PhotonNetwork.LocalPlayer.GetShotLv_voltex().ToString();
        statusObjects[3].GetComponent<TextMeshProUGUI>().text = "RANDOM Shot Lv "+PhotonNetwork.LocalPlayer.GetShotLv_random().ToString();
        killCountObject.GetComponent<TextMeshProUGUI>().text = PhotonNetwork.LocalPlayer.GetKillCount().ToString() + " Kill";
    }


    //[PunRPC]
    //public void ItemSpawn(int itemID, Vector2 vector2)
    //{
    //    GameObject itemObj = PhotonNetwork.Instantiate("Item", vector2, Quaternion.identity);
    //    ItemManager itemManager = itemObj.GetComponent<ItemManager>();
    //    itemManager.Init(itemID);
    //    itemObj.transform.parent = itemParent.transform;
    //}

}