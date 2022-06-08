using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterTraitsGenerator : MonoBehaviour
{
    [SerializeField] private List<CharacterTrait> _characterTraits;

    public CharacterTrait GetCharacterTrait()
    {
        int randomIndex = Random.Range(0, _characterTraits.Count);
        CharacterTrait trait = _characterTraits[randomIndex];

        return trait;
    }

    public List<CharacterTrait> GetCharacterTraits(int count)
    {
        List<CharacterTrait> result = new List<CharacterTrait>();

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, _characterTraits.Count);
            CharacterTrait trait = _characterTraits[randomIndex];

            result.Add(trait);
        }

        return result;
    }
}
