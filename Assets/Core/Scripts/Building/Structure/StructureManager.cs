using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    public PlacementManager placementManager;

    public Structure housesPrefab, specialPrefab, bigStructuresPrefab;

    public void PlaceHouse(Vector2Int position/*, Structure structure*/)
    {
        if (CheckBigStructure(position, housesPrefab))
        {
            placementManager.PlaceObjectOnTheMap(position, housesPrefab, CellType.Structure);
        }
    }

    internal void PlaceBigStructure(Vector2Int position/*, Structure structure*/)
    {
        if (CheckBigStructure(position, bigStructuresPrefab))
        {
            placementManager.PlaceObjectOnTheMap(position, bigStructuresPrefab, CellType.Structure);
        }
    }

    private bool CheckBigStructure(Vector2Int position, Structure structure)
    {
        bool nearRoad = false;
        foreach (Vector2Int item in structure.Points)
        {
            var newPosition = position + new Vector2Int(item.x, item.y);

            if (DefaultCheck(newPosition) == false)
            {
                return false;
            }

            if (nearRoad == false)
            {
                nearRoad = RoadCheck(newPosition);
            }
        }

        return nearRoad;
    }

    public void PlaceSpecial(Vector2Int position/*, Structure structure*/)
    {
        if (CheckBigStructure(position, specialPrefab))
        {
            placementManager.PlaceObjectOnTheMap(position, specialPrefab, CellType.Structure);
        }

    }

    private bool CheckPositionBeforePlacement(Vector2Int position)
    {
        if (DefaultCheck(position) == false)
        {
            return false;
        }
        if (RoadCheck(position) == false)
        {
            return false;
        }
        return true;

    }

    private bool RoadCheck(Vector2Int position)
    {
        if (placementManager.GetNeighboursOfTypeFor(position, CellType.Road).Count <= 0)
        {
            Debug.Log("Must be placed near a road");
            return false;
        }
        return true;
    }

    private bool DefaultCheck(Vector2Int position)
    {
        if (placementManager.CheckIfPositionInBound(position) == false)
        {
            Debug.Log("This position is out of bounds");
            return false;
        }
        if (placementManager.CheckIfPositionIsFree(position) == false)
        {
            Debug.Log("This position is not EMPTY");
            return false;
        }
        return true;
    }
}
