using UnityEngine;

public class CharacterData
{
    [SerializeField] private string _characterName;
    [SerializeField] private Sprite _portrait;

    public string CharacterName => _characterName;
    public Sprite Portrait => _portrait;

    public CharacterData(string name, Sprite portrait)
    {
        _characterName = name;
        _portrait = portrait;
    }
}
