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

    public StructureIcon placeRoadButton;
    public StructureIcon[] structureIcons;

    [Inject] [SerializeField] private StructureManager structureManager;

    //public Button placeRoadButton, placeHouseButton, placeSpecialButton, placeBigStructureButton;

    public Color outlineColor;

    private void Start()
    {
        placeRoadButton.button.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeRoadButton.button);
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
    }

    private void ModifyOutline(Button button)
    {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.enabled = true;
    }

    private void ResetButtonColor()
    {
        placeRoadButton.button.GetComponent<Outline>().enabled = false;

        foreach (StructureIcon structureIcon in structureIcons)
        {
            structureIcon.button.GetComponent<Outline>().enabled = false;
        }
    }
}