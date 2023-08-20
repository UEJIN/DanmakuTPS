using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerStatus : MonoBehaviourPunCallbacks
{
    public int shotLv_voltex;
    public int shotLv_circle;
    public int shotLv_random;
    AudioSource itemGetSound; //AudioSource��錾

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

        if (photonView.IsMine) //���̃I�u�W�F�N�g�������Ȃ��
        {
            if (collision.tag == "Item")    //"Item"�^�O�������Ă�����
            {
                string name = collision.GetComponent<SpriteRenderer>().sprite.name; //Item�̖��O���擾


                if (name == collision.GetComponent<ItemManager>().sprites[0].name)
                {
                    this.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, -5f); //����HP5��
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

                //switch (name)    //�擾����name��U�蕪��
                //{
                //    case "ItemA":               //name ��ItemA �̏ꍇ
                //        this.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, -5f); //����HP5��
                //        Debug.Log("HP 1UP"); //�f�o�b�O�Ŋm�F
                //        break;
                //    case "ItemB":
                //        Debug.Log("�p���[�V���b�g");
                //        break;
                //    case "ItemC":
                //        Debug.Log("�V�[���h");
                //        break;
                //    case "ItemD":
                //        Debug.Log("�X�s�[�h�A�b�v");
                //        break;
                //    case "ItemE":
                //        Debug.Log("�S��");
                //        break;
                //    case "ItemF":
                //        Debug.Log("�U�R��|");
                //        break;
                //}

                itemGetSound.Play();
                Destroy(collision.gameObject);      //Item�폜
            }
        }
    }

}
