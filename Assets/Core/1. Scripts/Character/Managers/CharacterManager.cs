using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private DeathPopup _deathPopupPrefab;
    [SerializeField] private NewCharacterInTownPopup _newCharacterInTownPopupPrefabs;
    [SerializeField] private LeaveFromTownPopup _leaveFromTownPopupPrefabs;

    [Inject] private Services services;

    [SerializeField] private PortraitGenerator _portraitGenerator;

    [SerializeField] private Character[] pedestrianPrefabs;

    [SerializeField] private List<Character> _allCharacters;
    [SerializeField] private List<Character> _aliveCharacters;

    [SerializeField] private List<Character> _peasantsCharacters;
    [SerializeField] private List<Character> _merchantsCharacters;
    [SerializeField] private List<Character> _priestsCharacters;
    [SerializeField] private List<Character> _peersCharacters;

    public List<Character> AllCharacters => _allCharacters;
    public List<Character> AliveCharacters => _aliveCharacters;
    public List<Character> PeasantsCharacters => _peasantsCharacters;
    public List<Character> MerchantsCharacters => _merchantsCharacters;
    public List<Character> PriestsCharacters => _priestsCharacters;
    public List<Character> PeersCharacters => _peersCharacters;

    public Action OnCharacterListChange;

    private void Start()
    {
        StartCoroutine(Test());
    }

    public void SpawnRandomCharacter()
    {
        CharacterData characterData = GenerateRandomCharacterData();
        SpawnCharacter(characterData);
    }

    public void SpawnCharacter(CharacterData characterData)
    {
        Vector2Int start = services.RoadManager.GetRandomRoadPosition();

        if (start != null)
        {
            Character character = Instantiate(GetRandomPedestrian(), new Vector3(start.x, start.y, 0), Quaternion.identity, transform);

            character.Initialize(characterData);

            AddCharacterToLists(character);

            OnCharacterEventsSubscribe(character);

            services.AiDirector.SelectNewRandomPath(character.AiAgent);

            CreateNewCharacterInTownPopup(characterData);
        }
    }

    public CharacterData GenerateRandomCharacterData(Character mother, Character father)
    {
        string characterName = NameGenerator.GetRandomName();
        Age age = new Age();
        Genders gender = GenderGenerator.GetRandomGender();
        Sprite portrait = _portraitGenerator.GetPortrait(gender);
        Estates estates = EstatesGenerator.GetRandomEstates();
        Happiness happiness = new Happiness();
        FamilyTies familyTies = new FamilyTies(mother, father);

        return new CharacterData(characterName, portrait, age, gender, estates, happiness, familyTies);
    }

    public CharacterData GenerateRandomCharacterData()
    {
        string characterName = NameGenerator.GetRandomName();
        Age age = new Age();
        Genders gender = GenderGenerator.GetRandomGender();
        Sprite portrait = _portraitGenerator.GetPortrait(gender);
        Estates estates = EstatesGenerator.GetRandomEstates();
        Happiness happiness = new Happiness();
        FamilyTies familyTies = new FamilyTies();

        return new CharacterData(characterName, portrait, age, gender, estates, happiness, familyTies);
    }

    private Character GetRandomPedestrian()
    {
        return pedestrianPrefabs[Random.Range(0, pedestrianPrefabs.Length)];
    }

    private void OnCharacterEventsSubscribe(Character character)
    {
        character.AiAgent.OnReachedFinalPoint += services.AiDirector.SelectNewRandomPath;

        character.OnDie += OnCharacterDie;
        character.OnLeaveFromTown += OnCharacterLeaveFromTown;
    }

    private void AddCharacterToLists(Character character)
    {
        AllCharacters.Add(character);
        AliveCharacters.Add(character);

        switch (character.CharacterData.Estates)
        {
            case Estates.Peasants:
                PeasantsCharacters.Add(character);
                break;
            case Estates.Merchants:
                MerchantsCharacters.Add(character);
                break;
            case Estates.Priests:
                PriestsCharacters.Add(character);
                break;
            case Estates.Peers:
                PeersCharacters.Add(character);
                break;
            default:
                Debug.LogError("Hey");
                break;
        }

        OnCharacterListChange?.Invoke();
    }

    private void RemoveCharacterFromAliveList(Character character)
    {
        AliveCharacters.Remove(character);

        switch (character.CharacterData.Estates)
        {
            case Estates.Peasants:
                PeasantsCharacters.Remove(character);
                break;
            case Estates.Merchants:
                MerchantsCharacters.Remove(character);
                break;
            case Estates.Priests:
                PriestsCharacters.Remove(character);
                break;
            case Estates.Peers:
                PeersCharacters.Remove(character);
                break;
            default:
                Debug.LogError("Hey");
                break;
        }

        OnCharacterListChange?.Invoke();
    }

    private void RemoveCharacter(Character character)
    {
        character.AiAgent.OnReachedFinalPoint -= services.AiDirector.SelectNewRandomPath;

        character.OnDie -= OnCharacterDie;
        character.OnLeaveFromTown -= OnCharacterLeaveFromTown;

        RemoveCharacterFromAliveList(character);
    }

    private void CreateNewCharacterInTownPopup(CharacterData characterData)
    {
        NewCharacterInTownPopup newCharacterInTownPopup = services.UIController.AlertList.Create(_newCharacterInTownPopupPrefabs.gameObject).GetComponent<NewCharacterInTownPopup>();
        newCharacterInTownPopup.Initialize(characterData);
    }

    private void CreateDeathPopup(Character character)
    {
        DeathPopup deathPopup = services.UIController.AlertList.Create(_deathPopupPrefab.gameObject).GetComponent<DeathPopup>();
        deathPopup.Initialize(character.CharacterData.Portrait, character.CharacterData.CharacterName, character.CharacterData.Age.years);
    }

    private void CreateLeaveFromTownPopup(Character character)
    {
        LeaveFromTownPopup leaveFromTownPopup = services.UIController.AlertList.Create(_leaveFromTownPopupPrefabs.gameObject).GetComponent<LeaveFromTownPopup>();
        leaveFromTownPopup.Initialize(character.CharacterData.Portrait, character.CharacterData.CharacterName);
    }

    private void OnCharacterDie(Character character)
    {
        RemoveCharacter(character);
        CreateDeathPopup(character);
    }

    private void OnCharacterLeaveFromTown(Character character)
    {
        RemoveCharacter(character);
        CreateLeaveFromTownPopup(character);
    }

    private System.Collections.IEnumerator Test()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            for (int i = 0; i < AliveCharacters.Count; i++)
            {
                AliveCharacters[i].CharacterData.Happiness.IndexOfHappiness = 10;
            }
        }
    }
}
