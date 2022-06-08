using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CharacterInfo : MonoBehaviour
{
    [Inject] protected Services services;

    [SerializeField] protected Character _character;

    [SerializeField] private CharacterTraitView _characterTraitViewPrefab;
    [SerializeField] private Transform _characterTraitViewContainer;

    [SerializeField] private EffectView _effectViewPrefab;
    [SerializeField] private Transform _effectViewsContainer;

    protected virtual void Start()
    {

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

        gameObject.SetActive(true);
    }

    private void DisplayCharacterTraits()
    {
        foreach (var characterTrait in _character.CharacterTraitsManager.CharacterTraits)
        {
            CharacterTraitView characterTraitViewPrefab = Instantiate(_characterTraitViewPrefab, _characterTraitViewContainer);
            characterTraitViewPrefab.Initialize(characterTrait);
        }
    }

    private void DisplayEffects()
    {
        foreach (var effectBox in _character.EffectsManager.EffectBoxes)
        {
            if (effectBox.Duration > 0)
            {
                EffectView effectViewPrefab = Instantiate(_effectViewPrefab, _effectViewsContainer);
                effectViewPrefab.Initialize(effectBox);
            }
        }
    }

    public virtual void Hide()
    {

        gameObject.SetActive(false);
    }
}
