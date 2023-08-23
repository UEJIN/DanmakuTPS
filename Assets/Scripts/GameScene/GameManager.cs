using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; //Photon�T�[�o�[�̏����g�p���邽��
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks //Photon view��Pun���g�p���邽��
{
    [SerializeField] GameObject playerPrefab;
    //GameObject itemObj;
    [SerializeField] GameObject itemParent;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] public GameObject[] statusObjects;
    [SerializeField] public GameObject killCountObject;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected) //�T�[�o�[�ɐڑ����Ă�����
        {

            if (playerPrefab != null)
            {
                int randomPoint = Random.Range(-20, 20);
                PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(randomPoint, randomPoint), Quaternion.identity); //Photon���������
            }
        }
    }


    void FixedUpdate()
    {
        timerText.text = Time.time.ToString("0.0");

        if (PhotonNetwork.IsMasterClient)
        {
            if (itemParent.transform.childCount < 5)
            {
                GameObject itemObj = PhotonNetwork.Instantiate("Item", new Vector2(Random.Range(-20, 20), Random.Range(-20, 20)), Quaternion.identity);
                ItemManager itemManager = itemObj.GetComponent<ItemManager>();
                itemManager.Init(Random.Range(0, itemManager.sprites.Length));
                itemObj.transform.parent = itemParent.transform;
            }
        }

        statusObjects[0].GetComponent<TextMeshProUGUI>().text = "SCORE : " + PhotonNetwork.LocalPlayer.GetScore().ToString();
        statusObjects[1].GetComponent<TextMeshProUGUI>().text = "CIRCLE Shot Lv "+PhotonNetwork.LocalPlayer.GetShotLv_circle().ToString();
        statusObjects[2].GetComponent<TextMeshProUGUI>().text = "VOLTEX Shot Lv "+PhotonNetwork.LocalPlayer.GetShotLv_voltex().ToString();
        statusObjects[3].GetComponent<TextMeshProUGUI>().text = "RANDOM Shot Lv "+PhotonNetwork.LocalPlayer.GetShotLv_random().ToString();
        killCountObject.GetComponent<TextMeshProUGUI>().text = PhotonNetwork.LocalPlayer.GetKillCount().ToString() + " Kill";
    }

}