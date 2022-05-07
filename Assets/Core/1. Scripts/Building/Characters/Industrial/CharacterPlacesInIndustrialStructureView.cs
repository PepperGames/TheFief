using UnityEngine;

public class CharacterPlacesInIndustrialStructureView : MonoBehaviour
{
    public CharacterPlaceInIndustrialStructureView characterPlacePrefab;
    [SerializeField] private Transform content;

    [SerializeField] private Services services;

    [SerializeField] private IndustrialStructure industrialStructure;
    public void Initialize(Services services, IndustrialStructure industrialStructure)
    {
        this.services = services;
        this.industrialStructure = industrialStructure;

        Debug.Log("Initialize");

        ClearContent();

        //Debug.Log(characterPlaces.numberOfPlaces);
        //Debug.Log(characterPlaces.Characters);
        //Debug.Log(characterPlaces.Characters.Count);

        for (int i = 0; i < industrialStructure.CharacterPlaces.numberOfPlaces; i++)
        {
            if (industrialStructure.CharacterPlaces.Characters.Count > i)
            {
                Character character = industrialStructure.CharacterPlaces.Characters[i];
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
        characterPlace.Initialize(services, industrialStructure, character);
    }

    private void EmptyCharacterPlace()
    {
        Debug.Log("EmptyCharacterPlace");
        CharacterPlaceInIndustrialStructureView characterPlace = Instantiate(characterPlacePrefab, content);
        characterPlace.Initialize(services, industrialStructure);
    }
}
