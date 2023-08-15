using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TakingDamage : MonoBehaviourPunCallbacks
{
    [SerializeField] Image hpBar; //Inspectorから紐づけ

    private float hp; //更新されるHPを宣言
    public float startHp = 100; //HPの最大値

    // Start is called before the first frame update
    void Start()
    {
        hp = startHp; //HPの初期値を最大にする
        hpBar.fillAmount = hp / startHp; //fillAmount用に0～1の値に変換して代入
    }

    [PunRPC]
    public void TakeDamage(float _damage) //Playerへの当たり判定から呼び出されるメソッド
    {
        hp -= _damage;
        hpBar.fillAmount = hp / startHp; //fillAmountを更新

        if (hp <= 0f) //hpが0以下になったら・・・
        {

        }
    }
}