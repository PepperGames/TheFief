public class FamilyLoverTrait : CharacterTrait
{
    protected override void Subscribe()
    {
        _character.CharacterData.FamilyTies.OnFamilyChange += Recheck;
    }

    private void Recheck()
    {
        if (_isActive == false)
        {
            if (_character.CharacterData.FamilyTies.FamilySize >= 5)
            {
                Activate();
            }
        }

        if (_isActive == true)
        {
            if (_character.CharacterData.FamilyTies.FamilySize < 5)
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
