using TMPro;
using UnityEngine;

public class IndustrialStructureInfo : StructureInfo
{
    [SerializeField] protected TMP_Text _productionPerHourText;

    protected override void Start()
    {
        base.Start();
        _structure.CharacterPlaces.OnCharacterListChange += DisplayroductionPerHourText;
        _structure.CharacterPlaces.OnNumberOfPlacesChange += DisplayroductionPerHourText;
    }

    public override void Show()
    {
        base.Show();
        DisplayroductionPerHourText();
    }

    private void DisplayroductionPerHourText()
    {
        _productionPerHourText.text = CharacterRounder.Round(((IndustrialStructure)_structure).CalculateProductivityPerHour(), 1) + "/h";
    }
}