using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    Rigidbody2D itemRd2d;     //���W�b�h�{�f�B�^�̕ϐ���錾
    SpriteRenderer itemRenderer;    //SpriteRenderer�^�̕ϐ���錾
    public Sprite[] sprites;        //�A�C�e���摜������z���錾


    void Start()
    {
        itemRd2d = GetComponent<Rigidbody2D>(); //
        itemRenderer = GetComponent<SpriteRenderer>();  //�ϐ��f�[�^���擾
        //int num = Random.Range(0, sprites.Length);
        itemRenderer.sprite = sprites[Random.Range(0, sprites.Length)];           //0�Ԗڂ̉摜���w��

        //int item = Random.Range(0, 100);                //Item�𗐐��Ō���
        //if (item < 30)                                  //������30�����Ȃ�
        //{
        //    itemRenderer.sprite = sprites[0];           //0�Ԗڂ̉摜���w��
        //}
        //else if (item < 50)
        //{
        //    itemRenderer.sprite = sprites[1];
        //}
        //else if (item < 70)
        //{
        //    itemRenderer.sprite = sprites[2];
        //}
        //else if (item < 85)
        //{
        //    itemRenderer.sprite = sprites[3];
        //}
        //else if (item < 95)
        //{
        //    itemRenderer.sprite = sprites[4];
        //}
        //else
        //{
        //    itemRenderer.sprite = sprites[5];
        //}
    }
}
