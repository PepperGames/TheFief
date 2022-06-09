using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

public abstract class StructureInfo : MonoBehaviour
{
    [Inject] protected Services services;

    [SerializeField] protected Slider durabilitySlider;
    [SerializeField] protected Slider _fillSlider;

    [SerializeField] protected TMP_Text _lvlText;

    [SerializeField] protected Button _upgradeButton;
    [SerializeField] protected Button _repairButton;
    [SerializeField] protected Button _destroyButton;

    [SerializeField] protected Button _closeButton;

    [SerializeField] protected Structure _structure;

    [SerializeField] protected CharacterPlacesInStructureView _characterPlacesInStructureView;

    protected virtual void Start()
    {
        _upgradeButton.onClick.AddListener(_structure.Upgrade);
        _repairButton.onClick.AddListener(_structure.RepairCompletely);
        _destroyButton.onClick.AddListener(DestroyStructure);
        _closeButton.onClick.AddListener(Hide);

        _structure.Durability.OnDurabilityChange += OnDurabilityChange;
        _structure.CharacterPlaces.OnNumberOfPlacesChange += _characterPlacesInStructureView.FillContent;
        _structure.OnLvlUpgrade += DisplayLvl;

        _structure.OnLvlUpgrade += DisplayFullness;
        _structure.CharacterPlaces.OnCharacterListChange += DisplayFullness;
        _structure.CharacterPlaces.OnNumberOfPlacesChange += DisplayFullness;
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
        DisplayFullness();

        gameObject.SetActive(true);
    }

    private void DisplayLvl()
    {
        _lvlText.text = _structure.LVL.ToString() + " lvl";
    }

    private void DisplayFullness()
    {
        _fillSlider.maxValue = _structure.CharacterPlaces.NumberOfPlaces;
        _fillSlider.value = _structure.CharacterPlaces.Characters.Count;
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
