using UnityEngine;
using Zenject;

public class StructureManager : MonoBehaviour
{
    [Inject] private Services services;

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
                    services.ResourcesManager.SpendResources(selectedStructure.structureCost.GetAmountOfResourcesForBuild());
                    services.PlacementManager.PlaceStructureOnTheMap(position, selectedStructure, CellType.Structure);
                }
            }
        }
    }

    public void Demolish(Vector2Int position)
    {
        services.PlacementManager.Demolish(position);
    }

    private bool CheckCost()
    {
        if (services.ResourcesManager.EnoughResources(selectedStructure.structureCost.GetAmountOfResourcesForBuild()))
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
        if (services.PlacementManager.GetNeighboursOfTypeFor(position, CellType.Road).Count <= 0)
        {
            Debug.Log("Must be placed near a road");
            return false;
        }
        return true;
    }

    private bool DefaultCheck(Vector2Int position)
    {
        if (services.PlacementManager.CheckIfPositionInBound(position) == false)
        {
            Debug.Log("This position is out of bounds");
            return false;
        }
        if (services.PlacementManager.CheckIfPositionIsFree(position) == false)
        {
            Debug.Log("This position is not EMPTY");
            return false;
        }
        return true;
    }
}
