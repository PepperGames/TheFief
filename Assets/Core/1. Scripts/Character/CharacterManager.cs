using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private DeathPopup _deathPopupPrefab;

    [Inject] private Services services;

    [SerializeField] private PortraitGenerator _portraitGenerator;

    [SerializeField] private List<Character> _characters;

    [SerializeField] private Character[] pedestrianPrefabs;

    public List<Character> Characters => _characters;

    public Action OnCharacterListChange;

    public void SpawnCharacter()
    {
        Vector2Int start = services.RoadManager.GetRandomRoadPosition();

        if (start != null)
        {
            Character character = Instantiate(GetRandomPedestrian(), new Vector3(start.x, start.y, 0), Quaternion.identity, transform);

            CharacterData characterData = GenerateRandomCharacterData();
            character.Initialize(characterData);

            Characters.Add(character);
            OnCharacterListChange?.Invoke();

            OnCharacterEventsSubscribe(character);

            services.AiDirector.SelectNewRandomPath(character.AiAgent);
        }
    }

    private CharacterData GenerateRandomCharacterData()
    {
        string characterName = NameGenerator.GetRandomName();
        Age age = new Age();
        Genders gender = GenderGenerator.GetRandomGender();
        Sprite portrait = _portraitGenerator.GetPortrait(gender);
        Estates estates = EstatesGenerator.GetRandomEstates();

        return new CharacterData(characterName, portrait, age, gender, estates);
    }

    private Character GetRandomPedestrian()
    {
        return pedestrianPrefabs[Random.Range(0, pedestrianPrefabs.Length)];
    }

    private void OnCharacterEventsSubscribe(Character character)
    {
        character.AiAgent.OnReachedFinalPoint += services.AiDirector.SelectNewRandomPath;
        character.OnDie += RemoveCharacter;
    }

    private void RemoveCharacter(Character character)
    {
        DeathPopup deathPopup = services.UIController.AlertList.Create(_deathPopupPrefab.gameObject).GetComponent<DeathPopup>();
        deathPopup.Initialize(character.CharacterData.Portrait, character.CharacterData.CharacterName, character.CharacterData.Age.years);

        character.OnDie -= RemoveCharacter;
        character.AiAgent.OnReachedFinalPoint -= services.AiDirector.SelectNewRandomPath;
        _characters.Remove(character);
    }
}
