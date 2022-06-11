using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StructureManager : MonoBehaviour
{
    [Inject] private Services services;

    [SerializeField] private Structure selectedStructure;

    [SerializeField] public List<Farm> farmList = new List<Farm>();
    [SerializeField] public List<Mine> mineList = new List<Mine>();
    [SerializeField] public List<Sawmill> sawmillList = new List<Sawmill>();
    [SerializeField] public List<StonePit> stonePitList = new List<StonePit>();
    [SerializeField] public List<Church> churchList = new List<Church>();
    [SerializeField] public List<Market> marketList = new List<Market>();
    [SerializeField] public List<WoodenHut> woodenHutList = new List<WoodenHut>();

    public Action OnPlaceHouse;

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
                    services.ResourcesManager.SpendResources(selectedStructure.StructureCost.GetAmountOfResourcesForBuild());
                    Structure structure = services.PlacementManager.PlaceStructureOnTheMap(position, selectedStructure, CellType.Structure) as Structure;
                    AddToStructuserList(structure);
                    OnPlaceHouse?.Invoke();
                    selectedStructure = null;
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
        if (services.ResourcesManager.EnoughResources(selectedStructure.StructureCost.GetAmountOfResourcesForBuild()))
        {
            return true;
        }
        Debug.Log("Not enough resources");
        CursorManager.Instance.SetErrorcursor();
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
            CursorManager.Instance.SetErrorcursor();
            return false;
        }
        return true;
    }

    private bool DefaultCheck(Vector2Int position)
    {
        if (services.PlacementManager.CheckIfPositionInBound(position) == false)
        {
            Debug.Log("This position is out of bounds");
            CursorManager.Instance.SetErrorcursor();
            return false;
        }
        if (services.PlacementManager.CheckIfPositionIsFree(position) == false)
        {
            Debug.Log("This position is not EMPTY");
            CursorManager.Instance.SetErrorcursor();
            return false;
        }
        return true;
    }


    private void AddToStructuserList(Structure basicStructure)
    {
        switch (basicStructure.StructireName)
        {
            case StructireName.Farm:
                farmList.Add(basicStructure as Farm);
                break;
            case StructireName.Mine:
                mineList.Add(basicStructure as Mine);
                break;
            case StructireName.Sawmill:
                sawmillList.Add(basicStructure as Sawmill);
                break;
            case StructireName.StonePit:
                stonePitList.Add(basicStructure as StonePit);
                break;
            case StructireName.Church:
                churchList.Add(basicStructure as Church);
                break;
            case StructireName.Market:
                marketList.Add(basicStructure as Market);
                break;
            case StructireName.WoodenHut:
                woodenHutList.Add(basicStructure as WoodenHut);
                break;
            default:
                break;
        }
    }
}
