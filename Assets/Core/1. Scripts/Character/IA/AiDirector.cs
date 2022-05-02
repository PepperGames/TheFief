using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class AiDirector : MonoBehaviour
{
    [Inject] [SerializeField] private PlacementManager placementManager;

    public List<Vector2> GetPath(Vector2Int start, Vector2Int end)
    {
        var path = placementManager.GetPathBetween(start, end);
        if (path.Count > 0)
        {
            path.Reverse();
            return new List<Vector2>(path.Select(x => (Vector2)x).ToList());
        }
        else
        {
            return GetPath(start, end);
        }
    }
}
