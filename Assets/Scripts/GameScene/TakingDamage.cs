using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TakingDamage : MonoBehaviourPunCallbacks
{
    [SerializeField] Image hpBar; //Inspector����R�Â�

    private float hp; //�X�V�����HP��錾
    public float startHp = 100; //HP�̍ő�l

    // Start is called before the first frame update
    void Start()
    {
        hp = startHp; //HP�̏����l���ő�ɂ���
        hpBar.fillAmount = hp / startHp; //fillAmount�p��0�`1�̒l�ɕϊ����đ��
    }

    [PunRPC]
    public void TakeDamage(float _damage) //Player�ւ̓����蔻�肩��Ăяo����郁�\�b�h
    {
        hp -= _damage;
        hpBar.fillAmount = hp / startHp; //fillAmount���X�V

        if (hp <= 0f) //hp��0�ȉ��ɂȂ�����E�E�E
        {

        }
    }
}