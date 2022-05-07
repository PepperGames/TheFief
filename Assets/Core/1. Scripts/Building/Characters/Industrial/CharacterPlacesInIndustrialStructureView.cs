using UnityEngine;

public class CharacterPlacesInIndustrialStructureView : MonoBehaviour
{
    public CharacterPlaceInIndustrialStructureView characterPlacePrefab;
    [SerializeField] private Transform content;

    [SerializeField] private Services services;

    public void Initialize(Services services, CharacterPlacesInIndustrialStructure characterPlaces)
    {
        this.services = services;

        Debug.Log("Initialize");
        
        ClearContent();
        
        Debug.Log(characterPlaces.numberOfPlaces);
        Debug.Log(characterPlaces.Characters);
        Debug.Log(characterPlaces.Characters.Count);
        
        for (int i = 0; i < characterPlaces.numberOfPlaces; i++)
        {
            if (characterPlaces.Characters.Count > i)
            {
                Character character = characterPlaces.Characters[i];
                FillCharacterPlace(character);
            }
            else
            {
                EmptyCharacterPlace();
            }
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

    private void FillCharacterPlace(Character character)
    {
        Debug.Log("FillCharacterPlace");
        CharacterPlaceInIndustrialStructureView characterPlace = Instantiate(characterPlacePrefab, content);
        characterPlace.Initialize(services, character);
    }

    private void EmptyCharacterPlace()
    {
        Debug.Log("EmptyCharacterPlace");
        CharacterPlaceInIndustrialStructureView characterPlace = Instantiate(characterPlacePrefab, content);
        characterPlace.Initialize(services);
    }
}
