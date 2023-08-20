using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    Rigidbody2D itemRd2d;     //リジッドボディ型の変数を宣言
    SpriteRenderer itemRenderer;    //SpriteRenderer型の変数を宣言
    public Sprite[] sprites;        //アイテム画像を入れる配列を宣言


    void Start()
    {
        itemRd2d = GetComponent<Rigidbody2D>(); //
        itemRenderer = GetComponent<SpriteRenderer>();  //変数データを取得
        //int num = Random.Range(0, sprites.Length);
        itemRenderer.sprite = sprites[Random.Range(0, sprites.Length)];           //0番目の画像を指定

        //int item = Random.Range(0, 100);                //Itemを乱数で決定
        //if (item < 30)                                  //乱数が30未満なら
        //{
        //    itemRenderer.sprite = sprites[0];           //0番目の画像を指定
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
