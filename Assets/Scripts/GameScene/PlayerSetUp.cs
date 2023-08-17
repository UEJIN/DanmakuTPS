using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetUp : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject FPSCamera;
    //[SerializeField] Text playerNameText;
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] GameObject miniMapMarker;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine) //���̃I�u�W�F�N�g��������Photon����Đ����������̂Ȃ��
        {
            transform.GetComponent<MovementController>().enabled = true; //MovementController.cs��L���ɂ���
            FPSCamera.GetComponent<Camera>().enabled = true; //FPSCamera��Camera�R���|�[�l���g��L���ɂ���
            miniMapMarker.GetComponent<SpriteRenderer>().color = new Color(0f, 255f, 0f);   //���@�̃}�[�J�[��΂ɂ���

        }
        else
        {
            transform.GetComponent<MovementController>().enabled = false;
            FPSCamera.GetComponent<Camera>().enabled = false;
        }

        if (playerNameText != null) //Text�I�u�W�F�N�g����łȂ����
        {
            playerNameText.text = photonView.Owner.NickName; //���O�C���������O����
        }
    }
}