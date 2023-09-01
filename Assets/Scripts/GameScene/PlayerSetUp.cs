using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetUp : MonoBehaviourPunCallbacks
{

    [SerializeField] public GameObject FPSCamera;
    //[SerializeField] Text playerNameText;
    [SerializeField] public TextMeshProUGUI playerNameText;
    [SerializeField] public GameObject miniMapMarker;
    //AudioSource itemGetSound; //AudioSource��錾

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.Owner.IsLocal && playerNameText.text != "NPC") //���̃I�u�W�F�N�g�������Ȃ��
        {
            transform.GetComponent<MovementController>().enabled = true; //MovementController.cs��L���ɂ���
            FPSCamera.GetComponent<Camera>().enabled = true; //FPSCamera��Camera�R���|�[�l���g��L���ɂ���
            transform.GetComponent<PlayerStatus>().enabled = true; //

            miniMapMarker.GetComponent<SpriteRenderer>().color = new Color(0f, 255f, 0f);   //���@�̃}�[�J�[��΂ɂ���
            //itemGetSound = GameObject.Find("ItemGetSound").GetComponent<AudioSource>(); //�V�[���ɂ���I�u�W�F�N�g��T���A�R���|�[�l���g���擾

        }
        else
        {
            //transform.GetComponent<MovementController>().enabled = false;
            FPSCamera.GetComponent<Camera>().enabled = false;
            //transform.GetComponent<PlayerStatus>().enabled = false; //
        }

        if (playerNameText != null && this.gameObject.tag == "Player") //Text�I�u�W�F�N�g����łȂ����
        {
            playerNameText.text = photonView.Owner.NickName; //���O�C���������O����
        }
    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //if (collision.tag == "Enemy")   //�L�q�ς�
    //    //{
    //    //    SubHP(1);               //�L�q�ς�			
    //    //}

    //    if (photonView.IsMine) //���̃I�u�W�F�N�g�������Ȃ��
    //    {
    //        if (collision.tag == "Item")    //"Item"�^�O�������Ă�����
    //        {
    //            string name = collision.GetComponent<SpriteRenderer>().sprite.name; //Item�̖��O���擾
    //            switch (name)    //�擾����name��U�蕪��
    //            {
    //                case "ItemA":               //name ��ItemA �̏ꍇ
    //                    this.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, -5f); //����HP5��
    //                    Debug.Log("HP 1UP"); //�f�o�b�O�Ŋm�F
    //                    break;           
    //                case "ItemB":
    //                    Debug.Log("�p���[�V���b�g");
    //                    break;
    //                case "ItemC":
    //                    Debug.Log("�V�[���h");
    //                    break;
    //                case "ItemD":
    //                    Debug.Log("�X�s�[�h�A�b�v");
    //                    break;
    //                case "ItemE":
    //                    Debug.Log("�S��");
    //                    break;
    //                case "ItemF":
    //                    Debug.Log("�U�R��|");
    //                    break;
    //            }

    //            itemGetSound.Play();
    //            Destroy(collision.gameObject);      //Item�폜
    //        }
    //    }
    //}

}