using System;
using System.Text;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI roomNameText;
    [SerializeField] 
    private TextMeshProUGUI label_Score= default;
    [SerializeField]
    private GameObject[] gameObjects;

    private StringBuilder builder_Name;
    private StringBuilder builder_Score;
    private float elapsedTime;

    private void Start()
    {
        builder_Name = new StringBuilder();
        builder_Score = new StringBuilder();
        elapsedTime = 0f;
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

    }

    private void Update()
    {
        // �܂����[���ɎQ�����Ă��Ȃ��ꍇ�͍X�V���Ȃ�
        if (!PhotonNetwork.InRoom) { return; }

        // 0.1�b���Ƀe�L�X�g���X�V����
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 0.1f)
        {
            elapsedTime = 0f;
            UpdateLabel();
        }
    }

    private void UpdateLabel()
    {
        //�v���C���[���X�g�擾
        var players = PhotonNetwork.PlayerList;
        
        //�\�[�g
        Array.Sort(
            players,
            (p1, p2) =>
            {
                // �X�R�A���������Ƀ\�[�g����
                int diff = p2.GetScore() - p1.GetScore();
                if (diff != 0)
                {
                    return diff;
                }
                // �X�R�A�������������ꍇ�́AID�����������Ƀ\�[�g����
                return p1.ActorNumber - p2.ActorNumber;
            }
        );

        //������
        builder_Name.Clear();
        builder_Score.Clear();

        //�X�R�A���X�g���J�Ƃ��Ĉ�̃e�L�X�g��
        foreach (var player in players)
        {
            //builder_Name.AppendLine($"{player.NickName}({player.ActorNumber}) - ");
            builder_Score.AppendLine($"{player.GetScore()}");
        }
        label_Score.text = builder_Score.ToString();

        //�v���C���[���̃e�L�X�g�{�b�N�X�ɏ��Ԃɓ���Ă���
        for(int i = 0; i<players.Length && i<=10; ++i)
        {
            gameObjects[i].GetComponent<TextMeshProUGUI>().text = players[i].NickName;
        }
        //label_Name.text = builder_Name.ToString();


    }
}