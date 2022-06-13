using UnityEngine;

public class BumTrait : CharacterTrait
{
    private void Start()
    {
        Recheck();
    }

    protected override void Subscribe()
    {
        _character.OnChangeLivingPlace += Recheck;
    }

    private void Recheck()
    {
        Debug.Log("Recheck BumTrait");

        //Debug.Log(_character);
        //Debug.Log(_character.LivingPlace);
        //Debug.Log(_character.LivingPlace.CharacterPlaces);
        //Debug.Log(_character.LivingPlace.CharacterPlaces.OnCharacterListChange);

        if (_isActive == false)
        {
            if (_character.LivingPlace == null)
            {
                Activate();
            }
        }

        if (_isActive == true)
        {
            if (_character.LivingPlace != null)
            {
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