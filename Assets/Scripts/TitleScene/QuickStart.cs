using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class QuickStart : MonoBehaviourPunCallbacks
{
    public ButtonState quickStartButton;
    public TMP_InputField playerNameInput;
    public GameObject LoginPanel;
    public GameObject ConnectingPanel;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        LoginPanel.SetActive(true);
        ConnectingPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (quickStartButton.IsDown() == true)
        {
            ConnectToPhotonServer();
        }
    }

    #region Public Methods

    public void ConnectToPhotonServer() //LoginButtonで呼ぶ
    {
        if (!PhotonNetwork.IsConnected) //サーバーに接続していたら
        {
            string playerName = playerNameInput.text;
            if (!string.IsNullOrEmpty(playerName))
            {
                PhotonNetwork.LocalPlayer.NickName = playerName;
                PhotonNetwork.ConnectUsingSettings();
                ConnectingPanel.SetActive(true);
                LoginPanel.SetActive(false);

            }
        }
    }

    public void JoinRandomRoom() //StatButtonで呼ぶ
    {
        PhotonNetwork.JoinRandomRoom();
    }
    #endregion

    #region Photon Callbacks

    public override void OnConnectedToMaster() //ログインしたら呼ばれる
    {
        Debug.Log(PhotonNetwork.NickName + "Connected to Photon server");
        //LobbyPanel.SetActive(true);
        ConnectingPanel.SetActive(false);
        JoinRandomRoom();

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateAndJoinRoom(); //ルームがなければ自ら作って入る
    }

    public override void OnJoinedRoom() //ルームに入ったら呼ばれる
    {
        Debug.Log(PhotonNetwork.NickName + "joined to" + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("GameScene"); //シーンをロード
    }

    #endregion

    #region Private Methods
    void CreateAndJoinRoom()
    {
        //自動で作られるルームの設定
        string roomName = "Room" + Random.Range(0, 10000); //ルーム名
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        //roomOptions.MaxPlayers = 100;　//ルームの定員
        roomOptions.MaxPlayers = 20;　//ルームの定員
        PhotonNetwork.CreateRoom(roomName, roomOptions);
        Debug.Log("create room");
    }
    #endregion

}
