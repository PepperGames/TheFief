using UnityEngine;
using Zenject;

public class X2ExtraPointsEffectBox : PermanentEffectBox
{
    protected X2ExtraPointsEffect _extraPointsEffect;
    [Inject] readonly X2ExtraPointsEffect.Factory _extraPointsEffectFactory;

    public override void Activate(Character character)
    {
        _extraPointsEffect = _extraPointsEffectFactory.Create();
        Debug.Log(_extraPointsEffect);
        _extraPointsEffect.Activate();
        base.Activate(character);
    }

    public override void Deactivate()
    {
        _extraPointsEffect.Deactivate();
        base.Deactivate();
    }
}
