using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TestUIController : MonoBehaviour
{
    public Action OnRoadPlacement;
    public Action<Structure> OnHousePlacement;
    public Action OnStructureDemolish;

    public Button placeRoadButton;
    public StructureIcon[] structureIcons;
    public Button demolishButton;

    [Inject] [SerializeField] private StructureManager structureManager;

    public Color outlineColor;

    private void Start()
    {
        placeRoadButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeRoadButton);
            OnRoadPlacement?.Invoke();
        });
        foreach (StructureIcon structureIcon in structureIcons)
        {
            structureIcon.button.onClick.AddListener(() =>
            {
                ResetButtonColor();
                ModifyOutline(structureIcon.button);
                structureManager.SetSelectedStructure(structureIcon.structure);
                OnHousePlacement?.Invoke(structureIcon.structure);
            });
        }
        demolishButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(demolishButton);
            OnStructureDemolish?.Invoke();
        });
    }

    private void ModifyOutline(Button button)
    {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.enabled = true;
    }

    private void ResetButtonColor()
    {
        placeRoadButton.GetComponent<Outline>().enabled = false;

        foreach (StructureIcon structureIcon in structureIcons)
        {
            structureIcon.button.GetComponent<Outline>().enabled = false;
        }
    }
}