using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; //Photonサーバーの情報を使用するため
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks //Photon viewやPunを使用するため
{
    [SerializeField] GameObject playerPrefab;
    //GameObject itemObj;
    [SerializeField] GameObject itemParent;
    [SerializeField] TextMeshProUGUI timerText;


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

        if (PhotonNetwork.IsMasterClient)
        {
            if (itemParent.transform.childCount < 5)
            {
                GameObject itemObj = PhotonNetwork.Instantiate("Item", new Vector2(Random.Range(-20, 20), Random.Range(-20, 20)), Quaternion.identity);
                ItemManager itemManager = itemObj.GetComponent<ItemManager>();
                itemManager.Init(Random.Range(0, itemManager.sprites.Length));
                itemObj.transform.parent = itemParent.transform;
            }
        }
    }
}