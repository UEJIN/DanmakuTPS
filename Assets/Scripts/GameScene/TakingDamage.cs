using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class TakingDamage : MonoBehaviourPunCallbacks
{
    [SerializeField] Image hpBar;
    [SerializeField] GameObject deadText; //Inspector‚©‚ç•R‚Ã‚¯
    [SerializeField] TextMeshProUGUI killText; //Inspector‚©‚ç•R‚Ã‚¯
    [SerializeField] TextMeshProUGUI respawnText; //Inspector‚©‚ç•R‚Ã‚¯

    private float hp;
    public float startHp = 100;

    // Start is called before the first frame update
    void Start()
    {
        hp = startHp;
        hpBar.fillAmount = hp / startHp;
    }

    [PunRPC]
    public void TakeDamage(float _damage, PhotonMessageInfo info) //Photon‚Ì‚à‚Âinfo‚à“n‚·
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
                killText.GetComponent<TextMeshProUGUI>().text = "You were killed by " + info.Sender.NickName; //©•ª‚Ì‰æ–Ê‚É‚â‚ç‚ê‚½‚±‚Æ‚ğ•\¦
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
        while (respawnTime > 0F) //0‚É‚È‚é‚Ü‚ÅŒJ‚è•Ô‚·
        {
            yield return new WaitForSeconds(1f);
            respawnTime -= 1.0f;
            transform.GetComponent<MovementController>().enabled = false; //“®‚¯‚È‚¢ó‘Ô
            transform.GetComponent<Shoot>().enabled = false; //Œ‚‚Ä‚È‚¢ó‘Ô
            respawnText.GetComponent<TextMeshProUGUI>().text = "Respawning at: " + respawnTime.ToString();
        }
        killText.GetComponent<TextMeshProUGUI>().text = "";
        respawnText.GetComponent<TextMeshProUGUI>().text = "";
        int randomPoint = Random.Range(-20, 20);
        transform.position = new Vector3(randomPoint, randomPoint, 0); //ƒ‰ƒ“ƒ_ƒ€‚ÈêŠ‚ÖˆÚ“®
        transform.GetComponent<MovementController>().enabled = true; //“®‚¯‚éó‘Ô
        transform.GetComponent<Shoot>().enabled = true; //Œ‚‚Ä‚éó‘Ô
        photonView.RPC("RegainHP", RpcTarget.AllBuffered); //RPC‚Åƒƒ\ƒbƒh‚ğŒÄ‚Ô

    }

    [PunRPC]
    public void RegainHP()
    {
        hp = startHp; //‰ñ•œ
        hpBar.fillAmount = hp / startHp; //HPBar‚É”½‰f
        deadText.SetActive(false); //€–S”ñ•\¦
    }
}