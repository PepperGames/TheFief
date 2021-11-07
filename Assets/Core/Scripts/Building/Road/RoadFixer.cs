using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoadFixer : MonoBehaviour
{
    public GameObject deadEnd, roadStraight, corner, threeWay, fourWay;

    public void FixRoadAtPosition(PlacementManager placementManager, Vector2Int temporaryPosition)
    {
        var result = placementManager.GetNeighbourTypesFor(temporaryPosition);
        int roadCount = 0;
        roadCount = result.Where(x => x == CellType.Road).Count();

        if (roadCount == 0 || roadCount == 1)
        {
            CreateDeadEnd(placementManager, result, temporaryPosition);
        }
        else if (roadCount == 2)
        {
            if (CreateStraightRoad(placementManager, result, temporaryPosition))
                return;
            CreateCorner(placementManager, result, temporaryPosition);
        }
        else if (roadCount == 3)
        {
            CreateThreeWay(placementManager, result, temporaryPosition);
        }
        else
        {
            CreateFourWay(placementManager, result, temporaryPosition);
        }
    }

    private void CreateFourWay(PlacementManager placementManager, CellType[] result, Vector2Int temporaryPosition)
    {
        placementManager.ModifyStructureModel(temporaryPosition, fourWay, Quaternion.identity);
    }

    private void CreateThreeWay(PlacementManager placementManager, CellType[] result, Vector2Int temporaryPosition)
    {
        if (result[1] == CellType.Road && result[2] == CellType.Road && result[3] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, threeWay, Quaternion.Euler(0, 0, 0));
        }
        else if (result[2] == CellType.Road && result[3] == CellType.Road && result[0] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, threeWay, Quaternion.Euler(0, 0, 270));
        }
        else if (result[3] == CellType.Road && result[0] == CellType.Road && result[1] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, threeWay, Quaternion.Euler(0, 0, 180));
        }
        else if (result[0] == CellType.Road && result[1] == CellType.Road && result[2] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, threeWay, Quaternion.Euler(0, 0, 90));
        }
    }

    private void CreateCorner(PlacementManager placementManager, CellType[] result, Vector2Int temporaryPosition)
    {
        if (result[1] == CellType.Road && result[2] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, corner, Quaternion.Euler(0, 0, 270));
        }
        else if (result[2] == CellType.Road && result[3] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, corner, Quaternion.Euler(0, 0, 180));
        }
        else if (result[3] == CellType.Road && result[0] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, corner, Quaternion.Euler(0, 0, 90));
        }
        else if (result[0] == CellType.Road && result[1] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, corner, Quaternion.Euler(0, 0, 0));
        }
    }

    private bool CreateStraightRoad(PlacementManager placementManager, CellType[] result, Vector2Int temporaryPosition)
    {
        if (result[0] == CellType.Road && result[2] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, roadStraight, Quaternion.Euler(0, 0, 0));
            return true;
        }
        else if (result[1] == CellType.Road && result[3] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, roadStraight, Quaternion.Euler(0, 0, 90));
            return true;
        }
        return false;
    }

    private void CreateDeadEnd(PlacementManager placementManager, CellType[] result, Vector2Int temporaryPosition)
    {
        if (result[1] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, deadEnd, Quaternion.Euler(0, 0, 90));
        }
        else if (result[2] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, deadEnd, Quaternion.Euler(0, 0, 0));
        }
        else if (result[3] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, deadEnd, Quaternion.Euler(0, 0, 270));
        }
        else if (result[0] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, deadEnd, Quaternion.Euler(0, 0, 180));
        }
    }
}
