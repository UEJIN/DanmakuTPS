using ExitGames.Client.Photon;
using Photon.Realtime;

public static class PlayerPropertiesExtensions
{
    private const string ScoreKey = "Score";
    private const string MessageKey = "Message";

    private const string shotLv_voltex_Key = "s_v";
    private const string shotLv_circle_Key = "s_c";
    private const string shotLv_random_Key = "s_r";
    private const string shotLv_aim_Key = "s_a";
    private const string UltID_Key = "u";

    private const string nowHP_Key = "n_h";
    private const string killCount_Key = "k";

    private static readonly Hashtable propsToSet = new Hashtable();

    
    #region GetStatus
    // プレイヤーのスコアを取得する
    public static int GetScore(this Player player)
    {
        return (player.CustomProperties[ScoreKey] is int score) ? score : 0;
    }

    // プレイヤーのメッセージを取得する
    public static string GetMessage(this Player player)
    {
        return (player.CustomProperties[MessageKey] is string message) ? message : string.Empty;
    }

    // プレイヤーのShotLv_voltexを取得する
    public static int GetShotLv_voltex(this Player player)
    {
        return (player.CustomProperties[shotLv_voltex_Key] is int shotLv_voltex) ? shotLv_voltex : 0;
    }

    // プレイヤーのShotLv_voltexを取得する
    public static int GetShotLv_circle(this Player player)
    {
        return (player.CustomProperties[shotLv_circle_Key] is int shotLv_circle) ? shotLv_circle : 0;
    }

    // プレイヤーのShotLv_voltexを取得する
    public static int GetShotLv_random(this Player player)
    {
        return (player.CustomProperties[shotLv_random_Key] is int shotLv_random) ? shotLv_random : 0;
    }

    // プレイヤーのShotLv_voltexを取得する
    public static int GetShotLv_aim(this Player player)
    {
        return (player.CustomProperties[shotLv_aim_Key] is int shotLv_aim) ? shotLv_aim : 0;
    }

    // プレイヤーのUltIDを取得する
    public static int GetUltID(this Player player)
    {
        return (player.CustomProperties[UltID_Key] is int ultID) ? ultID : 0;
    }

    // プレイヤーのNowHPを取得する
    public static float GetNowHP(this Player player)
    {
        return (player.CustomProperties[nowHP_Key] is float nowHP) ? nowHP : 100;
    }

    // プレイヤーのKillCountを取得する
    public static int GetKillCount(this Player player)
    {
        return (player.CustomProperties[killCount_Key] is int killCount) ? killCount : 0;
    }
    #endregion

    //-------------------------------------------------------------------------------------------------

    #region SetStatus
    // プレイヤーのスコアを設定する
    public static void SetScore(this Player player, int score)
    {
        propsToSet[ScoreKey] = score;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    // プレイヤーのスコアを追加して設定する
    public static void AddScore(this Player player, int addScore)
    {
        propsToSet[ScoreKey] = player.GetScore() + addScore;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    // プレイヤーのスコアを追加して設定する
    public static void AddShotLv_voltex(this Player player, int addShotLv_voltex)
    {
        propsToSet[shotLv_voltex_Key] = player.GetShotLv_voltex() + addShotLv_voltex;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    // プレイヤーのスコアを追加して設定する
    public static void AddShotLv_circle(this Player player, int addShotLv_circle)
    {
        propsToSet[shotLv_circle_Key] = player.GetShotLv_circle() + addShotLv_circle;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    // プレイヤーのスコアを追加して設定する
    public static void AddShotLv_random(this Player player, int addShotLv_random)
    {
        propsToSet[shotLv_random_Key] = player.GetShotLv_random() + addShotLv_random;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    // プレイヤーのスコアを追加して設定する
    public static void AddShotLv_aim(this Player player, int addShotLv_aim)
    {
        propsToSet[shotLv_aim_Key] = player.GetShotLv_aim() + addShotLv_aim;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    // プレイヤーのUltIDを設定する
    public static void SetUltID(this Player player, int setUltID)
    {
        propsToSet[UltID_Key] = setUltID;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    // プレイヤーのスコアを追加して設定する
    public static void AddNowHP(this Player player, float addNowHP)
    {
        propsToSet[nowHP_Key] = player.GetNowHP() + addNowHP;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    // プレイヤーのスコアを追加して設定する
    public static void SetNowHP(this Player player, float setNowHP)
    {
        propsToSet[nowHP_Key] = setNowHP;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    // プレイヤーのスコアを追加して設定する
    public static void AddKillCount(this Player player, int addKillCount)
    {
        propsToSet[killCount_Key] = player.GetKillCount() + addKillCount;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    // プレイヤーのメッセージを設定する
    public static void SetMessage(this Player player, string message)
    {
        propsToSet[MessageKey] = message;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }
    #endregion

    //// プレイヤーのカスタムプロパティを送信する
    //public static void SendPlayerProperties(this Player player)
    //{
    //    if (propsToSet.Count > 0)
    //    {
    //        player.SetCustomProperties(propsToSet);
    //        propsToSet.Clear();
    //    }
    //}
}