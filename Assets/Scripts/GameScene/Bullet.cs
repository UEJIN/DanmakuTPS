using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    [SerializeField] float angle; // �p�x
    [SerializeField] float speed; // ���x
    Vector3 velocity; // �ړ���

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
    public void Init(float input_angle, float input_speed)
    {
        angle = input_angle;
        speed = input_speed;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("�Փ�" + other.gameObject.name); //�Փ˂����I�u�W�F�N�g�̖��O���o��

        //if (other.gameObject.tag == "Tilemap_holl")
        //{
        //    //Destroy(this.gameObject);
        //    gameOverText.SetActive(true);
        //}

        //if (other.gameObject.tag == "Enemy")
        //{
        //    //Destroy(this.gameObject);
        //    gameOverText.SetActive(true);
        //}


        //if (this.gameObject.GetComponent<PhotonView>().IsMine)
        //{
        //    if (other.gameObject.CompareTag("Player") && !other.gameObject.GetComponent<PhotonView>().IsMine)
        //    //�Փ˂����I�u�W�F�N�g��Player�^�O�t��������A�Ȃ������ꂪ�����̃v���C���[�łȂ��ꍇ
        //    {
        //        Destroy(this.gameObject);
        //        other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 10f);
        //        //RPC����āA����I�u�W�F�N�g�̃��\�b�h���Ăԁi10f�Ƃ���float�l��n���j

        //    }
        //}
        if (!this.gameObject.GetComponent<PhotonView>().IsMine)�@//����̋���
        {
            if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PhotonView>().IsMine)
            //�Փ˂����I�u�W�F�N�g��Player�^�O�t��������A�Ȃ������ꂪ�����̃v���C���[�̏ꍇ
            {
                Destroy(this.gameObject);
                other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 10f);
                //RPC����āA�����̃I�u�W�F�N�g�̃��\�b�h���Ăԁi10f�Ƃ���float�l��n���j

            }
        }


    }


}