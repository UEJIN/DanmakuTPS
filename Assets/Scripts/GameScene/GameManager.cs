using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; //Photonサーバーの情報を使用するため

public class GameManager : MonoBehaviourPunCallbacks //Photon viewやPunを使用するため
{
    [SerializeField] GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected) //サーバーに接続していたら
        {
            if (playerPrefab != null)
            {
                int randomPoint = Random.Range(-20, 20);
                PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(randomPoint,randomPoint), Quaternion.identity); //Photonを介した生成
            }
        }
    }
}