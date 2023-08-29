using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; //Photon�T�[�o�[�̏����g�p���邽��
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks //Photon view��Pun���g�p���邽��
{
    [SerializeField] GameObject playerPrefab;
    //GameObject itemObj;
    [SerializeField] public GameObject itemParent;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] public GameObject[] statusObjects;
    [SerializeField] public GameObject killCountObject;
    [SerializeField] public GameObject npcParent;

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

        //�z�X�g�Ȃ�
        if (PhotonNetwork.IsMasterClient)
        {
            //�t�B�[���h�̃A�C�e����5���ȉ��Ȃ烉���_���X�|�[��
            if (itemParent.transform.childCount < 5)
            {
                GameObject itemObj = PhotonNetwork.Instantiate("Item", new Vector2(Random.Range(-20, 20), Random.Range(-20, 20)), Quaternion.identity);
                ItemManager itemManager = itemObj.GetComponent<ItemManager>();
                itemManager.Init(Random.Range(0, itemManager.sprites.Length));
                itemObj.transform.parent = itemParent.transform;
            }

            //�v���C���[�����T�ȉ��Ȃ�NPC�X�|�[��
            if(npcParent.transform.childCount + PhotonNetwork.PlayerList.Length < 10)
            {
                GameObject npc = PhotonNetwork.InstantiateRoomObject(playerPrefab.name, new Vector2(Random.Range(-20, 20), Random.Range(-20, 20)), Quaternion.identity);
                npc.tag = "Enemy";

                PlayerSetUp playerSetUp = npc.GetComponent<PlayerSetUp>();
                playerSetUp.transform.GetComponent<MovementController>().enabled = false;
                playerSetUp.FPSCamera.GetComponent<Camera>().enabled = false;
                playerSetUp.miniMapMarker.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f);   //���@�̃}�[�J�[��Ԃɂ���
                playerSetUp.playerNameText.text = "NPC";

                PlayerStatus playerStatus = npc.GetComponent<PlayerStatus>();
                playerStatus.shotLv_voltex = Random.Range(0,3);
                playerStatus.shotLv_circle = Random.Range(0, 3);
                playerStatus.shotLv_random = Random.Range(0, 3);
                playerStatus.hpBar.fillAmount = playerStatus.nowHP / playerStatus.maxHP;

                npc.transform.parent = npcParent.transform;
            }

        }

        statusObjects[0].GetComponent<TextMeshProUGUI>().text = "SCORE : " + PhotonNetwork.LocalPlayer.GetScore().ToString();
        statusObjects[1].GetComponent<TextMeshProUGUI>().text = "CIRCLE Shot Lv "+PhotonNetwork.LocalPlayer.GetShotLv_circle().ToString();
        statusObjects[2].GetComponent<TextMeshProUGUI>().text = "VOLTEX Shot Lv "+PhotonNetwork.LocalPlayer.GetShotLv_voltex().ToString();
        statusObjects[3].GetComponent<TextMeshProUGUI>().text = "RANDOM Shot Lv "+PhotonNetwork.LocalPlayer.GetShotLv_random().ToString();
        killCountObject.GetComponent<TextMeshProUGUI>().text = PhotonNetwork.LocalPlayer.GetKillCount().ToString() + " Kill";
    }


    //[PunRPC]
    //public void ItemSpawn(int itemID, Vector2 vector2)
    //{
    //    GameObject itemObj = PhotonNetwork.Instantiate("Item", vector2, Quaternion.identity);
    //    ItemManager itemManager = itemObj.GetComponent<ItemManager>();
    //    itemManager.Init(itemID);
    //    itemObj.transform.parent = itemParent.transform;
    //}

}