using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class IndustrialStructureInfo : MonoBehaviour
{
    [Inject] [HideInInspector] protected PlacementManager placementManager;

    [SerializeField] private Slider durabilitySlider;

    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button repairButton;
    [SerializeField] private Button destroyButton;

    [SerializeField] private IndustrialStructure industrialStructure;
    [SerializeField] private Durability durability;

    private void Start()
    {
        upgradeButton.onClick.AddListener(industrialStructure.Upgrade);
        repairButton.onClick.AddListener(industrialStructure.RepairCompletely);
        durability.OnDurabilityChange += OnDurabilityChange;
        destroyButton.onClick.AddListener(DestroyStructure);
    }

    private void OnDurabilityChange(float newDurability)
    {
        durabilitySlider.value = newDurability / 100;
    }

    private void DestroyStructure()
    {
        Vector3 structurePosition = industrialStructure.transform.position;
        placementManager.Demolish(new Vector2Int((int)structurePosition.x, (int)structurePosition.y));
    }
}
