using UnityEngine;
// Text を使用するため
using UnityEngine.UI;
using TMPro;

public class CopyrightUpdate : MonoBehaviour
{
    // 公開年
    [SerializeField] string year = string.Empty;
    // コピーライト表示欄
    //[SerializeField] Text copyright = null;


    void Start()
    {
        // バージョン
        var version = Application.version;
        // リリース元
        var companyName = Application.companyName;


        // コピーライト書き換え
        if (this.gameObject.GetComponent<TextMeshProUGUI>())
        {
           this.gameObject.GetComponent<TextMeshProUGUI>().text = $"Ver.{version} \n © {year} {companyName}";
        }
        else if (this.gameObject.GetComponent<Text>())
        {
           this.gameObject.GetComponent<Text>().text = $"Ver.{version} \n © {year} {companyName}";
        }




        //// コピーライト書き換え
        //copyright.text = $"Ver.{version} \n © {year} {companyName}";
    }
}