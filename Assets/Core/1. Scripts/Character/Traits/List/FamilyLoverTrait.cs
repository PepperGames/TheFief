using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyLoverTrait : CharacterTrait
{
    public override void Initialiize(Character character)
    {
        base.Initialiize(character);
        _character = character;

        _character.EffectsManager.InitializeEffect(_effectBox);
    }
}
