using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private List<Character> characters;

    [Inject] [SerializeField] private RoadManager roadManager;
    [Inject] [SerializeField] private AiDirector aiDirector;
    [SerializeField] private Character[] pedestrianPrefabs;

    public void SpawnCharacter()
    {
        Vector2Int start = roadManager.GetRandomRoadPosition();

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
        Vector2Int end = roadManager.GetRandomRoadPosition();

        aiAgent.Initialize(aiDirector.GetPath(start, end));
    }

    private Character GetRandomPedestrian()
    {
        return pedestrianPrefabs[Random.Range(0, pedestrianPrefabs.Length)];
    }
}
