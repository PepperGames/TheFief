using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuildingUIController : MonoBehaviour
{
    [Inject] private Services services;

    [SerializeField] private Button placeRoadButton;
    [SerializeField] private StructureIcon[] structureIcons;
    [SerializeField] private Button demolishButton;

    [SerializeField] private Image _demolishButtonImage;
    [SerializeField] private Sprite _demolishButtonActive;
    [SerializeField] private Sprite _demolishButtonNotActive;

    public Color outlineColor;

    public Action OnRoadPlacement;
    public Action<Structure> OnHousePlacement;
    public Action OnStructureDemolish;

    private void Start()
    {
        placeRoadButton.onClick.AddListener(() =>
        {
            OnRoadButton();
        });
        foreach (StructureIcon structureIcon in structureIcons)
        {
            structureIcon.button.onClick.AddListener(() =>
            {
                OnStructureButton(structureIcon);
            });
        }
        demolishButton.onClick.AddListener(() =>
        {
            OnDemolishButton();
        });
    }

    private void OnRoadButton()
    {
        ResetButtonColor();
        ModifyOutline(placeRoadButton);
        OnRoadPlacement?.Invoke();
    }

    private void OnStructureButton(StructureIcon structureIcon)
    {
        ResetButtonColor();
        ModifyOutline(structureIcon.button);
        services.StructureManager.SetSelectedStructure(structureIcon.structure);
        OnHousePlacement?.Invoke(structureIcon.structure);
    }

    private void OnDemolishButton()
    {
        if (services.GameManager.BuildingAction == BuildingAction.Demolish)
        {
            services.GameManager.ClearInputActions();
            _demolishButtonImage.sprite = _demolishButtonNotActive;
        }
        else
        {
            ResetButtonColor();
            ModifyOutline(demolishButton);
            _demolishButtonImage.sprite = _demolishButtonActive;
            OnStructureDemolish?.Invoke();
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
        placeRoadButton.GetComponent<Outline>().enabled = false;

        foreach (StructureIcon structureIcon in structureIcons)
        {
            structureIcon.button.GetComponent<Outline>().enabled = false;
        }
    }
}
