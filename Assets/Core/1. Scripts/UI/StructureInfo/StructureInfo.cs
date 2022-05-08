using UnityEngine;
using UnityEngine.UI;
using Zenject;


public abstract class StructureInfo : MonoBehaviour
{
    [Inject] protected Services services;

    [SerializeField] protected Slider durabilitySlider;

    [SerializeField] protected Button _upgradeButton;
    [SerializeField] protected Button _repairButton;
    [SerializeField] protected Button _destroyButton;

    [SerializeField] protected Structure _structure;

    [SerializeField] protected CharacterPlacesInStructureView _characterPlacesInStructureView;

    protected virtual void Start()
    {
        _upgradeButton.onClick.AddListener(_structure.Upgrade);
        _repairButton.onClick.AddListener(_structure.RepairCompletely);
        _structure.Durability.OnDurabilityChange += OnDurabilityChange;
        _destroyButton.onClick.AddListener(DestroyStructure);
    }

    public virtual void ShowOrHide()
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

    public virtual void Show()
    {
        _characterPlacesInStructureView.Initialize(services, _structure);
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        _characterPlacesInStructureView.Disable();
        gameObject.SetActive(false);
    }

    protected virtual void OnDurabilityChange(float newDurability)
    {
        durabilitySlider.value = newDurability / 100;
    }

    protected virtual void DestroyStructure()
    {
        _structure.Demolish();
    }
}
