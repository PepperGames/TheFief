using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ResidentialStructuresInfo : MonoBehaviour
{
    [Inject] protected Services services;

    [SerializeField] private Slider durabilitySlider;

    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _repairButton;
    [SerializeField] private Button _destroyButton;

    [SerializeField] private ResidentialStructure _residentialStructure;
    [SerializeField] private CharacterPlacesInResidentialStructureView _residentialStructureView;

    //[SerializeField] private CharacterPlacesInIndustrialStructureView _inIndustrialStructureView;


    private void Start()
    {
        _upgradeButton.onClick.AddListener(_residentialStructure.Upgrade);
        _repairButton.onClick.AddListener(_residentialStructure.RepairCompletely);
        _residentialStructure.Durability.OnDurabilityChange += OnDurabilityChange;
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
        _residentialStructureView.Initialize(services, _residentialStructure);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        _residentialStructureView.Disable();
        gameObject.SetActive(false);
    }

    private void OnDurabilityChange(float newDurability)
    {
        durabilitySlider.value = newDurability / 100;
    }

    private void DestroyStructure()
    {
        _residentialStructure.Demolish();

        //Vector3 structurePosition = _industrialStructure.transform.position;
        //services.PlacementManager.Demolish(new Vector2Int((int)structurePosition.x, (int)structurePosition.y));
    }
}
