using UnityEngine;

public class CharacterData
{
    [SerializeField] private string _characterName;
    [SerializeField] private Sprite _portrait;
    [SerializeField] private Age _age;
    [SerializeField] private Genders _gender;
    [SerializeField] private Estates _estates;
    [SerializeField] private Happiness _happiness;

    public string CharacterName => _characterName;
    public Sprite Portrait => _portrait;
    public Age Age => _age;
    public Genders Gender => _gender;
    public Estates Estates => _estates;
    public Happiness Happiness => _happiness;

    public CharacterData(string name, Sprite portrait, Age age, Genders genders, Estates estates, Happiness happiness)
    {
        _characterName = name;
        _portrait = portrait;
        _age = age;
        _gender = genders;
        _estates = estates;
        _happiness = happiness;
    }
}
