using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    public CameraMovement cameraMovement;

    [Inject] [SerializeField] private RoadManager roadManager;
    [Inject] [SerializeField] private InputManager inputManager;

    [Inject] [SerializeField] private StructureManager structureManager;

    [Inject] [SerializeField] public TestUIController uiController;
    private void Start()
    {
        uiController.OnRoadPlacement += RoadPlacementHandler;
        uiController.OnHousePlacement += HousePlacementHandler;
        uiController.OnStructureDemolish += StructureDemolishHandler;
    }

    private void HousePlacementHandler(Structure structure)
    {
        ClearInputActions();
        inputManager.OnMouseClick += structureManager.PlaceHouse;
    }

    private void RoadPlacementHandler()
    {
        ClearInputActions();

        inputManager.OnMouseClick += roadManager.PlaceRoad;
        inputManager.OnMouseHold += roadManager.PlaceRoad;
        inputManager.OnMouseUp += roadManager.FinishPlacingRoad;
    }

    private void StructureDemolishHandler()
    {
        ClearInputActions();
        inputManager.OnMouseClick += roadManager.Demolish;
        inputManager.OnMouseClick += structureManager.Demolish;
    }

    private void ClearInputActions()
    {
        inputManager.OnMouseClick = null;
        inputManager.OnMouseHold = null;
        inputManager.OnMouseUp = null;
    }

    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x, inputManager.CameraMovementVector.y, 0));
    }
}
