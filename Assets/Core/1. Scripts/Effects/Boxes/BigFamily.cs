public class BigFamily : PermanentEffectBox
{
    public override EffectType EffectType => EffectType.Permanent;

    public override void Deactivate()
    {
        _character.CharacterData.Happiness.IndexOfHappiness -= 10;
        base.Deactivate();
    }

    public override void Activate(Character character)
    {
        base.Activate(character);
        _character.CharacterData.Happiness.IndexOfHappiness += 100;
    }
}
