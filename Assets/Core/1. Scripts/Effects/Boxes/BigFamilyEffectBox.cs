public class BigFamilyEffectBox : PermanentEffectBox
{
    public override void Deactivate()
    {
        _character.CharacterData.Happiness.IndexOfHappiness -= 20;
        base.Deactivate();
    }

    public override void Activate(Character character)
    {
        base.Activate(character);
        _character.CharacterData.Happiness.IndexOfHappiness += 20;
    }
}
