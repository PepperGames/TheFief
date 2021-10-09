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
    internal List<Vector3Int> GetNeighbourTypesFor(Vector3Int position, CellType type)
    {
        var neighbourVerticles = placementGrid.GetAdjacentCellsOfType(position.x, position.y, type);

        List<Vector3Int> neighbours = new List<Vector3Int>();

        foreach (var point in neighbourVerticles)
        {
            neighbours.Add(new Vector3Int(point.X, point.Y, 0));
        }
        return neighbours;
    }

    internal bool ChecIfPositionInBound(Vector3Int position)
    {
        if (position.x >= 0 && position.x <= width && position.y >= 0 && position.y < height)
        {
            return true;
        }
        return false;
    }

    internal bool ChecIfPositionIsFree(Vector3Int position)
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
        }
        temporaryRoadObject.Clear();
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
