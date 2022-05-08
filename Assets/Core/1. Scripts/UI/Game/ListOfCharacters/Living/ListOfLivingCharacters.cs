using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListOfLivingCharacters : MonoBehaviour
{
    [SerializeField] private CharacterManager _characterManager;

    [SerializeField] private LivingCharactersView _livingCharactersViewPrefab;
    [SerializeField] private Transform content;

    [SerializeField] private List<LivingCharactersView> _livingCharactersViews;

    private ResidentialStructure _residentialStructure;

    [SerializeField] private Button _closeButton;

    private void Start()
    {
        _closeButton.onClick.AddListener(Close);
        _characterManager.OnCharacterListChange += Initialize;
    }

    public void Open(ResidentialStructure residentialStructure)
    {
        _residentialStructure = residentialStructure;
        Initialize();
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Initialize()
    {
        ClearContent();

        foreach (Character character in _characterManager.Characters)
        {
            FillAblebodiedCharactersView(character);
        }
    }

    private void ClearContent()
    {
        foreach (LivingCharactersView ablebodiedCharacter in _livingCharactersViews)
        {
            ablebodiedCharacter.Disable();
            Destroy(ablebodiedCharacter.gameObject);
        }
        _livingCharactersViews.Clear();
    }

    private void FillAblebodiedCharactersView(Character character)
    {
        LivingCharactersView livingCharactersView = Instantiate(_livingCharactersViewPrefab, content);
        livingCharactersView.Initialize(_residentialStructure, character);

        _livingCharactersViews.Add(livingCharactersView);
    }

}
