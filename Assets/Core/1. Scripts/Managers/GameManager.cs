using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] private Services services;

    [SerializeField] private BuildingAction _buildingAction;

    public BuildingAction BuildingAction => _buildingAction;

    private void Start()
    {
        services.UIController.BuildingUIController.OnRoadPlacement += RoadPlacementHandler;
        services.UIController.BuildingUIController.OnHousePlacement += HousePlacementHandler;
        services.UIController.BuildingUIController.OnStructureDemolish += StructureDemolishHandler;
    }

    private void HousePlacementHandler(Structure structure)
    {
        ClearInputActions();

        _buildingAction = BuildingAction.Build;

        services.InputManager.OnMouseClick += services.StructureManager.PlaceHouse;
    }

    private void RoadPlacementHandler()
    {
        ClearInputActions();

        _buildingAction = BuildingAction.Build;

        services.InputManager.OnMouseClick += services.RoadManager.PlaceRoad;
        services.InputManager.OnMouseHold += services.RoadManager.PlaceRoad;
        services.InputManager.OnMouseUp += services.RoadManager.FinishPlacingRoad;
    }

    private void StructureDemolishHandler()
    {
        ClearInputActions();

        _buildingAction = BuildingAction.Demolish;

        services.InputManager.OnMouseClick += services.RoadManager.Demolish;
        services.InputManager.OnMouseClick += services.StructureManager.Demolish;
    }

    public void ClearInputActions()
    {
        _buildingAction = BuildingAction.None;

        services.InputManager.OnMouseClick = null;
        services.InputManager.OnMouseHold = null;
        services.InputManager.OnMouseUp = null;
    }
}

public enum BuildingAction
{
    None,
    Build,
    Demolish
}