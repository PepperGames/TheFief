using UnityEngine;

public class CharacterPlacesInIndustrialStructure : CharacterPlacesInStructure
{
    public override bool AddCharacter(Character character)
    {
        Debug.Log("AddCharacter");
        if (Characters.Count < numberOfPlaces)
        {
            characters.Add(character);
            character.SetWorkplace(structure);
            OnCharacterListChange?.Invoke();
            return true;
        }
        return false;
    }

    public override void KickOut(Character character)
    {
        Debug.Log("KickOut");

        if (character.WorkPlace != null)
        {
            characters.Remove(character);
            character.KickOutFromWorkplace(structure);

            OnCharacterListChange?.Invoke();
        }
    }

    public override void KickOutAll()
    {
        Debug.Log("KickOutAll");
        foreach (Character character in characters)
        {
            character.KickOutFromWorkplace(structure);
        }
        characters.Clear();
        OnCharacterListChange?.Invoke();
    }
}
