using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LivingCharactersView : MonoBehaviour
{
    public Image portrait;
    public TMP_Text nameText;
    public TMP_Text workplaceText;

    public Button addCharacterButton;

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
        portrait.sprite = _character.Portrait;
        nameText.text = _character.CharacterName;

        if (_character.Workplace == null)
        {
            addCharacterButton.gameObject.SetActive(true);
        }
        else
        {
            addCharacterButton.gameObject.SetActive(false);
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
            addCharacterButton.onClick.AddListener(AddToWorkplace);
        }

        _character.OnChangeWorkplace += InitializeUI;
        _residentialStructure.CharacterPlaces.OnCharacterListChange += InitializeUI;
    }

    private void OnEventsUnscribe()
    {
        _character.OnChangeWorkplace -= InitializeUI;
        _residentialStructure.CharacterPlaces.OnCharacterListChange -= InitializeUI;
        addCharacterButton.onClick.RemoveAllListeners();
    }
}
