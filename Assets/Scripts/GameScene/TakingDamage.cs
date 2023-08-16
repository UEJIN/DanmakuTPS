using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class TakingDamage : MonoBehaviourPunCallbacks
{
    [SerializeField] Image hpBar;
    [SerializeField] GameObject deadText; //Inspectorから紐づけ
    [SerializeField] TextMeshProUGUI killText; //Inspectorから紐づけ
    [SerializeField] TextMeshProUGUI respawnText; //Inspectorから紐づけ

    private float hp;
    public float startHp = 100;

    // Start is called before the first frame update
    void Start()
    {
        hp = startHp;
        hpBar.fillAmount = hp / startHp;
    }

    [PunRPC]
    public void TakeDamage(float _damage, PhotonMessageInfo info) //Photonのもつinfoも渡す
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
                killText.GetComponent<TextMeshProUGUI>().text = "You were killed by " + info.Sender.NickName; //自分の画面にやられたことを表示
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
        while (respawnTime > 0F) //0になるまで繰り返す
        {
            yield return new WaitForSeconds(1f);
            respawnTime -= 1.0f;
            transform.GetComponent<MovementController>().enabled = false; //動けない状態
            transform.GetComponent<Shoot>().enabled = false; //撃てない状態
            respawnText.GetComponent<TextMeshProUGUI>().text = "Respawning at: " + respawnTime.ToString();
        }
        killText.GetComponent<TextMeshProUGUI>().text = "";
        respawnText.GetComponent<TextMeshProUGUI>().text = "";
        int randomPoint = Random.Range(-20, 20);
        transform.position = new Vector3(randomPoint, randomPoint, 0); //ランダムな場所へ移動
        transform.GetComponent<MovementController>().enabled = true; //動ける状態
        transform.GetComponent<Shoot>().enabled = true; //撃てる状態
        photonView.RPC("RegainHP", RpcTarget.AllBuffered); //RPCでメソッドを呼ぶ

    }

    [PunRPC]
    public void RegainHP()
    {
        hp = startHp; //回復
        hpBar.fillAmount = hp / startHp; //HPBarに反映
        deadText.SetActive(false); //死亡非表示
    }
}