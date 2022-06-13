using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class CharacterManager : MonoBehaviour
{
    [Inject] private Services services;

    [Header("Generators")]
    [SerializeField] private PortraitGenerator _portraitGenerator;
    [SerializeField] private CharacterTraitsGenerator _characterTraitsGenerator;

    [Header("Prefabs")]
    [SerializeField] private Character[] pedestrianPrefabs;

    [Header("Characters")]
    [SerializeField] private List<Character> _allCharacters;
    [SerializeField] private List<Character> _aliveCharacters;

    [Header("Characters by estate")]
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
        services.RoadManager.OnRoadPlaced += SpawnFirstCharacter;
    }

    private void SpawnFirstCharacter()
    {
        for (int i = 0; i < 5; i++)
        {
            CharacterData characterData = GenerateRandomCharacterData();
            SpawnComingCharacter(characterData);
        }
        services.RoadManager.OnRoadPlaced -= SpawnFirstCharacter;
    }

    private void SpawnRandomCharacter()
    {
        CharacterData characterData = GenerateRandomCharacterData();
        SpawnCharacter(characterData);
        services.UIController.AlertList.CreateNewCharacterInTownPopup(characterData);
    }

    private void SpawnCharacter(CharacterData characterData)
    {
        Vector2Int start = services.RoadManager.GetRandomRoadPosition();

        if (start != null)
        {
            Character character = Instantiate(GetRandomPedestrian(), new Vector3(start.x, start.y, 0), Quaternion.identity, transform);

            character.Initialize(characterData);
            character.CharacterTraitsManager.InitializeTrait(_characterTraitsGenerator.GetCharacterTraits(2));
            character.CharacterTraitsManager.InitializeTrait(_characterTraitsGenerator.GetBumTrait());

            AddCharacterToLists(character);

            OnCharacterEventsSubscribe(character);

            services.AiDirector.SelectNewRandomPath(character.AiAgent);
            services.HappinessManager.Recalculate(character.CharacterData.Estates);
            services.FaithManager.Recalculate();
        }
    }

    public void SpawnComingCharacter(CharacterData characterData)
    {
        SpawnCharacter(characterData);
        services.UIController.AlertList.CreateNewCharacterInTownPopup(characterData);
    }

    public void SpawnBornedCharacter(CharacterData characterData)
    {
        SpawnCharacter(characterData);
        services.UIController.AlertList.CreateBabyWasBornPopup(characterData.FamilyTies.mother.CharacterData, characterData.FamilyTies.father.CharacterData, characterData);
    }

    public CharacterData GenerateBornedCharacterData(Character mother, Character father)
    {
        string characterName = NameGenerator.GetRandomName();
        Age age = new Age(1);
        Genders gender = GenderGenerator.GetRandomGender();
        Sprite portrait = _portraitGenerator.GetPortrait(gender);
        Estates estates = father.CharacterData.Estates;
        Happiness happiness = new Happiness(65);
        Faith faith = new Faith();
        FamilyTies familyTies = new FamilyTies(mother, father);

        CharacterData characterData = new CharacterData(characterName, portrait, age, gender, estates, happiness, familyTies, faith);

        return characterData;
    }

    public CharacterData GenerateRandomCharacterData()
    {
        string characterName = NameGenerator.GetRandomName();
        Age age = new Age();
        Genders gender = GenderGenerator.GetRandomGender();
        Sprite portrait = _portraitGenerator.GetPortrait(gender);
        Estates estates = EstatesGenerator.GetRandomEstates();
        Happiness happiness = new Happiness();
        Faith faith = new Faith();
        FamilyTies familyTies = new FamilyTies();

        CharacterData characterData = new CharacterData(characterName, portrait, age, gender, estates, happiness, familyTies, faith);

        return characterData;
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

        character.CharacterData.Happiness.OnHappinessChange += services.HappinessManager.Recalculate;
        character.CharacterData.Faith.OnHappinessChange += services.FaithManager.Recalculate;
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
        character.CharacterData.Happiness.OnHappinessChange -= services.HappinessManager.Recalculate;
        character.CharacterData.Faith.OnHappinessChange -= services.FaithManager.Recalculate;

        RemoveCharacterFromAliveList(character);

        services.HappinessManager.Recalculate(character.CharacterData.Estates);
        services.FaithManager.Recalculate();
    }


    private void OnCharacterDie(Character character)
    {
        RemoveCharacter(character);
        services.UIController.AlertList.CreateDeathPopup(character);
    }

    private void OnCharacterLeaveFromTown(Character character)
    {
        RemoveCharacter(character);
        services.UIController.AlertList.CreateLeaveFromTownPopup(character);
    }
}
