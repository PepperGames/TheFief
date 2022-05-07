using System.Collections.Generic;
using UnityEngine;

public class CharacterPlacesInIndustrialStructure : MonoBehaviour
{
    [SerializeField] private Structure structure;

    [SerializeField] private List<Character> characters;
    public List<Character> Characters => characters;

    public int numberOfPlaces = 3;

    private void Start()
    {
        characters = new List<Character>();
    }

    public void AddCharacter(Character character)
    {
        Debug.Log("AddCharacter");
        characters.Add(character);
        character.SetWorkplace(structure);
    }

    public void KickOut(Character character)
    {
        Debug.Log("KickOut");
        characters.Remove(character);
        character.KickOutFromLivingPlace(structure);
    }
}
