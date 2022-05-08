using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterPlacesInStructure : MonoBehaviour
{
    [SerializeField] protected Structure structure;

    [SerializeField] protected List<Character> characters;
    public List<Character> Characters => characters;

    public int numberOfPlaces = 3;

    public Action OnCharacterListChange;

    protected void Start()
    {
        characters = new List<Character>();
    }

    public abstract bool AddCharacter(Character character);

    public abstract void KickOut(Character character);

    public abstract void KickOutAll();
}
