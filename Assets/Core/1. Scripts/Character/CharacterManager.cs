using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private List<Character> characters;

    [Inject] private Services services;

    [SerializeField] private Character[] pedestrianPrefabs;

    public void SpawnCharacter()
    {
        Vector2Int start = services.RoadManager.GetRandomRoadPosition();

        if (start != null)
        {
            Character character = Instantiate(GetRandomPedestrian(), new Vector3(start.x, start.y, 0), Quaternion.identity);
            characters.Add(character);

            character.AiAgent.OnReachedFinalPoint += services.AiDirector.SelectNewRandomPath;
            services.AiDirector.SelectNewRandomPath(character.AiAgent);
        }
    }

    private Character GetRandomPedestrian()
    {
        return pedestrianPrefabs[Random.Range(0, pedestrianPrefabs.Length)];
    }
}
