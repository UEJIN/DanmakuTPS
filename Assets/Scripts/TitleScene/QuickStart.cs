using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class QuickStart : MonoBehaviourPunCallbacks
{
    public ButtonState quickStartButton;
    public TMP_InputField playerNameInput;
    public GameObject LoginPanel;
    public GameObject ConnectingPanel;
    //[SerializeField] GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        LoginPanel.SetActive(true);
        ConnectingPanel.SetActive(false);
        //SceneManager.sceneLoaded += OnLoadedScene;
        //DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (quickStartButton.IsUp() == true)
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
        //PhotonNetwork.LoadLevel("GameScene"); //シーンをロード
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
        //PhotonNetwork.IsMessageQueueRunning = false;
        PhotonNetwork.LoadLevel("GameScene"); //シーンをロード
        //PhotonNetwork.IsMessageQueueRunning = true;

        //if (PhotonNetwork.IsConnected) //サーバーに接続していたら
        //{

        //    if (playerPrefab != null)
        //    {
        //        int randomPoint = Random.Range(-20, 20);
        //        PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(randomPoint, randomPoint), Quaternion.identity); //Photonを介した生成
        //    }
        //}

    }

    //private void OnLoadedScene(Scene i_scene, LoadSceneMode i_mode)
    //{
    //    Debug.Log("OnLoadedScene");


    //    //// シーンの遷移が完了したら自分用のオブジェクトを生成.
    //    //if (i_scene.name == SCENE_NAME)
    //    //{
    //    //    Vector3 pos = Random.insideUnitCircle * m_randomCircle;
    //    //    PhotonNetwork.Instantiate(m_resourcePath, pos, Quaternion.identity, 0);
    //    //}
    //}

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
