using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlacesInResidentialStructureView : MonoBehaviour
{
    public CharacterPlaceInResidentialStructureView characterPlacePrefab;
    [SerializeField] private Transform content;

    [SerializeField] private Services _services;

    [SerializeField] private ResidentialStructure _residentialStructure;

    public void Initialize(Services services, ResidentialStructure residentialStructure)
    {
        _services = services;
        _residentialStructure = residentialStructure;

        OnEventsSubscribe();

        FillContent();
    }

    private void FillContent()
    {
        ClearContent();

        for (int i = 0; i < _residentialStructure.CharacterPlaces.numberOfPlaces; i++)
        {
            if (_residentialStructure.CharacterPlaces.Characters.Count > i)
            {
                Character character = _residentialStructure.CharacterPlaces.Characters[i];
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
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    private void FillCharacterPlace(Character character)
    {
        CharacterPlaceInResidentialStructureView characterPlace = Instantiate(characterPlacePrefab, content);
        characterPlace.Initialize(_services, _residentialStructure, character);
    }

    private void EmptyCharacterPlace()
    {
        CharacterPlaceInResidentialStructureView characterPlace = Instantiate(characterPlacePrefab, content);
        characterPlace.Initialize(_services, _residentialStructure);
    }

    private void OnEventsSubscribe()
    {
        _residentialStructure.CharacterPlaces.OnCharacterListChange += FillContent;
    }

    private void OnEventsUnscribe()
    {
        _residentialStructure.CharacterPlaces.OnCharacterListChange -= FillContent;
    }
}
