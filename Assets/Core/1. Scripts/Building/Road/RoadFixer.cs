using System.Linq;
using UnityEngine;
using Zenject;

public class RoadFixer : MonoBehaviour
{
    [Inject] protected Services services;
 
    public Road deadEnd, roadStraight, corner, threeWay, fourWay;

    public void FixRoadAtPosition(Vector2Int temporaryPosition)
    {
        var result = services.PlacementManager.GetNeighbourTypesFor(temporaryPosition);
        int roadCount = 0;
        roadCount = result.Where(x => x == CellType.Road).Count();

        if (roadCount == 0 || roadCount == 1)
        {
            CreateDeadEnd(result, temporaryPosition);
        }
        else if (roadCount == 2)
        {
            if (CreateStraightRoad(result, temporaryPosition))
                return;
            CreateCorner(result, temporaryPosition);
        }
        else if (roadCount == 3)
        {
            CreateThreeWay(result, temporaryPosition);
        }
        else
        {
            CreateFourWay(result, temporaryPosition);
        }
    }

    private void CreateFourWay(CellType[] result, Vector2Int temporaryPosition)
    {
        services.PlacementManager.ModifyStructureModel(temporaryPosition, fourWay.modelView, Quaternion.identity);
    }

    private void CreateThreeWay(CellType[] result, Vector2Int temporaryPosition)
    {
        if (result[1] == CellType.Road && result[2] == CellType.Road && result[3] == CellType.Road)
        {
            services.PlacementManager.ModifyStructureModel(temporaryPosition, threeWay.modelView, Quaternion.Euler(0, 0, 0));
        }
        else if (result[2] == CellType.Road && result[3] == CellType.Road && result[0] == CellType.Road)
        {
            services.PlacementManager.ModifyStructureModel(temporaryPosition, threeWay.modelView, Quaternion.Euler(0, 0, 270));
        }
        else if (result[3] == CellType.Road && result[0] == CellType.Road && result[1] == CellType.Road)
        {
            services.PlacementManager.ModifyStructureModel(temporaryPosition, threeWay.modelView, Quaternion.Euler(0, 0, 180));
        }
        else if (result[0] == CellType.Road && result[1] == CellType.Road && result[2] == CellType.Road)
        {
            services.PlacementManager.ModifyStructureModel(temporaryPosition, threeWay.modelView, Quaternion.Euler(0, 0, 90));
        }
    }

    private void CreateCorner(CellType[] result, Vector2Int temporaryPosition)
    {
        if (result[1] == CellType.Road && result[2] == CellType.Road)
        {
            services.PlacementManager.ModifyStructureModel(temporaryPosition, corner.modelView, Quaternion.Euler(0, 0, 270));
        }
        else if (result[2] == CellType.Road && result[3] == CellType.Road)
        {
            services.PlacementManager.ModifyStructureModel(temporaryPosition, corner.modelView, Quaternion.Euler(0, 0, 180));
        }
        else if (result[3] == CellType.Road && result[0] == CellType.Road)
        {
            services.PlacementManager.ModifyStructureModel(temporaryPosition, corner.modelView, Quaternion.Euler(0, 0, 90));
        }
        else if (result[0] == CellType.Road && result[1] == CellType.Road)
        {
            services.PlacementManager.ModifyStructureModel(temporaryPosition, corner.modelView, Quaternion.Euler(0, 0, 0));
        }
    }

    private bool CreateStraightRoad(CellType[] result, Vector2Int temporaryPosition)
    {
        if (result[0] == CellType.Road && result[2] == CellType.Road)
        {
            services.PlacementManager.ModifyStructureModel(temporaryPosition, roadStraight.modelView, Quaternion.Euler(0, 0, 0));
            return true;
        }
        else if (result[1] == CellType.Road && result[3] == CellType.Road)
        {
            services.PlacementManager.ModifyStructureModel(temporaryPosition, roadStraight.modelView, Quaternion.Euler(0, 0, 90));
            return true;
        }
        return false;
    }

    private void CreateDeadEnd(CellType[] result, Vector2Int temporaryPosition)
    {
        if (result[1] == CellType.Road)
        {
            services.PlacementManager.ModifyStructureModel(temporaryPosition, deadEnd.modelView, Quaternion.Euler(0, 0, 90));
        }
        else if (result[2] == CellType.Road)
        {
            services.PlacementManager.ModifyStructureModel(temporaryPosition, deadEnd.modelView, Quaternion.Euler(0, 0, 0));
        }
        else if (result[3] == CellType.Road)
        {
            services.PlacementManager.ModifyStructureModel(temporaryPosition, deadEnd.modelView, Quaternion.Euler(0, 0, 270));
        }
        else if (result[0] == CellType.Road)
        {
            services.PlacementManager.ModifyStructureModel(temporaryPosition, deadEnd.modelView, Quaternion.Euler(0, 0, 180));
        }
    }
}
