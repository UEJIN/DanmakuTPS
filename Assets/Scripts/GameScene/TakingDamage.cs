using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class TakingDamage : MonoBehaviourPunCallbacks
{
    [SerializeField] Image hpBar;
    [SerializeField] GameObject deadText; //Inspector����R�Â�
    [SerializeField] TextMeshProUGUI killText; //Inspector����R�Â�
    [SerializeField] TextMeshProUGUI respawnText; //Inspector����R�Â�

    private float hp;
    public float startHp = 100;

    // Start is called before the first frame update
    void Start()
    {
        hp = startHp;
        hpBar.fillAmount = hp / startHp;
    }

    [PunRPC]
    public void TakeDamage(float _damage, PhotonMessageInfo info) //Photon�̂���info���n��
    {
        hp -= _damage;
        hpBar.fillAmount = hp / startHp;

        //if (hp & lt;= 0f)
        if (hp <= 0f)
        {
            deadText.SetActive(true);
            Die();
            if (photonView.IsMine)
            {
                killText.GetComponent<TextMeshProUGUI>().text = "You were killed by " + info.Sender.NickName; //�����̉�ʂɂ��ꂽ���Ƃ�\��
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