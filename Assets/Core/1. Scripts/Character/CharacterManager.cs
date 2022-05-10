using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private Sprite _defaultPortrait;

    [SerializeField] private List<Character> _characters;
    public List<Character> Characters => _characters;

    [Inject] private Services services;

    [SerializeField] private Character[] pedestrianPrefabs;

    public Action OnCharacterListChange;

    public void SpawnCharacter()
    {
        Vector2Int start = services.RoadManager.GetRandomRoadPosition();

        if (start != null)
        {
            Character character = Instantiate(GetRandomPedestrian(), new Vector3(start.x, start.y, 0), Quaternion.identity, transform);

            CharacterData characterData = new CharacterData(NameGenerator.GetRandomName(), _defaultPortrait);
            character.Initialize(characterData);

            Characters.Add(character);
            OnCharacterListChange?.Invoke();

            OnCharacterEventsSubscribe(character);

            services.AiDirector.SelectNewRandomPath(character.AiAgent);
        }
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
        _characters.Remove(character);
        character.OnDie -= RemoveCharacter;
    }
}
