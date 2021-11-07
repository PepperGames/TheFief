using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public int width;
    public int height;

    private Grid placementGrid;

    private Dictionary<Vector2Int, StructureModel> temporaryRoadObject = new Dictionary<Vector2Int, StructureModel>();
    private Dictionary<Vector2Int, StructureModel> structureDictionary = new Dictionary<Vector2Int, StructureModel>();

    private void Start()
    {
        placementGrid = new Grid(width, height);
    }

    internal CellType[] GetNeighbourTypesFor(Vector2Int temporaryPosition)
    {
        return placementGrid.GetAllAdjacentCellTypes(temporaryPosition.x, temporaryPosition.y);
    }

    internal List<Vector2Int> GetNeighboursOfTypeFor(Vector2Int position, CellType type)
    {
        var neighbourVerticles = placementGrid.GetAdjacentCellsOfType(position.x, position.y, type);

        List<Vector2Int> neighbours = new List<Vector2Int>();

        foreach (var point in neighbourVerticles)
        {
            neighbours.Add(new Vector2Int(point.X, point.Y));
        }
        return neighbours;
    }

    internal void PlaceObjectOnTheMap(Vector2Int position, Structure structure, CellType type)
    {
        StructureModel structureModel = CreateANewStructureModel(position, structure.prefab, type);

        foreach (Vector2Int item in structure.Points)
        {
            var newPosition = position + new Vector2Int(item.x, item.y);
            placementGrid[newPosition.x, newPosition.y] = type;
            structureDictionary.Add(newPosition, structureModel);
            DestroyNatureAt(newPosition);
        }
    }

    internal bool CheckIfPositionInBound(Vector2Int position)
    {
        if (position.x >= 0 && position.x <= width && position.y >= 0 && position.y < height)
        {
            return true;
        }
        return false;
    }

    internal bool CheckIfPositionIsFree(Vector2Int position)
    {
        return CheckIfPositionIsOfType(position, CellType.Empty);
    }

    internal List<Vector2Int> GetPathBetween(Vector2Int startPosition, Vector2Int endPosition)
    {
        var resultPath = GridSearch.AStarSearch(placementGrid, new Point(startPosition.x, startPosition.y), new Point(endPosition.x, endPosition.y));

        List<Vector2Int> path = new List<Vector2Int>();
        foreach (Point point in resultPath)
        {
            path.Add(new Vector2Int(point.X, point.Y));
        }
        return path;
    }

    internal void RemoveAllTemporaryStructures()
    {
        foreach (var structure in temporaryRoadObject.Values)
        {
            var position = Vector2Int.RoundToInt(structure.transform.position);
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

    private void DestroyNatureAt(Vector2Int position)
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll((Vector2Int)position, new Vector3(0.5f, 0.5f),0, Vector2.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("Nature"));
        foreach (var item in hits)
        {
            Destroy(item.collider.gameObject);
        }
    }

    private bool CheckIfPositionIsOfType(Vector2Int position, CellType type)
    {
        return placementGrid[position.x, position.y] == type;
    }

    internal void PlaceTemporaryStructure(Vector2Int position, GameObject structurePrefab, CellType type)
    {
        placementGrid[position.x, position.y] = type;
        StructureModel structure = CreateANewStructureModel(position, structurePrefab, type);
        temporaryRoadObject.Add(position, structure);
    }

    private StructureModel CreateANewStructureModel(Vector2Int position, GameObject structurePrefab, CellType type)
    {
        GameObject structure = new GameObject(type.ToString());
        structure.transform.SetParent(transform);
        structure.transform.localPosition = new Vector3(position.x, position.y);
        var structureModel = structure.AddComponent<StructureModel>();
        structureModel.CreateModel(structurePrefab);

        return structureModel;
    }

    public void ModifyStructureModel(Vector2Int position, GameObject newModel, Quaternion rotation)
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
