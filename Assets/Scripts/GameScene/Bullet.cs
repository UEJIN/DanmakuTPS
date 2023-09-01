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

    [SerializeField] float spriteAngle; // �摜�̏����p�x
    [SerializeField] float damage; // �p�x
    [SerializeField] float default_speed; // ���x
    [SerializeField] float lifeTime = 5f; // ������܂ł̕b��

    //������e�ɏ��L�҂̏����������ē����͐؂肽���i�v���C���[�̂ݓ�������B�j

    void Start()
    {
        // X�����̈ړ��ʂ�ݒ肷��
        velocity.x = speed * Mathf.Cos(angle * Mathf.Deg2Rad);

        // Y�����̈ړ��ʂ�ݒ肷��
        velocity.y = speed * Mathf.Sin(angle * Mathf.Deg2Rad);

        // �e�̌�����ݒ肷��
        float zAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(0, 0, zAngle+ spriteAngle);

        // 5�b��ɍ폜
        Destroy(gameObject, lifeTime);

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
        speed = default_speed + input_speed;
        ownerID = input_ownerID;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("other�F" + other);

        //�ǂɓ�����Ə�����
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        
        //����̋���
        if (PhotonNetwork.LocalPlayer.ActorNumber != ownerID)�@
        {
            //�Փ˂����I�u�W�F�N�g��Player�^�O�t��������A�Ȃ������ꂪ�����̃v���C���[�̏ꍇ
            if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                //RPC����āA�����̃I�u�W�F�N�g�̃��\�b�h���Ă�
                other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage, ownerID);

                Debug.Log("��e�̏��L�ҁF"+ ownerID);

            }
        }

        //NPC�ȊO�̂��܂�NPC�ɓ������
        if (ownerID != 0 && other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage, ownerID);
            //Destroy(this.gameObject);
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