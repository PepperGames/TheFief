using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class IndustrialStructureInfo : MonoBehaviour
{
    [Inject] protected Services services;

    [SerializeField] private Slider durabilitySlider;

    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button repairButton;
    [SerializeField] private Button destroyButton;

    [SerializeField] private IndustrialStructure industrialStructure;

    private void Start()
    {
        upgradeButton.onClick.AddListener(industrialStructure.Upgrade);
        repairButton.onClick.AddListener(industrialStructure.RepairCompletely);
        industrialStructure.Durability.OnDurabilityChange += OnDurabilityChange;
        destroyButton.onClick.AddListener(DestroyStructure);
    }

    private void OnDurabilityChange(float newDurability)
    {
        durabilitySlider.value = newDurability / 100;
    }

    private void DestroyStructure()
    {
        Vector3 structurePosition = industrialStructure.transform.position;
        services.PlacementManager.Demolish(new Vector2Int((int)structurePosition.x, (int)structurePosition.y));
    }
}
