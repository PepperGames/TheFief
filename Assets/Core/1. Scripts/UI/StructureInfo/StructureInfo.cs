using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

public abstract class StructureInfo : MonoBehaviour
{
    [Inject] protected Services services;

    [SerializeField] protected Slider durabilitySlider;

    [SerializeField] protected TMP_Text _lvlText;

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
        _structure.CharacterPlaces.OnCNumberOfPlacesChange += _characterPlacesInStructureView.FillContent;
        _structure.OnLvlUpgrade += DisplayLvl;
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
        DisplayLvl();

        gameObject.SetActive(true);
    }

    private void DisplayLvl()
    {
        _lvlText.text = _structure.LVL.ToString() + " lvl";
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
