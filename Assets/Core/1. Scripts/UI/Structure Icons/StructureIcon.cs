using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StructureIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    public Structure structure;

    public StructureCostInfo _structureCostInfo;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _structureCostInfo.Show(structure.StructureCost.GetAmountOfResourcesForBuild());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _structureCostInfo.Hide();
    }
}
