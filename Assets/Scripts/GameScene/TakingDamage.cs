using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class TakingDamage : MonoBehaviourPunCallbacks
{
    [SerializeField] Image hpBar;
    [SerializeField] GameObject deadText; //Inspector����R�Â�
    [SerializeField] TextMeshProUGUI killText; //Inspector����R�Â�
    [SerializeField] TextMeshProUGUI respawnText; //Inspector����R�Â�

    private float hp;
    public float startHp = 100;
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
        
        hp = startHp;
        hpBar.fillAmount = hp / startHp;

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
                this.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBufferedViaServer, 5f, PhotonNetwork.LocalPlayer.ActorNumber);
            }
        }
    }


    [PunRPC]
    public void TakeDamage(float _damage, int attackerID, PhotonMessageInfo info) //Photon�̂���info���n��
    {
        //if (!PhotonNetwork.InRoom) { return; }  //
        Debug.Log("call TakeDamage:damage="+ _damage+", sender="+ info.Sender.NickName);

        if (hp > 0f)
        {

            if (_damage > 0)�@�@//�_���[�W����
            {
                hp -= _damage;
                hpBar.fillAmount = hp / startHp;

                damageSound.Play();

                if (hp <= 0f)�@�@//���񂾂�
                {
                    deadText.SetActive(true);
                    Die();
                    dieSound.Play(); //���S�̂��m�点�����Đ�

                    if (photonView.IsMine) //�����œǂ񂾂�
                    {
                        killText.GetComponent<TextMeshProUGUI>().text = "You were killed by " + info.Sender.NickName; //�����̉�ʂɂ��ꂽ���Ƃ�\��
                        //PhotonNetwork.LocalPlayer.AddScore(10);
                        Player player = PhotonNetwork.LocalPlayer;
                        player.Get(attackerID).AddScore(10);
                    }
                }
            }
            if(_damage<0)�@�@//�񕜏���
            {
                if (hp - _damage > startHp)
                {
                    hp = startHp;       //����ȏ�͉񕜂��Ȃ�
                    hpBar.fillAmount = hp / startHp;
                }
                if(hp - _damage <= startHp)
                {
                    hp -= _damage;
                    hpBar.fillAmount = hp / startHp;
                }
                
            }

        }
    }

    void Die()
    {
        if (photonView.IsMine)
        {
           StartCoroutine(Respawn());
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
        int randomPoint = Random.Range(-20, 20);
        transform.position = new Vector3(randomPoint, randomPoint, 0); //�����_���ȏꏊ�ֈړ�
        transform.GetComponent<MovementController>().enabled = true; //��������
        transform.GetComponent<Shoot>().enabled = true; //���Ă���
        photonView.RPC("RegainHP", RpcTarget.AllBuffered); //RPC�Ń��\�b�h���Ă�

    }

    [PunRPC]
    public void RegainHP()
    {
        hp = startHp; //��
        hpBar.fillAmount = hp / startHp; //HPBar�ɔ��f
        deadText.SetActive(false); //���S��\��
    }
}