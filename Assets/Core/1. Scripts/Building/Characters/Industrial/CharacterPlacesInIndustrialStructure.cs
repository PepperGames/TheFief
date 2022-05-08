using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlacesInIndustrialStructure : MonoBehaviour
{
    [SerializeField] private Structure structure;

    [SerializeField] private List<Character> characters;
    public List<Character> Characters => characters;

    public int numberOfPlaces = 3;

    public Action OnCharacterListChange;

    private void Start()
    {
        characters = new List<Character>();
    }

    public void AddCharacter(Character character)
    {
        Debug.Log("AddCharacter");
        characters.Add(character);
        character.SetWorkplace(structure);
        OnCharacterListChange?.Invoke();
    }

    public void KickOut(Character character)
    {
        Debug.Log("KickOut");
        Debug.Log(character);
        Debug.Log(structure);
        characters.Remove(character);
        Debug.Log(character);
        Debug.Log(structure);
        character.KickOutFromWorkplace(structure);
        OnCharacterListChange?.Invoke();

    }
}