using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public int width;
    public int height;

    private Grid placementGrid;

    private Dictionary<Vector3Int, StructureModel> temporaryRoadObject = new Dictionary<Vector3Int, StructureModel>();
    private Dictionary<Vector3Int, StructureModel> structureDictionary = new Dictionary<Vector3Int, StructureModel>();

    private void Start()
    {
        placementGrid = new Grid(width, height);
    }

    internal CellType[] GetNeighbourTypesFor(Vector3Int temporaryPosition)
    {
        return placementGrid.GetAllAdjacentCellTypes(temporaryPosition.x, temporaryPosition.y);
    }

    internal List<Vector3Int> GetNeighboursOfTypeFor(Vector3Int position, CellType type)
    {
        var neighbourVerticles = placementGrid.GetAdjacentCellsOfType(position.x, position.y, type);

        List<Vector3Int> neighbours = new List<Vector3Int>();

        foreach (var point in neighbourVerticles)
        {
            neighbours.Add(new Vector3Int(point.X, point.Y, 0));
        }
        return neighbours;
    }

    internal void PlaceObjectOnTheMap(Vector3Int position, GameObject structurePrefab, CellType type, int width = 1, int height = 1)
    {
        //placementGrid[position.x, position.y] = type;
        //StructureModel structure = CreateANewStructureModel(position, structurePrefab, type);
        //structureDictionary.Add(position, structure);

        StructureModel structure = CreateANewStructureModel(position, structurePrefab, type);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var newPosition = position + new Vector3Int(x, y, 0);
                placementGrid[newPosition.x, newPosition.y] = type;
                structureDictionary.Add(newPosition, structure);
                DestroyNatureAt(newPosition);
            }
        }
    }

    internal bool CheckIfPositionInBound(Vector3Int position)
    {
        if (position.x >= 0 && position.x <= width && position.y >= 0 && position.y < height)
        {
            return true;
        }
        return false;
    }

    internal bool CheckIfPositionIsFree(Vector3Int position)
    {
        return CheckIfPositionIsOfType(position, CellType.Empty);
    }

    internal List<Vector3Int> GetPathBetween(Vector3Int startPosition, Vector3Int endPosition)
    {
        var resultPath = GridSearch.AStarSearch(placementGrid, new Point(startPosition.x, startPosition.y), new Point(endPosition.x, endPosition.y));

        List<Vector3Int> path = new List<Vector3Int>();
        foreach (Point point in resultPath)
        {
            path.Add(new Vector3Int(point.X, point.Y, 0));
        }
        return path;
    }

    internal void RemoveAllTemporaryStructures()
    {
        foreach (var structure in temporaryRoadObject.Values)
        {
            var position = Vector3Int.RoundToInt(structure.transform.position);
            placementGrid[position.x, position.y] = CellType.Empty;
            Destroy(structure.gameObject);
        }
        temporaryRoadObject.Clear();
    }

    internal void AddTemporaryStructuresToStructureDictionary()
    {
        foreach (var structure in temporaryRoadObject)
        {
            structureDictionary.Add(structure.Key, structure.Value);
            DestroyNatureAt(structure.Key);
        }
        temporaryRoadObject.Clear();
    }

    private void DestroyNatureAt(Vector3Int position)
    {
        //Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, groundMask);
        //if (hit.collider != null)
        //{
        //    Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
        //    return positionInt;
        //}
        //return null;

        RaycastHit2D[] hits = Physics2D.BoxCastAll((Vector2Int)position, new Vector3(0.5f, 0.5f),0, Vector2.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("Nature"));
        foreach (var item in hits)
        {
            Destroy(item.collider.gameObject);
        }
    }

    private bool CheckIfPositionIsOfType(Vector3Int position, CellType type)
    {
        return placementGrid[position.x, position.y] == type;
    }

    internal void PlaceTemporaryStructure(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        placementGrid[position.x, position.y] = type;
        StructureModel structure = CreateANewStructureModel(position, structurePrefab, type);
        temporaryRoadObject.Add(position, structure);
    }

    private StructureModel CreateANewStructureModel(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        GameObject structure = new GameObject(type.ToString());
        structure.transform.SetParent(transform);
        structure.transform.localPosition = position;
        var structureModel = structure.AddComponent<StructureModel>();
        structureModel.CreateModel(structurePrefab);

        return structureModel;
    }

    public void ModifyStructureModel(Vector3Int position, GameObject newModel, Quaternion rotation)
    {
        if (temporaryRoadObject.ContainsKey(position))
        {
            temporaryRoadObject[position].SwapModel(newModel, rotation);
        }
        else if (structureDictionary.ContainsKey(position))
        {
            structureDictionary[position].SwapModel(newModel, rotation);
        } 
    }
}
