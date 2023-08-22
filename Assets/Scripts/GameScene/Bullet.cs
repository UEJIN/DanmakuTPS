using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviourPunCallbacks
{ 
    [SerializeField] float angle; // �p�x
    [SerializeField] float speed; // ���x
    Vector3 velocity; // �ړ���
    [SerializeField] int ownerID;

    
    //������e�ɏ��L�҂̏����������ē����͐؂肽���i�v���C���[�̂ݓ�������B�j

    void Start()
    {
        // X�����̈ړ��ʂ�ݒ肷��
        velocity.x = speed * Mathf.Cos(angle * Mathf.Deg2Rad);

        // Y�����̈ړ��ʂ�ݒ肷��
        velocity.y = speed * Mathf.Sin(angle * Mathf.Deg2Rad);

        // �e�̌�����ݒ肷��
        float zAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(0, 0, zAngle);

        // 5�b��ɍ폜
        Destroy(gameObject, 5.0f);

    }
    void Update()
    {
        // ���t���[���A�e���ړ�������
        transform.position += velocity * Time.deltaTime;
    }

    // !!�ǉ�!!
    // �p�x�Ƒ��x��ݒ肷��֐�
    public void Init(float input_angle, float input_speed, int input_ownerID)
    {
        angle = input_angle;
        speed = input_speed;
        ownerID = input_ownerID;
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if (PhotonNetwork.LocalPlayer.ActorNumber != ownerID)�@//����̋���
        {
            if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PhotonView>().IsMine)
            //�Փ˂����I�u�W�F�N�g��Player�^�O�t��������A�Ȃ������ꂪ�����̃v���C���[�̏ꍇ
            {
                //RPC����āA�����̃I�u�W�F�N�g�̃��\�b�h���Ăԁi10f�Ƃ���float�l��n���j
                //other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 10f, ownerID);
                other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 10f, ownerID);

                //����

                Debug.Log("��e�̏��L�ҁF"+ ownerID);

                //this.gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer); //�󂯂����ō폜����ɂ͏��L����Ⴄ�K�v������
                //Destroy(this.gameObject);

            }
        }


        //if (!(this.gameObject.GetComponent<PhotonView>().IsMine && other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PhotonView>().IsMine))//�����Ɏ����̋ʂ����������ꍇ
        //{
        //    Destroy(this.gameObject);
        //}

    }

    //// IPunOwnershipCallbacks.OnOwnershipTransfered������
    //void IPunOwnershipCallbacks.OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    //{
    //    // �l�b�g���[�N�I�u�W�F�N�g���폜
    //    if (targetView.IsMine)
    //    {
    //        PhotonNetwork.Destroy(targetView.gameObject);
    //        Debug.Log("�����ڏ�");
    //    }
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