using System.Collections.Generic;
using UnityEngine;

public class CharacterTraitsManager : MonoBehaviour
{
    [SerializeField] private List<CharacterTrait> _characterTrait = new List<CharacterTrait>();
    [SerializeField] private Character _character;

    public List<CharacterTrait> CharacterTraits => _characterTrait;

    public void InitializeTrait(CharacterTrait characterTraitPrefab)
    {
        AddTraitToList(characterTraitPrefab);
    }
    
    public void InitializeTrait(List<CharacterTrait> characterTraitPrefabs)
    {
        foreach (var characterTraitPrefab in characterTraitPrefabs)
        {
            InitializeTrait(characterTraitPrefab);
        }
    }

    private void AddTraitToList(CharacterTrait characterTraitPrefab)
    {
        CharacterTrait characterTrait = Instantiate(characterTraitPrefab, transform);
        _characterTrait.Add(characterTrait);
        characterTrait.Initialiize(_character);
    }

    //private void RemoveTraitFromList(CharacterTrait effectBox)
    //{
    //    _characterTrait.Remove(effectBox);
    //    Destroy(effectBox.gameObject);
    //}

    //private void ClearList()
    //{
    //    _characterTrait = new List<CharacterTrait>();
    //}
}
