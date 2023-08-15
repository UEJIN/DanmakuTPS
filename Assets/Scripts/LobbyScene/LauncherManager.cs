using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LauncherManager : MonoBehaviourPunCallbacks
{
    public GameObject LoginPanel;
    public TMP_InputField playerNameInput;

    public GameObject ConnectingPanel;
    public GameObject LobbyPanel;

    #region Unity Methods

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        LoginPanel.SetActive(true);
        ConnectingPanel.SetActive(false);
        LobbyPanel.SetActive(false);
    }
    #endregion

    #region Public Methods

    public void ConnectToPhotonServer() //LoginButton�ŌĂ�
    {
        if (!PhotonNetwork.IsConnected) //�T�[�o�[�ɐڑ����Ă�����
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
        else { }
    }

    public void JoinRandomRoom() //StatButton�ŌĂ�
    {
        PhotonNetwork.JoinRandomRoom();
    }
    #endregion


    #region Photon Callbacks

    public override void OnConnectedToMaster() //���O�C��������Ă΂��
    {
        Debug.Log(PhotonNetwork.NickName + "Connected to Photon server");
        LobbyPanel.SetActive(true);
        ConnectingPanel.SetActive(false);

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateAndJoinRoom(); //���[�����Ȃ���Ύ������ē���
    }

    public override void OnJoinedRoom() //���[���ɓ�������Ă΂��
    {
        Debug.Log(PhotonNetwork.NickName + "joined to" + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("GameScene"); //�V�[�������[�h
    }

    #endregion

    #region Private Methods
    void CreateAndJoinRoom()
    {
        //�����ō���郋�[���̐ݒ�
        string roomName = "Room" + Random.Range(0, 10000); //���[����
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        //roomOptions.MaxPlayers = 100;�@//���[���̒��
        roomOptions.MaxPlayers = 20;�@//���[���̒��
        PhotonNetwork.CreateRoom(roomName, roomOptions);
        Debug.Log("create room");
    }
    #endregion
}