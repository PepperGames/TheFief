using UnityEngine;
using Zenject;

public class ExtraPointsEffectBox : RenewableEffectBox
{
    protected ExtraPointsEffect _extraPointsEffect;
    [Inject] readonly ExtraPointsEffect.Factory _extraPointsEffectFactory;

    public override void Activate()
    {
        _extraPointsEffect = _extraPointsEffectFactory.Create();
        Debug.Log(_extraPointsEffect);
        _extraPointsEffect.Activate();
        base.Activate();
    }

    public override void Deactivate()
    {
        _extraPointsEffect.Deactivate();
        base.Deactivate();
    }
}
