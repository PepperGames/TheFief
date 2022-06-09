using UnityEngine;

public class LonerTrait : CharacterTrait
{
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
        Debug.Log("Recheck LonerTrait");
        if (_isActive == false)
        {
            //Debug.Log(_character);
            //Debug.Log(_character.LivingPlace);
            //Debug.Log(_character.LivingPlace.CharacterPlaces);
            //Debug.Log(_character.LivingPlace.CharacterPlaces.OnCharacterListChange);
            if (_character.LivingPlace != null)
            {
                if (_character.LivingPlace.CharacterPlaces.Characters.Count >= 10)
                {
                    Activate();
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
                if (_character.LivingPlace.CharacterPlaces.Characters.Count < 10)
                {
                    Deactivate();
                }
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