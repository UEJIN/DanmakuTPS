using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ボタン状態
public class ButtonState : MonoBehaviour
{
    bool pressed = false;
    bool down = false;
    bool up = false;

    // ポインタダウン時に呼ばれる
    public void OnPointerDown()
    {
        if (this.pressed == false)
        {
            this.down = true;
            Debug.Log("ボタンダウン" + this.gameObject.name);
        }
        this.pressed = true;
    }

    // ポインタアップ時に呼ばれる
    public void OnPointerUp()
    {
        if (this.pressed == true)
        {
            this.up = true;
            Debug.Log("ボタンアップ" + this.gameObject.name);
        }
        this.pressed = false;
    }

    public void OnPointerExit()
    {
        if (this.pressed == true)
        {
            this.down = false;
            Debug.Log("ボタンexit" + this.gameObject.name);
        }
        this.pressed = false;
    }

    // ボタン押下の取得
    public bool IsPressed()
    {
        return this.pressed;
    }

    // ボタンダウンの取得
    public bool IsDown()
    {
        bool result = this.down;
        this.down = false;
        return result;
    }

    // ボタンアップの取得
    public bool IsUp()
    {
        bool result = this.up;
        this.up = false;
        return result;
    }
}