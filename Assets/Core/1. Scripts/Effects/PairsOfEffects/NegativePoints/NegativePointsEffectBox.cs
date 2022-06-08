using Zenject;

public class NegativePointsEffectBox : RenewableEffectBox
{
    protected NegativePointsEffect _negativePointsEffect;
    [Inject] readonly NegativePointsEffect.Factory _negativePointsEffectFactory;

    public override void Activate()
    {
        _negativePointsEffect = _negativePointsEffectFactory.Create();
        _negativePointsEffect.Activate();
        base.Activate();
    }

    public override void Deactivate()
    {
        _negativePointsEffect.Deactivate();
        base.Deactivate();
    }
}
