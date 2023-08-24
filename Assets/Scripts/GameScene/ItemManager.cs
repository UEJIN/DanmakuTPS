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
    int itemID;

    ExitGames.Client.Photon.Hashtable roomHash;
    private void Awake()
    {
        int itemID = (PhotonNetwork.CurrentRoom.CustomProperties[this.gameObject.GetComponent<PhotonView>().ViewID.ToString()] is int value) ? value : 0;
        itemRenderer.sprite = sprites[itemID];
    }

    void Start()
    {
        itemRd2d = GetComponent<Rigidbody2D>(); //
        itemRenderer = GetComponent<SpriteRenderer>();  //�ϐ��f�[�^���擾
        //int itemID = (PhotonNetwork.CurrentRoom.CustomProperties[this.gameObject.GetComponent<PhotonView>().ViewID.ToString()] is int value) ? value : 0;
        //Init(itemID);

        



        //int num = Random.Range(0, sprites.Length);
        //itemRenderer.sprite = sprites[Random.Range(0, sprites.Length)];           //0�Ԗڂ̉摜���w��

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

    private void FixedUpdate()
    {
        //if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(this.gameObject.GetComponent<PhotonView>().ViewID.ToString(), out object setObject))
        //{
        //    itemID = (int)setObject;
        //}

        //Debug.Log("start item id =" + itemID);
    }


    public void Init(int input_itemID)
    {
        itemRenderer.sprite = sprites[input_itemID];
        Debug.Log("ViewID:"+this.gameObject.GetComponent<PhotonView>().ViewID +"itemID:"+ input_itemID);
        
        //���[���v���p�e�B�ݒ�
        roomHash = new ExitGames.Client.Photon.Hashtable();
        roomHash.Add(this.gameObject.GetComponent<PhotonView>().ViewID.ToString(), input_itemID);
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("other�F" + other);

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
