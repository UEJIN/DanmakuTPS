using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shoot : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    float timeCount = 0; // �o�ߎ���
    float shotAngle = 0; // ���ˊp�x
    [SerializeField] GameObject shotBullet; // ���˂���e

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0)) //���N���b�N�����ꍇ
        //{
        //    RaycastHit _hit; //Ray���Փ˂�������錾
        //    Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f)); //�J���������ʒ��S�Ɍ����Ĕ�΂�Ray���`

        //    if (Physics.Raycast(ray, out _hit, 100)) //Ray�������I�u�W�F�N�g�ɏՓ˂����ꍇ
        //    {
        //        Debug.Log(_hit.collider.gameObject.name); //�Փ˂����I�u�W�F�N�g�̖��O���o��

        //        if (_hit.collider.gameObject.CompareTag("Player") && !_hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
        //        //�Փ˂����I�u�W�F�N�g��Player�^�O�t��������A�Ȃ������ꂪ�����̃v���C���[�łȂ��ꍇ
        //        {
        //            //_hit.collider.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 10f); 
        //            //RPC����āA����I�u�W�F�N�g�̃��\�b�h���Ăԁi10f�Ƃ���float�l��n���j
        //        }
        //    }
        //}

        // �O�t���[������̎��Ԃ̍������Z
        timeCount += Time.deltaTime;

        // 0.1�b�𒴂��Ă��邩
        if (timeCount > 0.1f)
        {
            timeCount = 0; // �Ĕ��˂̂��߂Ɏ��Ԃ����Z�b�g

            //shotAngle += 20;
            //shotAngle = 0;

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

            if(Time.time%10 >= 5)
            {
                //4way cross
                InstBullet(0, transform.position);
                InstBullet(90, transform.position);
                InstBullet(180, transform.position);
                InstBullet(270, transform.position);
            }
            else
            {
            //3way horizontal
            InstBullet(270, transform.position);
            InstBullet(270, transform.position + new Vector3(1,0,0));
            InstBullet(270, transform.position + new Vector3(-1, 0, 0));

            }




        }


    }

    private void InstBullet(int shotAngle, Vector3 offsetPos )
    {
        if (this.gameObject.GetComponent<PhotonView>().IsMine)
        {
            GameObject createObject = PhotonNetwork.Instantiate("Bullet", offsetPos, Quaternion.identity);
            // ��������GameObject�ɐݒ肳��Ă���ABullet�X�N���v�g���擾����
            Bullet bulletScript = createObject.GetComponent<Bullet>();
            // Bullet�X�N���v�g��Init���Ăяo��
            bulletScript.Init(shotAngle, 6);
        }
    }


}