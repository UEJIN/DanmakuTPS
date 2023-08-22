using System;
using System.Text;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{

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
    }

    private void Update()
    {
        // まだルームに参加していない場合は更新しない
        if (!PhotonNetwork.InRoom) { return; }

        // 0.1秒毎にテキストを更新する
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 0.1f)
        {
            elapsedTime = 0f;
            UpdateLabel();
        }
    }

    private void UpdateLabel()
    {
        var players = PhotonNetwork.PlayerList;
        Array.Sort(
            players,
            (p1, p2) =>
            {
                // スコアが多い順にソートする
                int diff = p2.GetScore() - p1.GetScore();
                if (diff != 0)
                {
                    return diff;
                }
                // スコアが同じだった場合は、IDが小さい順にソートする
                return p1.ActorNumber - p2.ActorNumber;
            }
        );

        builder_Name.Clear();
        builder_Score.Clear();

        foreach (var player in players)
        {
            //builder_Name.AppendLine($"{player.NickName}({player.ActorNumber}) - ");
            builder_Score.AppendLine($"{player.GetScore()}");
        }

        for(int i = 0; i<players.Length;++i)
        {
            gameObjects[i].GetComponent<TextMeshProUGUI>().text = players[i].NickName;
        }



        //label_Name.text = builder_Name.ToString();
        label_Score.text = builder_Score.ToString();

    }
}