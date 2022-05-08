using UnityEngine;

public class CharacterPlacesInStructureView : MonoBehaviour
{
    [SerializeField] protected CharacterPlaceInStructureView _characterPlaceInStructureViewPrefab;
    [SerializeField] protected Transform content;

    protected Services _services;

    protected Structure _structure;

    public virtual void Initialize(Services services, Structure structure)
    {
        _services = services;
        _structure = structure;

        OnEventsSubscribe();

        FillContent();
    }

    protected virtual void FillContent()
    {
        ClearContent();

        for (int i = 0; i < _structure.CharacterPlaces.numberOfPlaces; i++)
        {
            if (_structure.CharacterPlaces.Characters.Count > i)
            {
                Character character = _structure.CharacterPlaces.Characters[i];
                FillCharacterPlace(character);
            }
            else
            {
                EmptyCharacterPlace();
            }
        }
    }

    public virtual void Disable()
    {
        OnEventsUnscribe();
    }

    protected virtual void ClearContent()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    protected virtual void FillCharacterPlace(Character character)
    {
        CharacterPlaceInStructureView characterPlace = Instantiate(_characterPlaceInStructureViewPrefab, content);
        characterPlace.Initialize(_services, _structure, character);
    }

    protected virtual void EmptyCharacterPlace()
    {
        CharacterPlaceInStructureView characterPlace = Instantiate(_characterPlaceInStructureViewPrefab, content);
        characterPlace.Initialize(_services, _structure);
    }

    protected virtual void OnEventsSubscribe()
    {
        _structure.CharacterPlaces.OnCharacterListChange += FillContent;
    }

    protected virtual void OnEventsUnscribe()
    {
        _structure.CharacterPlaces.OnCharacterListChange -= FillContent;
    }
}
