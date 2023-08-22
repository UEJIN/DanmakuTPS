using ExitGames.Client.Photon;
using Photon.Realtime;

public static class PlayerPropertiesExtensions
{
    private const string ScoreKey = "Score";
    private const string MessageKey = "Message";

    private const string shotLv_voltex_Key = "s_v";
    private const string shotLv_circle_Key = "s_c";
    private const string shotLv_random_Key = "s_r";

    private const string nowHP_Key = "n_h";
    private const string killCount_Key = "k";

    private static readonly Hashtable propsToSet = new Hashtable();

    // �v���C���[�̃X�R�A���擾����
    public static int GetScore(this Player player)
    {
        return (player.CustomProperties[ScoreKey] is int score) ? score : 0;
    }

    // �v���C���[�̃��b�Z�[�W���擾����
    public static string GetMessage(this Player player)
    {
        return (player.CustomProperties[MessageKey] is string message) ? message : string.Empty;
    }

    // �v���C���[�̃X�R�A��ݒ肷��
    public static void SetScore(this Player player, int score)
    {
        propsToSet[ScoreKey] = score;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    // �v���C���[�̃X�R�A��ǉ����Đݒ肷��
    public static void AddScore(this Player player, int addScore)
    {
        propsToSet[ScoreKey] = player.GetScore() + addScore;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    // �v���C���[�̃��b�Z�[�W��ݒ肷��
    public static void SetMessage(this Player player, string message)
    {
        propsToSet[MessageKey] = message;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    //// �v���C���[�̃J�X�^���v���p�e�B�𑗐M����
    //public static void SendPlayerProperties(this Player player)
    //{
    //    if (propsToSet.Count > 0)
    //    {
    //        player.SetCustomProperties(propsToSet);
    //        propsToSet.Clear();
    //    }
    //}
}