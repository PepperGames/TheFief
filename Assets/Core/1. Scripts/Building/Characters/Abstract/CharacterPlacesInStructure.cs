using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterPlacesInStructure : MonoBehaviour
{
    [SerializeField] protected Structure structure;

    [SerializeField] protected int _numberOfPlaces = 3;

    [SerializeField] protected List<Character> characters;

    public List<Character> Characters => characters;
    public int NumberOfPlaces
    {
        get { return _numberOfPlaces; }
        set {
            int oldNumberOfPlaces = _numberOfPlaces;
            _numberOfPlaces = value;
            if (oldNumberOfPlaces!= _numberOfPlaces)
            {
                OnNumberOfPlacesChange?.Invoke();
            }
        }
    }

    public Action OnCharacterListChange;
    public Action OnNumberOfPlacesChange;

    protected void Start()
    {
        characters = new List<Character>();
    }

    public abstract bool AddCharacter(Character character);

    public abstract void KickOut(Character character);

    public abstract void KickOutAll();
}
