using UnityEngine;

public class CharacterPlacesInResidentialStructure : CharacterPlacesInStructure
{
    public override bool AddCharacter(Character character)
    {
        Debug.Log("AddCharacter");
        if (Characters.Count < NumberOfPlaces)
        {
            characters.Add(character);
            character.SetLivingPlace(structure);
            OnCharacterListChange?.Invoke();
            return true;
        }
        return false;
    }

    public override void KickOut(Character character)
    {
        Debug.Log("KickOut");

        if (character.LivingPlace != null)
        {
            characters.Remove(character);
            character.KickOutFromLivingPlace(structure);

            OnCharacterListChange?.Invoke();
        }
    }

    public override void KickOutAll()
    {
        Debug.Log("KickOutAll");

        foreach (Character character in characters)
        {
            character.KickOutFromLivingPlace(structure);
        }

        characters.Clear();
        OnCharacterListChange?.Invoke();
    }
}
