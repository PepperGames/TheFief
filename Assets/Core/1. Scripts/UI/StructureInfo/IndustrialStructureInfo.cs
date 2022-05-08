using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class IndustrialStructureInfo : MonoBehaviour
{
    [Inject] protected Services services;

    [SerializeField] private Slider durabilitySlider;

    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _repairButton;
    [SerializeField] private Button _destroyButton;

    [SerializeField] private IndustrialStructure _industrialStructure;
    [SerializeField] private CharacterPlacesInIndustrialStructureView _inIndustrialStructureView;

    private void Start()
    {
        _upgradeButton.onClick.AddListener(_industrialStructure.Upgrade);
        _repairButton.onClick.AddListener(_industrialStructure.RepairCompletely);
        _industrialStructure.Durability.OnDurabilityChange += OnDurabilityChange;
        _destroyButton.onClick.AddListener(DestroyStructure);
    }

    public void ShowOrHide()
    {
        if (gameObject.activeInHierarchy)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    public void Show()
    {
        _inIndustrialStructureView.Initialize(services, _industrialStructure); 
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        _inIndustrialStructureView.Disable();
        gameObject.SetActive(false); 
    }

    private void OnDurabilityChange(float newDurability)
    {
        durabilitySlider.value = newDurability / 100;
    }

    private void DestroyStructure()
    {
        _industrialStructure.Demolish();

        //Vector3 structurePosition = _industrialStructure.transform.position;
        //services.PlacementManager.Demolish(new Vector2Int((int)structurePosition.x, (int)structurePosition.y));
    }
}
