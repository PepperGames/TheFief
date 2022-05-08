using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListOfAblebodiedCharacters : MonoBehaviour
{
    [SerializeField] private CharacterManager _characterManager;

    [SerializeField] private AblebodiedCharactersView ablebodiedCharactersViewPrefab;
    [SerializeField] private Transform content;

    [SerializeField] private List<AblebodiedCharactersView> ablebodiedCharactersViews;

    private IndustrialStructure _industrialStructure;

    [SerializeField] private Button _closeButton;

    private void Start()
    {
        _closeButton.onClick.AddListener(Close);
        _characterManager.OnCharacterListChange += Initialize;
    }

    public void Open(IndustrialStructure industrialStructure)
    {
        _industrialStructure = industrialStructure;
        Initialize();
        gameObject.SetActive(true);
    }

    public void Close()
    {
        Debug.Log("Close");

        gameObject.SetActive(false);
    }

    public void Initialize()
    {
        Debug.Log("Initialize");

        ClearContent();

        foreach (Character character in _characterManager.Characters)
        {
            FillAblebodiedCharactersView(character);
        }
    }

    private void ClearContent()
    {
        Debug.Log("ClearContent");

        foreach (AblebodiedCharactersView ablebodiedCharacter in ablebodiedCharactersViews)
        {
            ablebodiedCharacter.Disable();
            Destroy(ablebodiedCharacter.gameObject);
        }
        ablebodiedCharactersViews.Clear();
    }

    private void FillAblebodiedCharactersView(Character character)
    {
        Debug.Log("FillCharacterPlace");

        AblebodiedCharactersView ablebodiedCharactersView = Instantiate(ablebodiedCharactersViewPrefab, content);
        ablebodiedCharactersView.Initialize(_industrialStructure, character);

        Debug.Log(ablebodiedCharactersViews);
        ablebodiedCharactersViews.Add(ablebodiedCharactersView);
    }

}
