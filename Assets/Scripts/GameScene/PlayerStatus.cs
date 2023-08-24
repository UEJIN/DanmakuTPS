using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;


public class PlayerStatus : MonoBehaviourPunCallbacks //, IPunOwnershipCallbacks
{
    public int shotLv_voltex;//�T�[�o�[�̒l���ύX���ꂽ�炱�����X�V���ĕێ����Ă���
    public int shotLv_circle;
    public int shotLv_random;
    AudioSource itemGetSound; //AudioSource��錾
    public float nowHP;
    public float maxHP;
    [SerializeField] Image hpBar;

    // Start is called before the first frame update
    void Start()
    {
        //�T�[�o�[�̃f�[�^���擾���ď�����
        shotLv_voltex = photonView.Owner.GetShotLv_voltex();
        shotLv_circle = photonView.Owner.GetShotLv_circle();
        shotLv_random = photonView.Owner.GetShotLv_random();
        nowHP = photonView.Owner.GetNowHP();
        maxHP = 100f;
        hpBar.fillAmount = nowHP / maxHP;
        itemGetSound = GameObject.Find("ItemGetSound").GetComponent<AudioSource>(); //�V�[���ɂ���I�u�W�F�N�g��T���A�R���|�[�l���g���擾

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("shotLv_voltex=" + shotLv_voltex);
        Debug.Log("shotLv_circle=" + shotLv_circle);
        Debug.Log("shotLv_random=" + shotLv_random);
        if (photonView.IsMine) //���̃I�u�W�F�N�g�������Ȃ��
        {
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
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Item")  //"Item"�^�O�������Ă�����
        {
            
            if (photonView.IsMine) //���̃I�u�W�F�N�g�������Ȃ��
            {
                itemGetSound.Play();
                string name = collision.GetComponent<SpriteRenderer>().sprite.name; //Item�̖��O���擾

                //�A�C�e����邽�уX�R�A10�A�b�v
                PhotonNetwork.LocalPlayer.AddScore(10);

                if (name == collision.GetComponent<ItemManager>().sprites[0].name)
                {
                    this.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, -5f, PhotonNetwork.LocalPlayer.ActorNumber); //����HP5��
                    Debug.Log("HP 5UP"); //�f�o�b�O�Ŋm�F
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
            }

            Destroy(collision.gameObject);      //Item�폜

        }
    }


    //�N���̃J�X�^���v���p�e�B�i�X�e�[�^�X�j���ύX���ꂽ��Ă΂��
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        // �J�X�^���v���p�e�B���X�V���ꂽ�v���C���[�̃v���C���[����ID���R���\�[���ɏo�͂���
        Debug.Log($"{targetPlayer.NickName}({targetPlayer.ActorNumber})");

        // �X�V���ꂽ�v���C���[�̃J�X�^���v���p�e�B�̃y�A���R���\�[���ɏo�͂���
        foreach (var prop in changedProps)
        {
            Debug.Log($"{prop.Key}: {prop.Value}");
        }

        //���̃X�N���v�g���A�^�b�`���ꂽ�v���C���[���X�V���ꂽ��
        if(targetPlayer == photonView.Owner)
        {


            foreach (var prop in changedProps)
            {
                switch(prop.Key)
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
                    case "n_h":
                        nowHP = targetPlayer.GetNowHP();
                        hpBar.fillAmount = nowHP / maxHP;
                        break;

                }

            }
        }

    }
}
