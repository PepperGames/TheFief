using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public PlacementManager placementManager;

    public List<Vector3Int> temporaryPlacementPositions = new List<Vector3Int>();
    public List<Vector3Int> roadPositionsToRecheck = new List<Vector3Int>();

    private Vector3Int startPosition;
    private bool placementMode = false;

    public RoadFixer roadFixer;

    private void Start()
    {
        roadFixer = GetComponent<RoadFixer>();
    }

    public void PlaceRoad(Vector3Int position)
    {
        if (placementManager.ChecIfPositionInBound(position) == false)
            return;
        if (placementManager.ChecIfPositionIsFree(position) == false)
            return;

        if (placementMode == false)
        {

            temporaryPlacementPositions.Clear();
            roadPositionsToRecheck.Clear();

            placementMode = true;
            startPosition = position;


            temporaryPlacementPositions.Add(position);
            placementManager.PlaceTemporaryStructure(position, roadFixer.deadEnd, CellType.Road);
        }
        else
        {
            placementManager.RemoveAllTemporaryStructures();
            temporaryPlacementPositions.Clear();

            foreach (var positionToFix in roadPositionsToRecheck)
            {
                roadFixer.FixRoadAtPosition(placementManager, positionToFix);
            }

            roadPositionsToRecheck.Clear();

            temporaryPlacementPositions = placementManager.GetPathBetween(startPosition, position);

            foreach (var temporaryPosition in temporaryPlacementPositions)
            {
                if (placementManager.ChecIfPositionIsFree(temporaryPosition) == false)
                    continue;
                placementManager.PlaceTemporaryStructure(temporaryPosition, roadFixer.deadEnd, CellType.Road);
            }
        }
        FixRoadPrefabs();
    }

    private void FixRoadPrefabs()
    {
        foreach (var temporaryPosition in temporaryPlacementPositions)
        {
            roadFixer.FixRoadAtPosition(placementManager, temporaryPosition);
            var neighbours = placementManager.GetNeighbourTypesFor(temporaryPosition, CellType.Road);

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
            roadFixer.FixRoadAtPosition(placementManager, positionToFix);
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
        temporaryPlacementPositions.Clear();
        startPosition = Vector3Int.zero;

        Debug.Log("FinishPlacingRoad");
    }
}
