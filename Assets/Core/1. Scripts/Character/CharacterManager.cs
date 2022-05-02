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


            character.aiAgent.OnReachedFinalPoint += SelectNewPath;
            SelectNewPath(character.aiAgent);
        }
    }

    private void SelectNewPath(AiAgent aiAgent)
    {
        Vector2Int start = new Vector2Int((int)aiAgent.transform.position.x, (int)aiAgent.transform.position.y);
        Vector2Int end = services.RoadManager.GetRandomRoadPosition();

        aiAgent.Initialize(services.AiDirector.GetPath(start, end));
    }

    private Character GetRandomPedestrian()
    {
        return pedestrianPrefabs[Random.Range(0, pedestrianPrefabs.Length)];
    }
}
