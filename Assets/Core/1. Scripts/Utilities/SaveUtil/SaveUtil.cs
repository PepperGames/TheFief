using System.IO;
using UnityEngine;

namespace Utils
{
    public static class SaveUtil
    {
        //public const string Levels = "Levels";

        public static void SaveToPlayerPrefs<T>(T data, string fileName) where T : struct
        {
            PlayerPrefs.SetString(fileName, JsonUtility.ToJson(data));
        }

        public static T LoadFromPlayerPrefs<T>(string fileName) where T : struct
        {
            if (PlayerPrefs.HasKey(fileName))
            {
                string result = PlayerPrefs.GetString(fileName);
                return JsonUtility.FromJson<T>(result);
            }
            return default(T);
        }

        public static bool IsExist(string fileName)
        {
            if (PlayerPrefs.HasKey(fileName))
            {
                return true;
            }
            return false;
        }

        public static void SaveToStreamingAssets<T>(T data, string fileTitle)
        {
            var path = Path.Combine(Application.streamingAssetsPath, fileTitle);

            File.WriteAllText(path, JsonUtility.ToJson(data));
        }

        public static T LoadFromStreamingAssets<T>(string fileTitle)
        {
            var path = Path.Combine(Application.streamingAssetsPath, fileTitle);

            if (File.Exists(path))
            {
                var text = File.ReadAllText(path);
                return JsonUtility.FromJson<T>(text);
            }

            return default(T);
        }
    }
}