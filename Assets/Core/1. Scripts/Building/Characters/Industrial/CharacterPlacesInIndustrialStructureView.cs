using UnityEngine;

public class CharacterPlacesInIndustrialStructureView : MonoBehaviour
{
    public CharacterPlaceInIndustrialStructureView characterPlacePrefab;
    [SerializeField] private Transform content;

    [SerializeField] private Services _services;

    [SerializeField] private IndustrialStructure _industrialStructure;

    public void Initialize(Services services, IndustrialStructure industrialStructure)
    {
        _services = services;
        _industrialStructure = industrialStructure;

        OnEventsSubscribe();

        Debug.Log("Initialize");

        FillContent();
    }

    private void FillContent()
    {
        ClearContent();

        for (int i = 0; i < _industrialStructure.CharacterPlaces.numberOfPlaces; i++)
        {
            if (_industrialStructure.CharacterPlaces.Characters.Count > i)
            {
                Character character = _industrialStructure.CharacterPlaces.Characters[i];
                FillCharacterPlace(character);
            }
            else
            {
                EmptyCharacterPlace();
            }
        }
    }

    public void Disable()
    {
        OnEventsUnscribe();
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
        characterPlace.Initialize(_services, _industrialStructure, character);
    }

    private void EmptyCharacterPlace()
    {
        Debug.Log("EmptyCharacterPlace");
        CharacterPlaceInIndustrialStructureView characterPlace = Instantiate(characterPlacePrefab, content);
        characterPlace.Initialize(_services, _industrialStructure);
    }

    private void OnEventsSubscribe()
    {
        _industrialStructure.CharacterPlaces.OnCharacterListChange += FillContent;
    }

    private void OnEventsUnscribe()
    {
        _industrialStructure.CharacterPlaces.OnCharacterListChange -= FillContent;
    }
}
