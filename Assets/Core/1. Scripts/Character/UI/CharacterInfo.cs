using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

public class CharacterInfo : MonoBehaviour
{
    [Inject] protected Services services;

    [SerializeField] protected Character _character;

    [SerializeField] protected Button _closeButton;

    [SerializeField] private Image _portrait;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _estatesText;
    [SerializeField] private TMP_Text _ageText;

    [SerializeField] private CharacterTraitView _characterTraitViewPrefab;
    [SerializeField] private Transform _characterTraitViewContainer;

    [SerializeField] private EffectView _effectViewPrefab;
    [SerializeField] private Transform _effectViewsContainer;

    [SerializeField] private Slider _happinesSlider;

    private void Start()
    {
        _closeButton.onClick.AddListener(Hide);

        _character.CharacterData.Happiness.OnHappinessChange += DisplayHappines;
        _portrait.sprite = _character.CharacterData.Portrait;
        _nameText.text = _character.CharacterData.CharacterName;
        _estatesText.text = _character.CharacterData.Estates.ToString();
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
        DisplayCharacterTraits();
        DisplayEffects();
        DisplayHappines();
        DisplayAge();

        gameObject.SetActive(true);
    }

    private void DisplayCharacterTraits()
    {
        CleaerContainer(_characterTraitViewContainer);
        foreach (var characterTrait in _character.CharacterTraitsManager.CharacterTraits)
        {
            if (characterTrait.visible)
            {
                CharacterTraitView characterTraitViewPrefab = Instantiate(_characterTraitViewPrefab, _characterTraitViewContainer);
                characterTraitViewPrefab.Initialize(characterTrait);
            }
        }
    }

    private void DisplayEffects()
    {
        CleaerContainer(_effectViewsContainer);
        foreach (var effectBox in _character.EffectsManager.EffectBoxes)
        {
            if (effectBox.Duration > 0)
            {
                EffectView effectViewPrefab = Instantiate(_effectViewPrefab, _effectViewsContainer);
                effectViewPrefab.Initialize(effectBox);
            }
        }
    }

    private void DisplayAge()
    {
        _ageText.text = "Age:<br>" + _character.CharacterData.Age.years.ToString();
    }

    private void CleaerContainer(Transform container)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }

    private void DisplayHappines()
    {
        _happinesSlider.value = _character.CharacterData.Happiness.IndexOfHappiness;
    }

    public virtual void Hide()
    {

        gameObject.SetActive(false);
    }
}
