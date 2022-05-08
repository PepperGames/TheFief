using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AblebodiedCharactersView : MonoBehaviour
{
    public Image portrait;
    public TMP_Text nameText;
    public TMP_Text workplaceText;

    public Button addCharacterButton;

    private IndustrialStructure _industrialStructure;
    private Character _character;

    public Action<AblebodiedCharactersView> OnSelectCharacter;

    public void Initialize(IndustrialStructure industrialStructure, Character character)
    {
        Debug.Log("character" + character);
        if (character != null)
        {
            _character = character;
            _industrialStructure = industrialStructure;

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
        Debug.Log(_character);
        Debug.Log(_industrialStructure);
        Debug.Log(_industrialStructure.CharacterPlaces);
        _industrialStructure.CharacterPlaces.AddCharacter(_character);

        InitializeUI();
    }

    private void OnEventsSubscribe()
    {
        if (_character.Workplace == null)
        {
            addCharacterButton.onClick.AddListener(AddToWorkplace);
        }

        _industrialStructure.CharacterPlaces.OnCharacterListChange += InitializeUI;
    }

    private void OnEventsUnscribe()
    {
        _industrialStructure.CharacterPlaces.OnCharacterListChange -= InitializeUI;
        addCharacterButton.onClick.RemoveAllListeners();
    }
}
