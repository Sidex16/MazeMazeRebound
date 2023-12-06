using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public int playerMoney;
        public int lastPlayedLevelIndex = 4;
        public int skinIndex = 0;
        public int teleportsCount = 1;
        public int hammersCount = 1;
        public int fingersCount = 1;
        public bool isFitrsPlay = true;

    }

    private static string filePath;

    static SaveManager()
    {
        filePath = Application.persistentDataPath + "/playerData.json";
    }

    public static PlayerData LoadPlayerData()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonUtility.FromJson<PlayerData>(jsonData);
        }
        return new PlayerData();
    }

    public static void SavePlayerData(PlayerData playerData)
    {
        string jsonData = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, jsonData);
    }

    public static void ClearAllData()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        else
        {

        }
    }
}
