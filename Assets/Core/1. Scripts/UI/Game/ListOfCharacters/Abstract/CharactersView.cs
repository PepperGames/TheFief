using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharactersView : MonoBehaviour
{
    [SerializeField] protected Image _portrait;
    [SerializeField] protected TMP_Text _nameText;
    [SerializeField] protected TMP_Text _placeNameText;

    [SerializeField] protected Button _addCharacterButton;

    protected Structure _structure;
    protected Character _character;

    public Action<AblebodiedCharactersView> OnSelectCharacter;

    public virtual void Initialize(Structure structure, Character character)
    {
        if (character != null)
        {
            _character = character;
            _structure = structure;

            InitializeUI();
            OnEventsSubscribe();
        }
    }

    public abstract void InitializeUI();

    public virtual void Disable()
    {
        OnEventsUnscribe();
    }

    protected virtual void AddToStructure()
    {
        if (_structure.CharacterPlaces.AddCharacter(_character))
        {
            InitializeUI();
        }
    }

    protected virtual void OnEventsSubscribe()
    {
        //_character.OnChangeWorkplace += InitializeUI;
        _structure.CharacterPlaces.OnCharacterListChange += InitializeUI;
    }

    protected virtual void OnEventsUnscribe()
    {
        //_character.OnChangeWorkplace -= InitializeUI;
        //_structure.CharacterPlaces.OnCharacterListChange -= InitializeUI;
        _addCharacterButton.onClick.RemoveAllListeners();
    }
}
