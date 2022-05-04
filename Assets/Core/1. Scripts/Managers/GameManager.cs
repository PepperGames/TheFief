using SVS;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] private Services services;

    private void Start()
    {
        services.UIController.OnRoadPlacement += RoadPlacementHandler;
        services.UIController.OnHousePlacement += HousePlacementHandler;
        services.UIController.OnStructureDemolish += StructureDemolishHandler;
    }

    private void HousePlacementHandler(Structure structure)
    {
        ClearInputActions();
        services.InputManager.OnMouseClick += services.StructureManager.PlaceHouse;
    }

    private void RoadPlacementHandler()
    {
        ClearInputActions();

        services.InputManager.OnMouseClick += services.RoadManager.PlaceRoad;
        services.InputManager.OnMouseHold += services.RoadManager.PlaceRoad;
        services.InputManager.OnMouseUp += services.RoadManager.FinishPlacingRoad;
    }

    private void StructureDemolishHandler()
    {
        ClearInputActions();
        services.InputManager.OnMouseClick += services.RoadManager.Demolish;
        services.InputManager.OnMouseClick += services.StructureManager.Demolish;
    }

    private void ClearInputActions()
    {
        services.InputManager.OnMouseClick = null;
        services.InputManager.OnMouseHold = null;
        services.InputManager.OnMouseUp = null;
    }
}
