using System.Text.RegularExpressions;
using UnityEngine;

public class NameGenerator : MonoBehaviour
{
    private static string[] _nicknames;

    private void Awake()
    {
        TextAsset rawDataAsset = (TextAsset)UnityEngine.Resources.Load("Data/bot_nicknames", typeof(TextAsset));
        string rawData = rawDataAsset.text;

        _nicknames = Regex.Split(rawData, @"[_\s\n]+");
    }

    public static string GetRandomName()
    {
        string nickName = _nicknames[Random.Range(0, _nicknames.Length)];
        return nickName;
    }
}

