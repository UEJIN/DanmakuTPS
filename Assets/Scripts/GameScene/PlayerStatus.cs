using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;

public class PlayerStatus : MonoBehaviourPunCallbacks //, IPunOwnershipCallbacks
{
    public int shotLv_voltex;//�T�[�o�[�̒l���ύX���ꂽ�炱�����X�V���ĕێ����Ă���
    public int shotLv_circle;
    public int shotLv_random;
    public int shotLv_aim;

    public int UltID;

    AudioSource itemGetSound; //AudioSource��錾
    public float nowHP;
    public float maxHP;
    [SerializeField] public Image hpBar;

    // Start is called before the first frame update
    void Awake()
    {
        //�T�[�o�[�̃f�[�^���擾���ď�����
        shotLv_voltex = photonView.Owner.GetShotLv_voltex();
        shotLv_circle = photonView.Owner.GetShotLv_circle();
        shotLv_random = photonView.Owner.GetShotLv_random();
        shotLv_aim = photonView.Owner.GetShotLv_aim();
        UltID = photonView.Owner.GetUltID();
        nowHP = photonView.Owner.GetNowHP();
        maxHP = 100f;
        hpBar.fillAmount = nowHP / maxHP;
        itemGetSound = GameObject.Find("ItemGetSound").GetComponent<AudioSource>(); //�V�[���ɂ���I�u�W�F�N�g��T���A�R���|�[�l���g���擾

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.CompareTag("Player"))
        {
            if (photonView.IsMine) //���̃I�u�W�F�N�g�������Ȃ��
            {
                //Debug.Log("shotLv_voltex=" + shotLv_voltex);
                //Debug.Log("shotLv_circle=" + shotLv_circle);
                //Debug.Log("shotLv_random=" + shotLv_random);

                //�e�X�g�p���x���A�b�v
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    PhotonNetwork.LocalPlayer.AddScore(10);
                    PhotonNetwork.LocalPlayer.AddShotLv_circle(1);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    PhotonNetwork.LocalPlayer.AddScore(10);
                    PhotonNetwork.LocalPlayer.AddShotLv_voltex(1);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    PhotonNetwork.LocalPlayer.AddScore(10);
                    PhotonNetwork.LocalPlayer.AddShotLv_random(1);
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    UltID++;
                }
            }
        }
    }

    //�Փˏ���
    void OnTriggerEnter2D(Collider2D collision)
    {
        //NPC�ł͂Ȃ��v���C���[��
        if (this.gameObject.CompareTag("Player"))
        {
            if (collision.CompareTag("Item"))  //"Item"�ɐG�ꂽ��
            {

                GetItem(collision);

            }
            if(collision.CompareTag("UltItem"))
            {
                GetUlt(collision);

            }
        }
    }

    //�A�C�e���擾���̏���
    public void GetItem(Collider2D collision)
    {
        if (photonView.IsMine) //���̃I�u�W�F�N�g�������Ȃ��
        {
            itemGetSound.Play();
            //Item�̖��O���擾
            string name = collision.GetComponent<SpriteRenderer>().sprite.name;

            //�A�C�e����邽�уX�R�A10�A�b�v
            PhotonNetwork.LocalPlayer.AddScore(10);

            if (name == collision.GetComponent<ItemManager>().sprites[0].name)
            {
                this.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, -5f, PhotonNetwork.LocalPlayer.ActorNumber); //����HP5��
            }
            if (name == collision.GetComponent<ItemManager>().sprites[1].name)
            {
                PhotonNetwork.LocalPlayer.AddShotLv_circle(1);
            }
            if (name == collision.GetComponent<ItemManager>().sprites[2].name)
            {
                PhotonNetwork.LocalPlayer.AddShotLv_voltex(1);
            }
            if (name == collision.GetComponent<ItemManager>().sprites[3].name)
            {
                PhotonNetwork.LocalPlayer.AddShotLv_random(1);
            }
            if (name == collision.GetComponent<ItemManager>().sprites[4].name)
            {
                PhotonNetwork.LocalPlayer.AddShotLv_aim(1);
            }


        }

        Destroy(collision.gameObject);      //Item�폜
    }

    public void GetUlt(Collider2D collision)
    {

        if (photonView.IsMine) //���̃I�u�W�F�N�g�������Ȃ��
        {
                
            //ult�z��̉��Ԗڂ�����
            int ultNum = Array.IndexOf(collision.GetComponent<ItemManager>().ultSprites, collision.GetComponent<SpriteRenderer>().sprite);
            //�T�[�o�[�ɃX�e�[�^�X�ۑ�
            PhotonNetwork.LocalPlayer.SetUltID(ultNum);
        }

        Destroy(collision.gameObject);      //Item�폜
    }

    //�X�e�[�^�X���������B�N���̃J�X�^���v���p�e�B�i�X�e�[�^�X�j���ύX���ꂽ��Ă΂��
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (this.gameObject.tag == "Player")
        {
            // �J�X�^���v���p�e�B���X�V���ꂽ�v���C���[�̃v���C���[����ID���R���\�[���ɏo�͂���
            Debug.Log($"{targetPlayer.NickName}({targetPlayer.ActorNumber})");

            // �X�V���ꂽ�v���C���[�̃J�X�^���v���p�e�B�̃y�A���R���\�[���ɏo�͂���
            foreach (var prop in changedProps)
            {
                Debug.Log($"{prop.Key}: {prop.Value}");
            }

            //���̃X�N���v�g���A�^�b�`���ꂽ�v���C���[���X�V���ꂽ��
            if (targetPlayer == photonView.Owner)
            {

                //�e�X�e�[�^�X�X�V
                foreach (var prop in changedProps)
                {
                    
                    switch (prop.Key)
                    {
                        case "s_v":
                            shotLv_voltex = targetPlayer.GetShotLv_voltex();
                            break;
                        case "s_c":
                            shotLv_circle = targetPlayer.GetShotLv_circle();
                            break;
                        case "s_r":
                            shotLv_random = targetPlayer.GetShotLv_random();
                            break;
                        case "s_a":
                            shotLv_aim = targetPlayer.GetShotLv_aim();
                            break;
                        case "u":
                            UltID = targetPlayer.GetUltID();
                            Debug.Log("������GetUltID:"+ photonView.Owner.GetUltID());
                            break;
                        case "n_h":
                            nowHP = targetPlayer.GetNowHP();
                            hpBar.fillAmount = nowHP / maxHP;
                            break;

                    }

                }
            }
        }

    }
}
