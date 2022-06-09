public class LossOfAFamilyMemberEffectBox : NonRenewableEffectBox
{
    public override void Activate(Character character)
    {
        base.Activate(character);
        _character.CharacterData.Happiness.IndexOfHappiness -= 10;
    }

    public override void Deactivate()
    {
        _character.CharacterData.Happiness.IndexOfHappiness += 10;
        base.Deactivate();
    }
}