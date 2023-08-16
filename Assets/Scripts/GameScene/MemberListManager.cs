using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class MemberListManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI joinedMembersText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateMemberList();

    }

    public void UpdateMemberList()
    {
        joinedMembersText.text = "";
        foreach (var p in PhotonNetwork.PlayerList)
        {
            joinedMembersText.text += p.NickName + "\n";
        }
    }


}
