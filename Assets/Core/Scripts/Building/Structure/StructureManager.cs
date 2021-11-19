using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class StructureManager : MonoBehaviour
{
    [Inject] [SerializeField] private PlacementManager placementManager;
    [Inject] [SerializeField] private ResourcesManager resourcesManager;

    [SerializeField] private Structure selectedStructure;

    public void SetSelectedStructure(Structure newSelectedStructure)
    {
        if (newSelectedStructure != null)
        {
            selectedStructure = newSelectedStructure;
        }
    }

    public void PlaceHouse(Vector2Int position)
    {
        if (selectedStructure != null)
        {
            if (CheckCost())
            {
                if (CheckStructurePosition(position, selectedStructure))
                {
                    resourcesManager.SpendResources(selectedStructure.structureCost.GetAmountOfResourcesForBuild());
                    placementManager.PlaceStructureOnTheMap(position, selectedStructure, CellType.Structure);
                }
            }
        }
    }

    private bool CheckCost()
    {
        if (resourcesManager.EnoughResources(selectedStructure.structureCost.GetAmountOfResourcesForBuild()))
        {
            return true;
        }
        Debug.Log("Not enough resources");
        return false;
    }

    private bool CheckStructurePosition(Vector2Int position, Structure structure)
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
