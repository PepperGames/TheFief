using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RoadManager : MonoBehaviour
{
    [Inject] [SerializeField] private PlacementManager placementManager;
    [Inject] [SerializeField] private RoadFixer roadFixer;

    [SerializeField] private List<Vector2Int> roadsPositions = new List<Vector2Int>();

    [SerializeField] private List<Vector2Int> temporaryPlacementPositions = new List<Vector2Int>();
    public List<Vector2Int> roadPositionsToRecheck = new List<Vector2Int>();

    private Vector2Int startPosition;
    private bool placementMode = false;

    public void PlaceRoad(Vector2Int position)
    {
        if (placementManager.CheckIfPositionInBound(position) == false)
            return;
        if (placementManager.CheckIfPositionIsFree(position) == false)
            return;

        if (placementMode == false)
        {

            temporaryPlacementPositions.Clear();
            roadPositionsToRecheck.Clear();

            placementMode = true;
            startPosition = position;


            temporaryPlacementPositions.Add(position);
            placementManager.PlaceTemporaryObject(position, roadFixer.deadEnd, CellType.Road);
        }
        else
        {
            placementManager.RemoveAllTemporaryStructures();
            temporaryPlacementPositions.Clear();

            foreach (var positionToFix in roadPositionsToRecheck)
            {
                roadFixer.FixRoadAtPosition(positionToFix);
            }

            roadPositionsToRecheck.Clear();

            temporaryPlacementPositions = placementManager.GetPathBetween(startPosition, position);

            foreach (var temporaryPosition in temporaryPlacementPositions)
            {
                if (placementManager.CheckIfPositionIsFree(temporaryPosition) == false)
                    continue;
                placementManager.PlaceTemporaryObject(temporaryPosition, roadFixer.deadEnd, CellType.Road);
            }
        }
        FixRoadPrefabs();
    }

    private void FixRoadPrefabs()
    {
        foreach (var temporaryPosition in temporaryPlacementPositions)
        {
            roadFixer.FixRoadAtPosition(temporaryPosition);
            var neighbours = placementManager.GetNeighboursOfTypeFor(temporaryPosition, CellType.Road);

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
            roadFixer.FixRoadAtPosition(positionToFix);
        }
    }

    private void FixRoadPrefabs(Vector2Int positions)
    {
        roadFixer.FixRoadAtPosition(positions);
        var neighbours = placementManager.GetNeighboursOfTypeFor(positions, CellType.Road);

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
            roadFixer.FixRoadAtPosition(positionToFix);
        }
    }

    public void FinishPlacingRoad()
    {
        placementMode = false;
        placementManager.AddTemporaryStructuresToStructureDictionary();
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
        placementManager.Demolish(position);
        roadsPositions.Remove(position);
        FixRoadPrefabs(position);
    }
}
