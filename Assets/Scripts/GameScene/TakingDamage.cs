using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class TakingDamage : MonoBehaviourPunCallbacks
{
    //[SerializeField] Image hpBar;
    [SerializeField] GameObject deadText; //Inspector����R�Â�
    [SerializeField] TextMeshProUGUI killText; //Inspector����R�Â�
    [SerializeField] TextMeshProUGUI respawnText; //Inspector����R�Â�

    private float hp;
    private float startHp ;
    public bool isInDamageZone = false;
    float timeCount = 0; // �o�ߎ���
    AudioSource dieSound; //AudioSource��錾
    AudioSource damageSound;

    // Start is called before the first frame update

    private void Awake()
    {
        dieSound = GameObject.Find("KillSound").GetComponent<AudioSource>(); //�V�[���ɂ���I�u�W�F�N�g��T���A�R���|�[�l���g���擾
        damageSound = GameObject.Find("DamageSound").GetComponent<AudioSource>();
    }

    void Start()
    {
        
        //hp = photonView.Owner.GetNowHP();
        //hpBar.fillAmount = hp / startHp;

    }
    public void Update()
    {
        if(isInDamageZone)
        {
            //Debug.Log("Time.time % 10 = " + Time.time % 10);
            // �O�t���[������̎��Ԃ̍������Z
            timeCount += Time.deltaTime;

            // 0.1�b�𒴂��Ă��邩
            if (timeCount > 1f)
            {
                timeCount = 0;
                //Debug.Log("�d�a�o��");
                this.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 5f, PhotonNetwork.LocalPlayer.ActorNumber);
            }
        }
    }


    [PunRPC]
    public void TakeDamage(float _damage, int attackerID, PhotonMessageInfo info) //Photon�̂���info���n��
    {
        //if (!PhotonNetwork.InRoom) { return; }  //
        Debug.Log("call TakeDamage:damage="+ _damage+", sender="+ info.Sender.NickName);
        
        //�����HP���擾
        hp = this.gameObject.GetComponent<PlayerStatus>().nowHP;
        startHp = this.gameObject.GetComponent<PlayerStatus>().maxHP;

        if (hp > 0f)
        {

            if (_damage > 0)�@�@//�_���[�W����
            {
                hp -= _damage;
                //hpBar.fillAmount = hp / startHp;

                if (hp <= 0f)�@�@//���񂾂�
                {

                    deadText.SetActive(true);
                    dieSound.Play(); //���S�̂��m�点�����Đ�

                    if (photonView.IsMine) //�����œǂ񂾂�
                    {
                        if (this.gameObject.tag == "Player") //�_���[�W�H�炤�̂����v���C���[�Ȃ�
                        {
                            PhotonNetwork.LocalPlayer.SetNowHP(0);
                            killText.GetComponent<TextMeshProUGUI>().text = "You were killed by " + info.Sender.NickName; //�����̉�ʂɂ��ꂽ���Ƃ�\��
                                                                                                                          //PhotonNetwork.LocalPlayer.AddScore(10);
                        }
                        else  //�_���[�W�H�炤�̂�NPC�Ȃ�
                        {
                            this.gameObject.GetComponent<PlayerStatus>().nowHP = 0;
                            this.gameObject.GetComponent<PlayerStatus>().hpBar.fillAmount = this.gameObject.GetComponent<PlayerStatus>().nowHP / this.gameObject.GetComponent<PlayerStatus>().maxHP;
                        }                            
                        
                        if (attackerID != 0) //NPC����̍U������Ȃ�������X�R�A����
                        {
                            Player player = PhotonNetwork.LocalPlayer;
                            player.Get(attackerID).AddScore(50); //�U�������l�ɃX�R�A
                            player.Get(attackerID).AddKillCount(1); //�U�������l�ɃL��+1

                            if(player.Get(attackerID).GetNowHP() + 50 > 100) //HP50������100������Ȃ�
                            {
                                player.Get(attackerID).SetNowHP(100); //100�ɂ���
                            }
                            else
                            {
                                player.Get(attackerID).AddNowHP(50);�@//50��
                            }
                        }

                        Die();
                    }
                }
                else�@//���ȂȂ�������
                {
                    damageSound.Play();

                    if (this.gameObject.tag == "Player")
                    {
                        if (photonView.IsMine) //�����œǂ񂾂�
                        {
                            PhotonNetwork.LocalPlayer.SetNowHP(hp);
                        }
                    }
                    else
                    {
                        this.gameObject.GetComponent<PlayerStatus>().nowHP = hp;
                        this.gameObject.GetComponent<PlayerStatus>().hpBar.fillAmount = this.gameObject.GetComponent<PlayerStatus>().nowHP / this.gameObject.GetComponent<PlayerStatus>().maxHP;
                    }
                }
            }
            if(_damage<0)�@�@//�񕜏���
            {
                if (hp - _damage > startHp)
                {
                    hp = startHp;       //����ȏ�͉񕜂��Ȃ�
                    PhotonNetwork.LocalPlayer.SetNowHP(hp);
                    //hpBar.fillAmount = hp / startHp;
                }
                if(hp - _damage <= startHp)
                {
                    hp -= _damage;
                    PhotonNetwork.LocalPlayer.SetNowHP(hp);
                    //hpBar.fillAmount = hp / startHp;
                }
                
            }

        }
    }

    void Die()
    {
        if (photonView.IsMine)
        {
 
            int i = 0;
            //while (i < PhotonNetwork.LocalPlayer.GetShotLv_voltex()) //0�ɂȂ�܂ŌJ��Ԃ�
            while (i < this.gameObject.GetComponent<PlayerStatus>().shotLv_circle) //0�ɂȂ�܂ŌJ��Ԃ�
            {
                GameObject itemObj = PhotonNetwork.Instantiate("Item", new Vector2(transform.localPosition.x + CircleHorizon(1f, 2.0f).x, transform.localPosition.y + CircleHorizon(1f, 2.0f).y), Quaternion.identity);
                ItemManager itemManager = itemObj.GetComponent<ItemManager>();
                itemManager.Init(1);

                //itemObj.transform.parent = GameManager.itemParent.transform;
                i++;
            }

            i = 0;
            //while (i < PhotonNetwork.LocalPlayer.GetShotLv_circle()) //0�ɂȂ�܂ŌJ��Ԃ�
            while (i < this.gameObject.GetComponent<PlayerStatus>().shotLv_voltex) //0�ɂȂ�܂ŌJ��Ԃ�
            {
                GameObject itemObj = PhotonNetwork.Instantiate("Item", new Vector2(transform.localPosition.x + CircleHorizon(1f, 2.0f).x, transform.localPosition.y + CircleHorizon(1f, 2.0f).y), Quaternion.identity);
                ItemManager itemManager = itemObj.GetComponent<ItemManager>();
                itemManager.Init(2);
                //itemObj.transform.parent = itemParent.transform;
                i++;
            }

            i = 0;
            //while (i < PhotonNetwork.LocalPlayer.GetShotLv_random()) //0�ɂȂ�܂ŌJ��Ԃ�
            while (i < this.gameObject.GetComponent<PlayerStatus>().shotLv_random) //0�ɂȂ�܂ŌJ��Ԃ�
            {
                GameObject itemObj = PhotonNetwork.Instantiate("Item", new Vector2(transform.localPosition.x + CircleHorizon(1f, 2.0f).x, transform.localPosition.y + CircleHorizon(1f, 2.0f).y), Quaternion.identity);
                ItemManager itemManager = itemObj.GetComponent<ItemManager>();
                itemManager.Init(3);
                //itemObj.transform.parent = itemParent.transform;
                i++;
            }
            i = 0;

            if (this.gameObject.tag == "Player")
            {
                StartCoroutine(Respawn());
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator Respawn()
    {
        float respawnTime = 5.0f;
        while (respawnTime > 0F) //0�ɂȂ�܂ŌJ��Ԃ�
        {
            yield return new WaitForSeconds(1f);
            respawnTime -= 1.0f;
            transform.GetComponent<MovementController>().enabled = false; //�����Ȃ����
            transform.GetComponent<Shoot>().enabled = false; //���ĂȂ����
            respawnText.GetComponent<TextMeshProUGUI>().text = "Respawning at: " + respawnTime.ToString();
        }
        killText.GetComponent<TextMeshProUGUI>().text = "";
        respawnText.GetComponent<TextMeshProUGUI>().text = "";
        //int randomPoint = Random.Range(-20, 20);
        transform.position = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), 0); //�����_���ȏꏊ�ֈړ�
        transform.GetComponent<MovementController>().enabled = true; //��������
        transform.GetComponent<Shoot>().enabled = true; //���Ă���
        photonView.RPC("RegainHP", RpcTarget.AllBuffered); //RPC�Ń��\�b�h���Ă�

    }

    [PunRPC]
    public void RegainHP()
    {
        //hp = startHp; //��
        //hpBar.fillAmount = hp / startHp; //HPBar�ɔ��f
        if (photonView.IsMine) //�����œǂ񂾂�
        {
            
            
            //�X�e�[�^�X���Z�b�g
            PhotonNetwork.LocalPlayer.AddShotLv_voltex(- PhotonNetwork.LocalPlayer.GetShotLv_voltex());
            PhotonNetwork.LocalPlayer.AddShotLv_circle(- PhotonNetwork.LocalPlayer.GetShotLv_circle());
            PhotonNetwork.LocalPlayer.AddShotLv_random(- PhotonNetwork.LocalPlayer.GetShotLv_random());
            PhotonNetwork.LocalPlayer.AddKillCount(-PhotonNetwork.LocalPlayer.GetKillCount());
            PhotonNetwork.LocalPlayer.SetNowHP(startHp);
            PhotonNetwork.LocalPlayer.SetScore(0);
        }
        deadText.SetActive(false); //���S��\��
    }

    public Vector3 CircleHorizon(float min, float max)
    {

        var angle = Random.Range(0, 360);
        var radius = Random.Range(min, max);
        var rad = angle * Mathf.Deg2Rad;
        var px = Mathf.Cos(rad) * radius;
        var py = Mathf.Sin(rad) * radius;
        return new Vector3(px, py, 0);
    }
}