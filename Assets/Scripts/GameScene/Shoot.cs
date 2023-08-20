using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shoot : MonoBehaviourPunCallbacks
{
    [SerializeField] Camera fpsCamera;
    float timeCount = 0; // �o�ߎ���
    float shotAngle = 0; // ���ˊp�x
    [SerializeField] GameObject shotBullet; // ���˂���e
    AudioSource shootSound; //AudioSource��錾

    //public int shotType = 0;


    void Awake()
    {
        shootSound = GameObject.Find("ShotSound").GetComponent<AudioSource>(); //�V�[���ɂ���I�u�W�F�N�g��T���A�R���|�[�l���g���擾
        //shotType = 3;
    }

    //void Start()
    //{}

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
        timeCount += Time.deltaTime;

        // 0.1�b�𒴂��Ă��邩
        if (timeCount > 0.2f)
        {
            timeCount = 0; // �Ĕ��˂̂��߂Ɏ��Ԃ����Z�b�g

            // GameObject��V���ɐ�������
            // �������F��������GameObject
            // �������F����������W
            // ��O�����F��������p�x
            // �߂�l�F��������GameObject
            //if (this.gameObject.GetComponent<PhotonView>().IsMine)
            //{
            //    GameObject createObject = PhotonNetwork.Instantiate("Bullet", transform.position, Quaternion.identity);
            //    // ��������GameObject�ɐݒ肳��Ă���ABullet�X�N���v�g���擾����
            //    Bullet bulletScript = createObject.GetComponent<Bullet>();
            //    // Bullet�X�N���v�g��Init���Ăяo��
            //    bulletScript.Init(shotAngle, 3);
            //}


            //Debug.Log("shotLv_voltex=" + transform.GetComponent<PlayerStatus>().shotLv_voltex);
            //Debug.Log("shotLv_circle=" + transform.GetComponent<PlayerStatus>().shotLv_circle);
            //Debug.Log("shotLv_random=" + transform.GetComponent<PlayerStatus>().shotLv_random);

            //switch (shotType)
            //{
            //    case 1:
            //        //4way cross
            //        InstBullet(0, transform.position,3);
            //        InstBullet(90, transform.position,3);
            //        InstBullet(180, transform.position,3);
            //        InstBullet(270, transform.position,3);
            //        break;

            //    case 2:
            //        //3way horizontal
            //        InstBullet(270, transform.position,3);
            //        InstBullet(270, transform.position + new Vector3(1,0,0),3);
            //        InstBullet(270, transform.position + new Vector3(-1, 0, 0), 3);
            //        break;

            //    case 3:
            //        //1way uzumaki
            //        shotAngle += 20;
            //        InstBullet(shotAngle, transform.position, 6);
            //        break;

            //}
            if (transform.GetComponent<PlayerStatus>().shotLv_circle > 0)
            {

                switch (transform.GetComponent<PlayerStatus>().shotLv_circle)
                {
                    case 1:
                        //4way cross
                        InstBullet(0, transform.position, 3);
                        InstBullet(90, transform.position, 3);
                        InstBullet(180, transform.position, 3);
                        InstBullet(270, transform.position, 3);
                        break;

                    case 2:
                        //8way cross
                        InstBullet(0, transform.position, 3);
                        InstBullet(90, transform.position, 3);
                        InstBullet(180, transform.position, 3);
                        InstBullet(270, transform.position, 3);
                        InstBullet(45, transform.position, 3);
                        InstBullet(135, transform.position, 3);
                        InstBullet(225, transform.position, 3);
                        InstBullet(315, transform.position, 3);
                        break;

                    case 3:
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
                        break;

                    case 4:
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
                        //8way cross
                        //InstBullet(0, transform.position, 5);
                        //InstBullet(90, transform.position, 5);
                        //InstBullet(180, transform.position, 5);
                        //InstBullet(270, transform.position, 5);
                        //InstBullet(45, transform.position, 5);
                        //InstBullet(135, transform.position, 5);
                        //InstBullet(225, transform.position, 5);
                        //InstBullet(315, transform.position, 5);
                        break;

                }
            }

            if (transform.GetComponent<PlayerStatus>().shotLv_voltex > 0)
            {

                switch (transform.GetComponent<PlayerStatus>().shotLv_voltex)
                {
                    case 1:
                        //1way uzumaki
                        shotAngle += 20;
                        InstBullet(shotAngle, transform.position, 6);
                        break;

                    case 2:
                        //1way uzumaki
                        shotAngle += 20;
                        InstBullet(shotAngle, transform.position, 6);
                        InstBullet(shotAngle + 180, transform.position, 6);
                        break;

                    case 3:
                        //1way uzumaki
                        shotAngle += 20;
                        InstBullet(shotAngle, transform.position, 6);
                        InstBullet(shotAngle + 120, transform.position, 6);
                        InstBullet(shotAngle + 240, transform.position, 6);
                        break;

                    case 4:
                        //1way uzumaki
                        shotAngle += 20;
                        InstBullet(shotAngle, transform.position, 6);
                        InstBullet(shotAngle + 90, transform.position, 6);
                        InstBullet(shotAngle + 180, transform.position, 6);
                        InstBullet(shotAngle + 270, transform.position, 6);
                        break;

                }
            }

            if (transform.GetComponent<PlayerStatus>().shotLv_random > 0)
            {

                switch (transform.GetComponent<PlayerStatus>().shotLv_random)
                {
                    case 1:
                        //random

                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);

                        break;

                    case 2:
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        break;

                    case 3:
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        break;

                    case 4:
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        InstBullet(Random.Range(0, 360), transform.position, 8);
                        break;


                }
            }




        }


    }

    private void InstBullet(float shotAngle, Vector3 offsetPos, float shotSpeed )
    {
        if (this.gameObject.GetComponent<PhotonView>().IsMine)
        {
            GameObject createObject = PhotonNetwork.Instantiate("Bullet", offsetPos, Quaternion.identity);
            // ��������GameObject�ɐݒ肳��Ă���ABullet�X�N���v�g���擾����
            Bullet bulletScript = createObject.GetComponent<Bullet>();
            // Bullet�X�N���v�g��Init���Ăяo��
            bulletScript.Init(shotAngle, shotSpeed);
            //�����̍U���̐F��ς���
            createObject.GetComponent<SpriteRenderer>().color = new Color(107, 162, 255);

            photonView.RPC("ShootEffect", RpcTarget.AllBuffered); //�S�v���C���[�ɔ��f�����邽�߁ARPC���\�b�h�ɓn��

        }
    }

    [PunRPC] //RPC���\�b�h
    public void ShootEffect()
    {
        shootSound.Play(); //�e�����Đ�
    }

}