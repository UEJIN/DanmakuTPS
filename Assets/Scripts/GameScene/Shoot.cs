using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shoot : MonoBehaviourPunCallbacks
{
    [SerializeField] Camera fpsCamera;
    //float timeCount = 0; // �o�ߎ���
    float shotAngle = 0; // ���ˊp�x
    [SerializeField] GameObject shotBullet; // ���˂���e
    AudioSource shootSound; //AudioSource��錾
    float timeCount_circle = 0;
    float timeCount_voltex = 0;
    float timeCount_random = 0;
    public int ownerID;
    [SerializeField] GameObject bullet_offline_prefab; 

    void Awake()
    {
        shootSound = GameObject.Find("ShotSound").GetComponent<AudioSource>(); //�V�[���ɂ���I�u�W�F�N�g��T���A�R���|�[�l���g���擾
        //bullet_offline_prefab = GameObject.Find("Bullet_offline");

        //shotType = 3;
    }

    void Start()
    {
        ownerID = photonView.Owner.ActorNumber;
        Debug.Log("shoot owner ID = " + ownerID);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.GetComponent<PlayerStatus>().shotLv_circle += 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.GetComponent<PlayerStatus>().shotLv_voltex += 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.GetComponent<PlayerStatus>().shotLv_random += 1;
        }

        // �O�t���[������̎��Ԃ̍������Z
        timeCount_voltex += Time.deltaTime;
        timeCount_circle += Time.deltaTime;
        timeCount_random += Time.deltaTime;

            //Debug.Log("shotLv_voltex=" + transform.GetComponent<PlayerStatus>().shotLv_voltex);
            //Debug.Log("shotLv_circle=" + transform.GetComponent<PlayerStatus>().shotLv_circle);
            //Debug.Log("shotLv_random=" + transform.GetComponent<PlayerStatus>().shotLv_random);

            //�~�`
            if (transform.GetComponent<PlayerStatus>().shotLv_circle > 0)
            {

                switch (transform.GetComponent<PlayerStatus>().shotLv_circle)
                {
                    case 1:
                        //4way cross
                        if (timeCount_circle > 0.5f)
                        {
                            timeCount_circle = 0;
                            InstBullet(0, transform.position, 3);
                            InstBullet(90, transform.position, 3);
                            InstBullet(180, transform.position, 3);
                            InstBullet(270, transform.position, 3);
                        }
                        break;

                    case 2:
                        //8way cross
                        if (timeCount_circle > 0.5f)
                        {
                            timeCount_circle = 0;
                            InstBullet(0, transform.position, 3);
                            InstBullet(90, transform.position, 3);
                            InstBullet(180, transform.position, 3);
                            InstBullet(270, transform.position, 3);
                            InstBullet(45, transform.position, 3);
                            InstBullet(135, transform.position, 3);
                            InstBullet(225, transform.position, 3);
                            InstBullet(315, transform.position, 3);
                        }
                        break;

                    case 3:
                        if (timeCount_circle > 0.5f)
                        {
                            timeCount_circle = 0; 
                            InstBullet(0, transform.position, 3);
                            InstBullet(90, transform.position, 3);
                            InstBullet(180, transform.position, 3);
                            InstBullet(270, transform.position, 3);
                            InstBullet(45, transform.position, 3);
                            InstBullet(135, transform.position, 3);
                            InstBullet(225, transform.position, 3);
                            InstBullet(315, transform.position, 3);
                            InstBullet(67.5f, transform.position, 3);
                            InstBullet(157.5f, transform.position, 3);
                            InstBullet(247.5f, transform.position, 3);
                            InstBullet(337.5f, transform.position, 3);
                        }
                        break;

                    case 4:
                        if (timeCount_circle > 0.5f)
                        {
                            timeCount_circle = 0; 
                            InstBullet(0, transform.position, 3);
                            InstBullet(90, transform.position, 3);
                            InstBullet(180, transform.position, 3);
                            InstBullet(270, transform.position, 3);
                            InstBullet(45, transform.position, 3);
                            InstBullet(135, transform.position, 3);
                            InstBullet(225, transform.position, 3);
                            InstBullet(315, transform.position, 3);
                            InstBullet(67.5f, transform.position, 3);
                            InstBullet(157.5f, transform.position, 3);
                            InstBullet(247.5f, transform.position, 3);
                            InstBullet(337.5f, transform.position, 3);
                            InstBullet(22.5f, transform.position, 3);
                            InstBullet(112.5f, transform.position, 3);
                            InstBullet(202.5f, transform.position, 3);
                            InstBullet(292.5f, transform.position, 3);
                        }
                        break;

                    default:
                        if (timeCount_circle > 0.5f)
                        {
                            timeCount_circle = 0; 
                            InstBullet(0, transform.position, 3);
                            InstBullet(90, transform.position, 3);
                            InstBullet(180, transform.position, 3);
                            InstBullet(270, transform.position, 3);
                            InstBullet(45, transform.position, 3);
                            InstBullet(135, transform.position, 3);
                            InstBullet(225, transform.position, 3);
                            InstBullet(315, transform.position, 3);
                            InstBullet(67.5f, transform.position, 3);
                            InstBullet(157.5f, transform.position, 3);
                            InstBullet(247.5f, transform.position, 3);
                            InstBullet(337.5f, transform.position, 3);
                            InstBullet(22.5f, transform.position, 3);
                            InstBullet(112.5f, transform.position, 3);
                            InstBullet(202.5f, transform.position, 3);
                            InstBullet(292.5f, transform.position, 3);
                        }
                        break;
                }
            }

            //�Q����
            if (transform.GetComponent<PlayerStatus>().shotLv_voltex > 0)
            {

                switch (transform.GetComponent<PlayerStatus>().shotLv_voltex)
                {
                    case 1:
                        //1way uzumaki
                        if (timeCount_voltex > 0.2f)
                        {
                            timeCount_voltex = 0;   
                            shotAngle += 20;
                            InstBullet(shotAngle, transform.position, 6);
                        }
                        break;

                    case 2:
                        //1way uzumaki
                        if (timeCount_voltex > 0.2f)
                        {
                            timeCount_voltex = 0; 
                            shotAngle += 20;
                            InstBullet(shotAngle, transform.position, 6);
                            InstBullet(shotAngle + 180, transform.position, 6);
                        }
                        break;

                    case 3:
                        //1way uzumaki
                        if (timeCount_voltex > 0.2f)
                        {
                            timeCount_voltex = 0;
                            shotAngle += 20;
                            InstBullet(shotAngle, transform.position, 6);
                            InstBullet(shotAngle + 120, transform.position, 6);
                            InstBullet(shotAngle + 240, transform.position, 6);
                        }
                        break;

                    case 4:
                        //1way uzumaki
                        if (timeCount_voltex > 0.2f)
                        {
                            timeCount_voltex = 0; 
                            shotAngle += 20;
                            InstBullet(shotAngle, transform.position, 6);
                            InstBullet(shotAngle + 90, transform.position, 6);
                            InstBullet(shotAngle + 180, transform.position, 6);
                            InstBullet(shotAngle + 270, transform.position, 6);
                        }
                        break;

                    default:
                        //1way uzumaki
                        if (timeCount_voltex > 0.2f)
                        {
                            timeCount_voltex = 0; 
                            shotAngle += 20;
                            InstBullet(shotAngle, transform.position, 6);
                            InstBullet(shotAngle + 90, transform.position, 6);
                            InstBullet(shotAngle + 180, transform.position, 6);
                            InstBullet(shotAngle + 270, transform.position, 6);
                        }
                        break;

                }
            }

            //�����_��
            if (transform.GetComponent<PlayerStatus>().shotLv_random > 0)
            {

                switch (transform.GetComponent<PlayerStatus>().shotLv_random)
                {
                    case 1:
                        //random
                        if (timeCount_random > 0.5f)
                        {
                            timeCount_random = 0; 
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                        }
                        break;

                    case 2:
                        if (timeCount_random > 0.5f)
                        {
                            timeCount_random = 0;
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                        }
                        break;

                    case 3:
                        if (timeCount_random > 0.5f)
                        {
                            timeCount_random = 0;
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                        }
                        break;

                    case 4:
                        if (timeCount_random > 0.5f)
                        {
                            timeCount_random = 0;
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                        }
                        break;

                    default:
                        if (timeCount_random > 0.5f)
                        {
                            timeCount_random = 0; 
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                            InstBullet(Random.Range(0, 360), transform.position, 8);
                        }
                        break;


                }
            }
     
    }

    //private void InstBullet(float shotAngle, Vector3 offsetPos, float shotSpeed )
    //{
    //    if (this.gameObject.GetComponent<PhotonView>().IsMine)
    //    {
    //        GameObject createObject = PhotonNetwork.Instantiate("Bullet", offsetPos, Quaternion.identity);
    //        // ��������GameObject�ɐݒ肳��Ă���ABullet�X�N���v�g���擾����
    //        Bullet bulletScript = createObject.GetComponent<Bullet>();
    //        // Bullet�X�N���v�g��Init���Ăяo��
    //        bulletScript.Init(shotAngle, shotSpeed, ownerID);
    //        //�����̍U���̐F��ς���
    //        createObject.GetComponent<SpriteRenderer>().color = new Color(107, 162, 255);

    //        //photonView.RPC("ShootEffect", RpcTarget.AllBuffered); //�S�v���C���[�ɔ��f�����邽�߁ARPC���\�b�h�ɓn��
    //        ShootEffect();
    //    }
    //}

    ////[PunRPC] //RPC���\�b�h
    //public void ShootEffect()
    //{
    //    shootSound.Play(); //�e�����Đ�
    //}

    private void InstBullet(float shotAngle, Vector3 offsetPos, float shotSpeed)
    {

        GameObject createObject = Instantiate(bullet_offline_prefab, offsetPos, Quaternion.identity);
        // ��������GameObject�ɐݒ肳��Ă���ABullet�X�N���v�g���擾����
        Bullet bulletScript = createObject.GetComponent<Bullet>();
        // Bullet�X�N���v�g��Init���Ăяo��
        bulletScript.Init(shotAngle, shotSpeed, ownerID);

        if (this.gameObject.GetComponent<PhotonView>().IsMine)
        {
            //�����̍U���̐F��ς���
            createObject.GetComponent<SpriteRenderer>().color = new Color(107, 162, 255);
            ShootEffect();
        }
    }

    public void ShootEffect()
    {
        shootSound.Play(); //�e�����Đ�
    }

}