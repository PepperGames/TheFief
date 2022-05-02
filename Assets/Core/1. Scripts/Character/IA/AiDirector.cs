using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class AiDirector : MonoBehaviour
{
    [Inject] protected Services services;

    public List<Vector2> GetPath(Vector2Int start, Vector2Int end)
    {
        var path = services.PlacementManager.GetPathBetween(start, end);
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
