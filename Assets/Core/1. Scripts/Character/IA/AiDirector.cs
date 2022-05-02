using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class AiDirector : MonoBehaviour
{
    [Inject] protected Services services;

    public List<Vector2> GetPath(Vector2Int start, Vector2Int end, Action<bool> action = null)
    {
        var path = GetPathBetween(start, end);
        Debug.Log(path.Count);
        if (path.Count > 0)
        {
            path.Reverse();
            action?.Invoke(true);
            return new List<Vector2>(path.Select(x => (Vector2)x).ToList());
        }
        else
        {
            action?.Invoke(false);
            return null;
        }
    }

    public void SelectNewRandomPath(AiAgent aiAgent, Action<bool> action = null)
    {
        Vector2Int start = new Vector2Int((int)aiAgent.transform.position.x, (int)aiAgent.transform.position.y);
        Vector2Int end = services.RoadManager.GetRandomRoadPosition();

        List<Vector2> path = services.AiDirector.GetPath(start, end, action);
        if (path == null)
        {
            SelectNewRandomPath(aiAgent, action);//пока так
        }
        else
        {
            aiAgent.Initialize(path);
        }
    }


    internal List<Vector2Int> GetPathBetween(Vector2Int startPosition, Vector2Int endPosition)
    {
        var resultPath = GridSearch.AStarSearch(services.Grid, new Point(startPosition.x, startPosition.y), new Point(endPosition.x, endPosition.y), true);

        List<Vector2Int> path = new List<Vector2Int>();
        foreach (Point point in resultPath)
        {
            path.Add(new Vector2Int(point.X, point.Y));
        }
        return path;
    }
}
