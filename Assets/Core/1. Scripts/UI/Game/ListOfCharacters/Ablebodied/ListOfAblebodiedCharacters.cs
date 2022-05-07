using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ListOfAblebodiedCharacters : MonoBehaviour
{
    [Inject] private CharacterManager characterManager;

    [SerializeField] private AblebodiedCharactersView ablebodiedCharactersViewPrefab;
    [SerializeField] private Transform content;

    [SerializeField] private List<AblebodiedCharactersView> ablebodiedCharactersViews;

    private IndustrialStructure industrialStructure;

    [SerializeField] private Button closeButton;

    private void Start()
    {
        closeButton.onClick.AddListener(Close);
    }

    public void Open(IndustrialStructure industrialStructure)
    {
        this.industrialStructure = industrialStructure;
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

        foreach (Character character in characterManager.Characters)
        {
            FillAblebodiedCharactersView(character);
        }
    }

    private void ClearContent()
    {
        Debug.Log("ClearContent");

        foreach (AblebodiedCharactersView child in ablebodiedCharactersViews)
        {
            ablebodiedCharactersViews.Remove(child);
            Destroy(child.gameObject);
        }
    }

    private void FillAblebodiedCharactersView(Character character)
    {
        Debug.Log("FillCharacterPlace");

        AblebodiedCharactersView ablebodiedCharactersView = Instantiate(ablebodiedCharactersViewPrefab, content);
        ablebodiedCharactersView.Initialize(character);

        ablebodiedCharactersViews.Add(ablebodiedCharactersView);
    }

}
