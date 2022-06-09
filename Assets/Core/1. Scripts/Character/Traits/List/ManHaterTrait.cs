using UnityEngine;

public class ManHaterTrait : CharacterTrait
{
    public override void Initialiize(Character character)
    {
        base.Initialiize(character);
        _character = character;
    }

    protected override void Subscribe()
    {
        _character.OnChangeLivingPlace += OnChangeLivingPlace;
    }

    private void OnChangeLivingPlace()
    {
        //Recheck();
        if (_character.LivingPlace != null)
        {
            _character.LivingPlace.CharacterPlaces.OnCharacterListChange += Recheck;
        }
    }

    private void Recheck()
    {
        Debug.Log("Recheck");
        if (_isActive == false)
        {
            //Debug.Log(_character);
            //Debug.Log(_character.LivingPlace);
            //Debug.Log(_character.LivingPlace.CharacterPlaces);
            //Debug.Log(_character.LivingPlace.CharacterPlaces.OnCharacterListChange);
            if (_character.LivingPlace != null)
            {
                foreach (var item in _character.LivingPlace.CharacterPlaces.Characters)
                {
                    if (item != _character)
                    {
                        if (item.CharacterData.Gender == Genders.Male)
                        {
                            Activate();
                            return;
                        }
                    }
                }
            }
        }

        if (_isActive == true)
        {
            if (_character.LivingPlace == null)
            {
                Deactivate();
                return;
            }
            else
            {
                foreach (var item in _character.LivingPlace.CharacterPlaces.Characters)
                {
                    if (item != _character)
                    {
                        if (item.CharacterData.Gender == Genders.Male)
                        {
                            return;
                        }
                    }
                }
                Deactivate();
            }
        }
    }

    protected override void Activate()
    {
        _isActive = true;
        _character.EffectsManager.InitializeEffect(_effectBox);
    }

    protected override void Deactivate()
    {
        _isActive = false;
        _character.EffectsManager.RemoveEffectByType(_effectBox);
    }
}
