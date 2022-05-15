using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterPlacesInStructure : MonoBehaviour
{
    [SerializeField] protected Structure structure;

    [SerializeField] protected List<Character> characters;

    protected int _numberOfPlaces = 3;

    public List<Character> Characters => characters;
    public int NumberOfPlaces => _numberOfPlaces;

    public Action OnCharacterListChange;
    public Action OnCNumberOfPlacesChange;

    protected void Start()
    {
        characters = new List<Character>();
    }

    public void IncreaseNumberOfPlaces(int additionalQuantity)
    {
        _numberOfPlaces += additionalQuantity;
        OnCNumberOfPlacesChange?.Invoke();
    }

    public abstract bool AddCharacter(Character character);

    public abstract void KickOut(Character character);

    public abstract void KickOutAll();
}
