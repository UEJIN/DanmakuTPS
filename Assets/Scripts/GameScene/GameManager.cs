using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; //Photon�T�[�o�[�̏����g�p���邽��

public class GameManager : MonoBehaviourPunCallbacks //Photon view��Pun���g�p���邽��
{
    [SerializeField] GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected) //�T�[�o�[�ɐڑ����Ă�����
        {
            if (playerPrefab != null)
            {
                int randomPoint = Random.Range(-20, 20);
                PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(randomPoint,randomPoint), Quaternion.identity); //Photon���������
            }
        }
    }
}