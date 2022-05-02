using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RoadManager : MonoBehaviour
{
    [Inject] protected Services services;

    [SerializeField] private List<Vector2Int> roadsPositions = new List<Vector2Int>();

    [SerializeField] private List<Vector2Int> temporaryPlacementPositions = new List<Vector2Int>();
    public List<Vector2Int> roadPositionsToRecheck = new List<Vector2Int>();

    private Vector2Int startPosition;
    private bool placementMode = false;

    public void PlaceRoad(Vector2Int position)
    {
        if (services.PlacementManager.CheckIfPositionInBound(position) == false)
            return;
        if (services.PlacementManager.CheckIfPositionIsFree(position) == false)
            return;

        if (placementMode == false)
        {

            temporaryPlacementPositions.Clear();
            roadPositionsToRecheck.Clear();

            placementMode = true;
            startPosition = position;


            temporaryPlacementPositions.Add(position);
            services.PlacementManager.PlaceTemporaryObject(position, services.RoadFixer.deadEnd, CellType.Road);
        }
        else
        {
            services.PlacementManager.RemoveAllTemporaryStructures();
            temporaryPlacementPositions.Clear();

            foreach (var positionToFix in roadPositionsToRecheck)
            {
                services.RoadFixer.FixRoadAtPosition(positionToFix);
            }

            roadPositionsToRecheck.Clear();

            temporaryPlacementPositions = services.PlacementManager.GetPathBetween(startPosition, position);

            foreach (var temporaryPosition in temporaryPlacementPositions)
            {
                if (services.PlacementManager.CheckIfPositionIsFree(temporaryPosition) == false)
                    continue;
                services.PlacementManager.PlaceTemporaryObject(temporaryPosition, services.RoadFixer.deadEnd, CellType.Road);
            }
        }
        FixRoadPrefabs();
    }

    private void FixRoadPrefabs()
    {
        foreach (var temporaryPosition in temporaryPlacementPositions)
        {
            services.RoadFixer.FixRoadAtPosition(temporaryPosition);
            var neighbours = services.PlacementManager.GetNeighboursOfTypeFor(temporaryPosition, CellType.Road);

            foreach (var roadPosition in neighbours)
            {
                if (roadPositionsToRecheck.Contains(roadPosition) == false)
                {
                    roadPositionsToRecheck.Add(roadPosition);
                }
            }
        }
        foreach (var positionToFix in roadPositionsToRecheck)
        {
            services.RoadFixer.FixRoadAtPosition(positionToFix);
        }
    }

    private void FixRoadPrefabs(Vector2Int positions)
    {
        services.RoadFixer.FixRoadAtPosition(positions);
        var neighbours = services.PlacementManager.GetNeighboursOfTypeFor(positions, CellType.Road);

        roadPositionsToRecheck.Clear();

        foreach (var roadPosition in neighbours)
        {
            if (roadPositionsToRecheck.Contains(roadPosition) == false)
            {
                roadPositionsToRecheck.Add(roadPosition);
            }
        }
        foreach (var positionToFix in roadPositionsToRecheck)
        {
            services.RoadFixer.FixRoadAtPosition(positionToFix);
        }
    }

    public void FinishPlacingRoad()
    {
        placementMode = false;
        services.PlacementManager.AddTemporaryStructuresToStructureDictionary();
        if (temporaryPlacementPositions.Count > 0)
        {
            //AudioPlayer.instance.PlayPlacementSound();
        }
        roadsPositions.AddRange(temporaryPlacementPositions);
        temporaryPlacementPositions.Clear();
        startPosition = Vector2Int.zero;

        //Debug.Log("FinishPlacingRoad");
    }

    public Vector2Int GetRandomRoadPosition()
    {
        if (roadsPositions == null || roadsPositions.Count <= 0)
            return Vector2Int.zero;

        int randomIndex = UnityEngine.Random.Range(0, roadsPositions.Count);

        Vector2Int position = roadsPositions[randomIndex];
        return position;
    }

    public void Demolish(Vector2Int position)
    {
        services.PlacementManager.Demolish(position);
        roadsPositions.Remove(position);
        FixRoadPrefabs(position);
    }
}
