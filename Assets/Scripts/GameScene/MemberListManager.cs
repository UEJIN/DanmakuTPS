using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class MemberListManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI joinedMembersText;


    //// <summary>
    //// �����[�g�v���C���[�����������ۂɃR�[�������
    //// </summary>
    //public void OnPhotonPlayerConnected(PhotonPlayer player)
    //{
    //    Debug.Log(player.name + " is joined.");
    //    UpdateMemberList();
    //}

    //// <summary>
    //// �����[�g�v���C���[���ގ������ۂɃR�[�������
    //// </summary>
    //public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    //{
    //    Debug.Log(player.name + " is left.");
    //    UpdateMemberList();
    //}

    //public void UpdateMemberList()
    //{
    //    joinedMembersText.text = "";
    //    foreach (var p in PhotonNetwork.playerList)
    //    {
    //        joinedMembersText.text += p.name + "\n";
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateMemberList();

    }

    //public void UpdateMemberList()
    //{
    //    joinedMembersText.text = "";
    //    foreach (var p in PhotonNetwork.playerList)
    //    {
    //        joinedMembersText.text += p.name + "\n";
    //    }
    //}


}
