using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListOfCharacters : MonoBehaviour
{
    [SerializeField] protected CharacterManager _characterManager;

    [SerializeField] protected CharactersView _charactersViewPrefab;
    [SerializeField] protected Transform content;

    [SerializeField] protected List<CharactersView> _charactersViews;

    protected Structure _structure;

    [SerializeField] protected Button _closeButton;

    protected virtual void Start()
    {
        _closeButton.onClick.AddListener(Close);
        _characterManager.OnCharacterListChange += Initialize;
    }

    public virtual void Open(Structure structure)
    {
        _structure = structure;
        Initialize();
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }

    public virtual void Initialize()
    {
        ClearContent();

        foreach (Character character in _characterManager.AllCharacters)
        {
            FillCharactersView(character);
        }
    }

    protected virtual void ClearContent()
    {
        foreach (CharactersView character in _charactersViews)
        {
            character.Disable();
            Destroy(character.gameObject);
        }
        _charactersViews.Clear();
    }

    protected virtual void FillCharactersView(Character character)
    {
        CharactersView livingCharactersView = Instantiate(_charactersViewPrefab, content);
        livingCharactersView.Initialize(_structure, character);

        _charactersViews.Add(livingCharactersView);
    }
}
