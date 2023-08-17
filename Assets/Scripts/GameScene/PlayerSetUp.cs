using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetUp : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject FPSCamera;
    //[SerializeField] Text playerNameText;
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] GameObject miniMapMarker;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine) //このオブジェクトが自分がPhotonを介して生成したものならば
        {
            transform.GetComponent<MovementController>().enabled = true; //MovementController.csを有効にする
            FPSCamera.GetComponent<Camera>().enabled = true; //FPSCameraのCameraコンポーネントを有効にする
            miniMapMarker.GetComponent<SpriteRenderer>().color = new Color(0f, 255f, 0f);   //自機のマーカーを緑にする

        }
        else
        {
            transform.GetComponent<MovementController>().enabled = false;
            FPSCamera.GetComponent<Camera>().enabled = false;
        }

        if (playerNameText != null) //Textオブジェクトが空でなければ
        {
            playerNameText.text = photonView.Owner.NickName; //ログインした名前を代入
        }
    }
}