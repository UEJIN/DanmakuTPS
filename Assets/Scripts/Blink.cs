using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Blink : MonoBehaviour
{

    //public
    public float speed = 1.0f;

    //private
    private Text text;
    private Image image;
    private TextMeshProUGUI tmpro;
    private float time;

    private enum ObjType
    {
        TEXT,
        IMAGE,
        TMPro
    };
    private ObjType thisObjType = ObjType.TEXT;

    void Start()
    {
        //�A�^�b�`���Ă�I�u�W�F�N�g�𔻕�
        if (this.gameObject.GetComponent<Image>())
        {
            thisObjType = ObjType.IMAGE;
            image = this.gameObject.GetComponent<Image>();
        }
        else if (this.gameObject.GetComponent<Text>())
        {
            thisObjType = ObjType.TEXT;
            text = this.gameObject.GetComponent<Text>();
        }
        else if (this.gameObject.GetComponent<TextMeshProUGUI>())
        {
            thisObjType = ObjType.TMPro;
            tmpro = this.gameObject.GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        //�I�u�W�F�N�g��Alpha�l���X�V
        if (thisObjType == ObjType.IMAGE)
        {
            image.color = GetAlphaColor(image.color);
        }
        else if (thisObjType == ObjType.TEXT)
        {
            text.color = GetAlphaColor(text.color);
        }
        else if(thisObjType == ObjType.TMPro)
        {
            tmpro.color = GetAlphaColor(tmpro.color);
        }
    }

    //Alpha�l���X�V����Color��Ԃ�
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }
}
