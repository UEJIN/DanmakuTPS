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

    [SerializeField] bool isBeam = false; // ������܂ł̕b��
    [SerializeField] bool isRotate = false; // ������܂ł̕b��
    [SerializeField] float rotateSpeed = 3f; // ������܂ł̕b��

    [SerializeField, Tooltip("�Ǐ]")]
    private bool isFollow = false;
     
    [SerializeField, Tooltip("�^�[�Q�b�g�I�u�W�F�N�g")]
    private GameObject TargetObject;

    [SerializeField, Tooltip("�����ʒu")]
    private Vector3 initialPosition;

    [SerializeField, Tooltip("��]��")]
    private Vector3 RotateAxis;

    [SerializeField, Tooltip("���x�W��")]
    private float SpeedFactor = 0.1f;

    float nowLifeTime;

    //������e�ɏ��L�҂̏����������ē����͐؂肽���i�v���C���[�̂ݓ�������B�j

    void Start()
    {

        SetDirection();

        nowLifeTime = lifeTime;
        initialPosition = this.transform.position;

        // lifetime�b��ɍ폜
        Destroy(gameObject, lifeTime);

    }
    void Update()
    {
        nowLifeTime -= Time.deltaTime;

        // ���t���[���A�e���ړ�������
        transform.position += velocity * Time.deltaTime;

        //�r�[���i�L�т�j
        if(isBeam)
        {

            if (nowLifeTime > lifeTime/2)
            {
                GetComponent<SpriteRenderer>().size += new Vector2(0, speed * Time.deltaTime * 10f);

            }
            else if(GetComponent<SpriteRenderer>().size.x>0)
            {
                GetComponent<SpriteRenderer>().size -= new Vector2(speed * Time.deltaTime * 1f, 0);
            }

        }

        //��]
        if(isRotate)
        {
            Rotation();
        }

        //�Ǐ]
        if(isFollow && ownerID!=0 && PhotonView.Find(ownerID * 1000 + 1) != null) //NPC�̏ꍇ�͖���
        {
            GameObject owner = PhotonView.Find(ownerID * 1000 + 1).gameObject;
            transform.parent = owner.transform;
        }

    }

    //���˕����Ɖ摜�̕\���p�x�̏����ݒ�
    public void SetDirection()
    {
        // X�����̈ړ��ʂ�ݒ肷��
        velocity.x = speed * Mathf.Cos(angle * Mathf.Deg2Rad);

        // Y�����̈ړ��ʂ�ݒ肷��
        velocity.y = speed * Mathf.Sin(angle * Mathf.Deg2Rad);

        // �e�̌�����ݒ肷��
        float zAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(0, 0, zAngle + spriteAngle);
    }

    //�����ʒu�𒆐S�ɉ�]����B
    public void Rotation()
    {
        RotateAxis = new Vector3(0, 0, 1);

        // �w��I�u�W�F�N�g�𒆐S�ɉ�]����
        this.transform.RotateAround(
            initialPosition,
            RotateAxis,
            360.0f / (1.0f / SpeedFactor) * Time.deltaTime
            );

        //�I�u�W�F�N�g�̌�����Ǐ]������
        this.transform.Rotate(0, 0, -(360.0f / (1.0f / SpeedFactor) * Time.deltaTime)/2);

    }

  
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
        if (other.gameObject.CompareTag("Wall") && !isBeam)
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

}