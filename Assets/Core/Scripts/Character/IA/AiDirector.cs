using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class AiDirector : MonoBehaviour
{

    [Inject] [SerializeField] private PlacementManager placementManager;
    [SerializeField] private GameObject[] pedestrianPrefabs;

    public void SpawnAllAagents()
    {
        BasicStructure structure1 = placementManager.GetRandomStructure();
        BasicStructure structure2 = placementManager.GetRandomStructure();

        TrySpawningAnAgent(structure1, structure2);
    }

    private void TrySpawningAnAgent(BasicStructure startStructure, BasicStructure endStructure)
    {
        if (startStructure != null && endStructure != null)
        {
            var startPosition = new Vector2Int((int)startStructure.transform.position.x, (int)startStructure.transform.position.y);
            var endPosition = new Vector2Int((int)endStructure.transform.position.x, (int)endStructure.transform.position.y);
            var agent = Instantiate(GetRandomPedestrian(), new Vector3(startPosition.x, startPosition.y, 0), Quaternion.identity);
            //var path = placementManager.GetPathBetween(startPosition, endPosition, true);
            var path = placementManager.GetPathBetween(startPosition, endPosition);
            if (path.Count > 0)
            {
                path.Reverse();
                var aiAgent = agent.GetComponent<AiAgent>();
                aiAgent.Initialize(new List<Vector2>(path.Select(x => (Vector2)x).ToList()));
            }
        }
    }

    private GameObject GetRandomPedestrian()
    {
        return pedestrianPrefabs[Random.Range(0, pedestrianPrefabs.Length)];
    }
}
