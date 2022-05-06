using System.Collections.Generic;
using UnityEngine;

public class CharacterPlacesInIndustrialStructure : MonoBehaviour
{
    [SerializeField] private Structure structure;

    private List<Character> characters;
    public List<Character> Characters => characters;

    public void AddCharacter(Character character)
    {
        characters.Add(character);
        character.SetWorkplace(structure);
    }

    public void KickOut(Character character)
    {
        characters.Remove(character);
        character.KickOutFromLivingPlace(structure);
    }

}
