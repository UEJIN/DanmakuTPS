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

    public int shotType = 0;

    void Start()
    {
        shootSound = GameObject.Find("ShotSound").GetComponent<AudioSource>(); //�V�[���ɂ���I�u�W�F�N�g��T���A�R���|�[�l���g���擾
        shotType = 3;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            shotType = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            shotType = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            shotType = 3;
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

            switch(shotType)
            {
                case 1:
                    //4way cross
                    InstBullet(0, transform.position,3);
                    InstBullet(90, transform.position,3);
                    InstBullet(180, transform.position,3);
                    InstBullet(270, transform.position,3);
                    break;

                case 2:
                    //3way horizontal
                    InstBullet(270, transform.position,3);
                    InstBullet(270, transform.position + new Vector3(1,0,0),3);
                    InstBullet(270, transform.position + new Vector3(-1, 0, 0), 3);
                    break;

                case 3:
                    //1way uzumaki
                    shotAngle += 20;
                    InstBullet(shotAngle, transform.position, 6);
                    break;

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