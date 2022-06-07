using TMPro;
using UnityEngine;

public class IndustrialStructureInfo : StructureInfo
{
    [SerializeField] protected TMP_Text _productionPerHourText;

    public override void Show()
    {
        base.Show();
        _productionPerHourText.text = ((IndustrialStructure)_structure).CalculateProductivityPerHour().ToString() + "/h";
    }
}