using Zenject;

public class NegativePointsEffectBox : RenewableEffectBox
{
    protected NegativePointsEffect _negativePointsEffect;
    [Inject] readonly NegativePointsEffect.Factory _negativePointsEffectFactory;

    public override void Activate(Character character)
    {
        _negativePointsEffect = _negativePointsEffectFactory.Create();
        _negativePointsEffect.Activate();
        base.Activate(character);
    }

    public override void Deactivate()
    {
        _negativePointsEffect.Deactivate();
        base.Deactivate();
    }
}
