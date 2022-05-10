using UnityEngine;

public class CharacterData
{
    [SerializeField] private string _characterName;
    [SerializeField] private Sprite _portrait;
    [SerializeField] private Age _age;
    [SerializeField] private Genders _gender;
    [SerializeField] private Estates _estates;

    public string CharacterName => _characterName;
    public Sprite Portrait => _portrait;
    public Age Age => _age;
    public Genders Gender => _gender;
    public Estates Estates => _estates;

    public CharacterData(string name, Sprite portrait, Age age, Genders genders, Estates estates)
    {
        _characterName = name;
        _portrait = portrait;
        _age = age;
        _gender = genders;
        _estates = estates;
    }
}
