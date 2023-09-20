using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviourPunCallbacks
{
    Rigidbody2D itemRd2d;     //���W�b�h�{�f�B�^�̕ϐ���錾
    [SerializeField] SpriteRenderer itemRenderer;    //SpriteRenderer�^�̕ϐ���錾
    public Sprite[] sprites;        //�A�C�e���摜������z���錾
    public Sprite[] ultSprites;        //�A�C�e���摜������z���錾

    int itemID;

    ExitGames.Client.Photon.Hashtable roomHash;



    private void Awake()
    {
        
        //���[���������̃A�C�e����ԓ���
        //�T�[�o�[�ɕۑ�����Ă�A�C�e��ID���擾���ē�������
        itemID = (PhotonNetwork.CurrentRoom.CustomProperties[this.gameObject.GetComponent<PhotonView>().ViewID.ToString()] is int value) ? value : 0;
        //ID�����ƂɃX�v���C�g��ݒ�
        if (CompareTag("Item"))
        {
            itemRenderer.sprite = sprites[itemID];
        }
        if (CompareTag("UltItem"))
        {
            itemRenderer.sprite = ultSprites[itemID];
        }
    }

    void Start()
    {
        itemRd2d = GetComponent<Rigidbody2D>(); //
        itemRenderer = GetComponent<SpriteRenderer>();  //�ϐ��f�[�^���擾

    }

    private void FixedUpdate()
    {

    }


    public void Init(int input_itemID)
    {
        //�A�C�e���������̏����ݒ�

        if (CompareTag("Item"))
        {
            itemRenderer.sprite = sprites[input_itemID];
            Debug.Log("ViewID:" + this.gameObject.GetComponent<PhotonView>().ViewID + "itemID:" + input_itemID);
        }
        if (CompareTag("UltItem"))
        {
            itemRenderer.sprite = ultSprites[input_itemID];
            Debug.Log("ViewID:" + this.gameObject.GetComponent<PhotonView>().ViewID + "ultitemID:" + input_itemID);
        }
       
        //���[���v���p�e�B�ݒ�B�A�C�e����ID�����[���ɕۑ����Ăق��v���C���[�Ɠ���������B
        roomHash = new ExitGames.Client.Photon.Hashtable();
        roomHash.Add(this.gameObject.GetComponent<PhotonView>().ViewID.ToString(), input_itemID);
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        //�ǂɓ�����Ə�����
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }

    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        // �X�V���ꂽ���[���̃J�X�^���v���p�e�B�̃y�A���R���\�[���ɏo�͂���
        foreach (var prop in propertiesThatChanged)
        {
            Debug.Log($"{prop.Key}: {prop.Value}");
        }
    }


}
