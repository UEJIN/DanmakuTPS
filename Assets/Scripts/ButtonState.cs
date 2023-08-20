using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �{�^�����
public class ButtonState : MonoBehaviour
{
    bool pressed = false;
    bool down = false;
    bool up = false;

    // �|�C���^�_�E�����ɌĂ΂��
    public void OnPointerDown()
    {
        if (this.pressed == false)
        {
            this.down = true;
            Debug.Log("�{�^���_�E��" + this.gameObject.name);
        }
        this.pressed = true;
    }

    // �|�C���^�A�b�v���ɌĂ΂��
    public void OnPointerUp()
    {
        if (this.pressed == true)
        {
            this.up = true;
            Debug.Log("�{�^���A�b�v" + this.gameObject.name);
        }
        this.pressed = false;
    }

    public void OnPointerExit()
    {
        if (this.pressed == true)
        {
            this.down = false;
            Debug.Log("�{�^��exit" + this.gameObject.name);
        }
        this.pressed = false;
    }

    // �{�^�������̎擾
    public bool IsPressed()
    {
        return this.pressed;
    }

    // �{�^���_�E���̎擾
    public bool IsDown()
    {
        bool result = this.down;
        this.down = false;
        return result;
    }

    // �{�^���A�b�v�̎擾
    public bool IsUp()
    {
        bool result = this.up;
        this.up = false;
        return result;
    }
}