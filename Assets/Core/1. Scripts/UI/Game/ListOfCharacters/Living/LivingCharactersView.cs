using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LivingCharactersView : MonoBehaviour
{
    [SerializeField] private Image _portrait;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _livingPlaceText;

    [SerializeField] private Button _addCharacterButton;

    private ResidentialStructure _residentialStructure;
    private Character _character;

    public Action<AblebodiedCharactersView> OnSelectCharacter;

    public void Initialize(ResidentialStructure residentialStructure, Character character)
    {
        if (character != null)
        {
            _character = character;
            _residentialStructure = residentialStructure;

            InitializeUI();
            OnEventsSubscribe();
        }
    }

    public void InitializeUI()
    {
        _portrait.sprite = _character.Portrait;
        _nameText.text = _character.CharacterName;

        if (_character.Workplace == null)
        {
            _addCharacterButton.gameObject.SetActive(true);
        }
        else
        {
            _addCharacterButton.gameObject.SetActive(false);
        }
    }

    public void Disable()
    {
        OnEventsUnscribe();
    }

    private void AddToWorkplace()
    {
        if (_residentialStructure.CharacterPlaces.AddCharacter(_character))
        {
            InitializeUI();
        }
    }

    private void OnEventsSubscribe()
    {
        if (_character.Workplace == null)
        {
            _addCharacterButton.onClick.AddListener(AddToWorkplace);
        }

        _character.OnChangeWorkplace += InitializeUI;
        _residentialStructure.CharacterPlaces.OnCharacterListChange += InitializeUI;
    }

    private void OnEventsUnscribe()
    {
        _character.OnChangeWorkplace -= InitializeUI;
        _residentialStructure.CharacterPlaces.OnCharacterListChange -= InitializeUI;
        _addCharacterButton.onClick.RemoveAllListeners();
    }
}
