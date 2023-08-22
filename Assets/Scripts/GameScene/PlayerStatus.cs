using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerStatus : MonoBehaviourPunCallbacks //, IPunOwnershipCallbacks
{
    public int shotLv_voltex;
    public int shotLv_circle;
    public int shotLv_random;
    AudioSource itemGetSound; //AudioSource��錾
    public float nowHP;
    public float maxHP = 100;

    // Start is called before the first frame update
    void Start()
    {
        
        shotLv_voltex = 0;
        shotLv_circle = 1;
        shotLv_random = 0;
        itemGetSound = GameObject.Find("ItemGetSound").GetComponent<AudioSource>(); //�V�[���ɂ���I�u�W�F�N�g��T���A�R���|�[�l���g���擾

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("shotLv_voltex=" + shotLv_voltex);
        Debug.Log("shotLv_circle=" + shotLv_circle);
        Debug.Log("shotLv_random=" + shotLv_random);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Enemy")   //�L�q�ς�
        //{
        //    SubHP(1);               //�L�q�ς�			
        //}

        if (collision.tag == "Item")  //"Item"�^�O�������Ă�����
        {

                string name = collision.GetComponent<SpriteRenderer>().sprite.name; //Item�̖��O���擾


                if (name == collision.GetComponent<ItemManager>().sprites[0].name)
                {
                    this.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, -5f, PhotonNetwork.LocalPlayer.ActorNumber); //����HP5��
                    Debug.Log("HP 5UP"); //�f�o�b�O�Ŋm�F
                }
                if (name == collision.GetComponent<ItemManager>().sprites[1].name)
                {
                    shotLv_circle += 1;
                }
                if (name == collision.GetComponent<ItemManager>().sprites[2].name)
                {
                    shotLv_voltex += 1;
                }
                if (name == collision.GetComponent<ItemManager>().sprites[3].name)
                {
                    shotLv_random += 1;
                }

            if (photonView.IsMine) //���̃I�u�W�F�N�g�������Ȃ��
            {
                itemGetSound.Play();
                //collision.gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer); //�󂯂����ō폜����ɂ͏��L����Ⴄ�K�v������

            }

            Destroy(collision.gameObject);      //Item�폜

        }
    }

    //// IPunOwnershipCallbacks.OnOwnershipTransfered������
    //void IPunOwnershipCallbacks.OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    //{
    //    // �l�b�g���[�N�I�u�W�F�N�g���폜

    //    PhotonNetwork.Destroy(targetView.gameObject);
    //    Debug.Log("�����ڏ�");
    //}

    //// �ȉ��̃��\�b�h���������Ȃ��ƃG���[���o��
    //void IPunOwnershipCallbacks.OnOwnershipTransferFailed(PhotonView targetView, Player previousOwner)
    //{
    //    Debug.Log("�����ڏ����s");
    //}

    //void IPunOwnershipCallbacks.OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    //{

    //}

}
