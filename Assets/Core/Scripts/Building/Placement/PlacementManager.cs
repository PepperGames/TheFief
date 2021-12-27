using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public int width;
    public int height;

    private Grid placementGrid;

    [SerializeField] private Dictionary<Vector2Int, BasicStructure> temporaryStructureObject = new Dictionary<Vector2Int, BasicStructure>();
    [SerializeField] private Dictionary<Vector2Int, BasicStructure> structureDictionary = new Dictionary<Vector2Int, BasicStructure>();

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

    internal void PlaceStructureOnTheMap(Vector2Int position, BasicStructure basicStructure, CellType type)
    {
        BasicStructure createdBasicStructure = CreateANewStructureModel(position, basicStructure, type);

        foreach (Vector2Int item in createdBasicStructure.Points)
        {
            var newPosition = position + new Vector2Int(item.x, item.y);
            placementGrid[newPosition.x, newPosition.y] = type;
            structureDictionary.Add(newPosition, createdBasicStructure);
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
        foreach (var structure in temporaryStructureObject.Values)
        {
            foreach (Vector2Int point in structure.Points)
            {
                var position = Vector2Int.RoundToInt(structure.transform.position);
                var newPosition = position + new Vector2Int(point.x, point.y);
                placementGrid[newPosition.x, newPosition.y] = CellType.Empty;
            }

            Destroy(structure.gameObject);
        }
        temporaryStructureObject.Clear();
    }

    internal void AddTemporaryStructuresToStructureDictionary()
    {
        foreach (var structure in temporaryStructureObject)
        {
            structureDictionary.Add(structure.Key, structure.Value);
            DestroyNatureAt(structure.Key);
        }
        temporaryStructureObject.Clear();
    }

    public void Demolish(Vector2Int position)
    {
        if (structureDictionary.ContainsKey(position) == false)
            return;

        BasicStructure structure = structureDictionary[position];

        foreach (Vector2Int point in structure.Points)
        {
            var structurePosition = Vector2Int.RoundToInt(structure.transform.position);
            var newPosition = structurePosition + new Vector2Int(point.x, point.y);
            placementGrid[newPosition.x, newPosition.y] = CellType.Empty;
        }

        Destroy(structure.gameObject);

        structureDictionary.Remove(position);
    }

    private void DestroyNatureAt(Vector2Int position)
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll((Vector2Int)position, new Vector3(0.5f, 0.5f), 0, Vector2.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("Nature"));
        foreach (var item in hits)
        {
            Destroy(item.collider.gameObject);
        }
    }

    private bool CheckIfPositionIsOfType(Vector2Int position, CellType type)
    {
        return placementGrid[position.x, position.y] == type;
    }

    internal void PlaceTemporaryObject(Vector2Int position, BasicStructure basicStructure, CellType type)
    {
        BasicStructure createdBasicStructure = CreateANewStructureModel(position, basicStructure, type);

        foreach (Vector2Int item in createdBasicStructure.Points)
        {
            var newPosition = position + new Vector2Int(item.x, item.y);
            placementGrid[newPosition.x, newPosition.y] = type;

            temporaryStructureObject.Add(position, createdBasicStructure);
        }
    }

    private BasicStructure CreateANewStructureModel(Vector2Int position, BasicStructure basicStructure, CellType type)
    {
        GameObject structureModel = basicStructure.model;
        structureModel.transform.localPosition = new Vector3(position.x, position.y);

        GameObject createdGameObject = Instantiate(structureModel, transform);
        BasicStructure createdBasicStructure = createdGameObject.GetComponent<BasicStructure>();

        createdBasicStructure.Initialize();

        return createdBasicStructure;
    }

    public void ModifyStructureModel(Vector2Int position, GameObject viewModel, Quaternion rotation)
    {
        if (temporaryStructureObject.ContainsKey(position))
        {
            temporaryStructureObject[position].SwapModelView(viewModel, rotation);
        }
        else if (structureDictionary.ContainsKey(position))
        {
            structureDictionary[position].SwapModelView(viewModel, rotation);
        }
    }

    public BasicStructure GetRandomStructure()
    {
        int randomIndex = UnityEngine.Random.Range(0, structureDictionary.Count-1);
        Debug.Log(randomIndex);
        Debug.Log(structureDictionary.Count - 1);

        BasicStructure structure = structureDictionary.ElementAt(randomIndex).Value;
        return structure;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1f, 1f);
        Gizmos.DrawWireCube(new Vector3(width / 2f - 0.5f, height / 2f - 0.5f), new Vector3(width, height, 0));
    }
}
