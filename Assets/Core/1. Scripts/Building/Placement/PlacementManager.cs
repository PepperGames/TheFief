using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlacementManager : MonoBehaviour
{
    public int width;
    public int height;

    [Inject] private Services services;

    [SerializeField] private Dictionary<Vector2Int, BasicStructure> temporaryStructureObject = new Dictionary<Vector2Int, BasicStructure>();
    [SerializeField] private Dictionary<Vector2Int, BasicStructure> structureDictionary = new Dictionary<Vector2Int, BasicStructure>();

    internal CellType[] GetNeighbourTypesFor(Vector2Int temporaryPosition)
    {
        return services.Grid.GetAllAdjacentCellTypes(temporaryPosition.x, temporaryPosition.y);
    }

    internal List<Vector2Int> GetNeighboursOfTypeFor(Vector2Int position, CellType type)
    {
        var neighbourVerticles = services.Grid.GetAdjacentCellsOfType(position.x, position.y, type);

        List<Vector2Int> neighbours = new List<Vector2Int>();

        foreach (var point in neighbourVerticles)
        {
            neighbours.Add(new Vector2Int(point.X, point.Y));
        }
        return neighbours;
    }

    internal BasicStructure PlaceStructureOnTheMap(Vector2Int position, BasicStructure basicStructure, CellType type)
    {
        BasicStructure createdBasicStructure = CreateANewStructureModel(position, basicStructure, type);

        foreach (Vector2Int item in createdBasicStructure.Points)
        {
            var newPosition = position + new Vector2Int(item.x, item.y);
            services.Grid[newPosition.x, newPosition.y] = type;
            structureDictionary.Add(newPosition, createdBasicStructure);
            DestroyNatureAt(newPosition);
        }
        return createdBasicStructure;
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
        var resultPath = GridSearch.AStarSearch(services.Grid, new Point(startPosition.x, startPosition.y), new Point(endPosition.x, endPosition.y));

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
                services.Grid[newPosition.x, newPosition.y] = CellType.Empty;
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

            services.Grid[newPosition.x, newPosition.y] = CellType.Empty;
            structureDictionary.Remove(newPosition);
        }

        Destroy(structure.gameObject);
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
        return services.Grid[position.x, position.y] == type;
    }

    internal void PlaceTemporaryObject(Vector2Int position, BasicStructure basicStructure, CellType type)
    {
        BasicStructure createdBasicStructure = CreateANewStructureModel(position, basicStructure, type);

        foreach (Vector2Int item in createdBasicStructure.Points)
        {
            var newPosition = position + new Vector2Int(item.x, item.y);
            services.Grid[newPosition.x, newPosition.y] = type;

            temporaryStructureObject.Add(position, createdBasicStructure);
        }
    }

    private BasicStructure CreateANewStructureModel(Vector2Int position, BasicStructure basicStructure, CellType type)
    {
        //GameObject structureModel = basicStructure.model;
        //structureModel.transform.localPosition = new Vector3(position.x, position.y);

        //GameObject createdGameObject = Instantiate(structureModel, transform);
        //BasicStructure createdBasicStructure = createdGameObject.GetComponent<BasicStructure>();
        //======================================
        Vector3 position3 = new Vector3(position.x, position.y);

        BasicStructure createdBasicStructure = Instantiate(basicStructure, position3, Quaternion.identity, transform);

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
        if (structureDictionary.Count > 0)
        {
            int randomIndex = Random.Range(0, structureDictionary.Count - 1);

            BasicStructure structure = structureDictionary.ElementAt(randomIndex).Value;
            return structure;
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1f, 1f);
        Gizmos.DrawWireCube(new Vector3(width / 2f - 0.5f, height / 2f - 0.5f), new Vector3(width, height, 0));
    }
}
