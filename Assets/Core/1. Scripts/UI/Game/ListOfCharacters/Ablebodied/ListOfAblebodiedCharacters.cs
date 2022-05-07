using UnityEngine;
using Zenject;

public class ListOfAblebodiedCharacters : MonoBehaviour
{
    [SerializeField] private AblebodiedCharactersView ablebodiedCharactersViewPrefab;
    [SerializeField] private Transform content;
    [Inject] [SerializeField] private CharacterManager characterManager;

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
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    private void FillAblebodiedCharactersView(Character character)
    {
        Debug.Log("FillCharacterPlace");
        AblebodiedCharactersView ablebodiedCharactersView = Instantiate(ablebodiedCharactersViewPrefab, content);
        ablebodiedCharactersView.Initialize(character);
    }

}
