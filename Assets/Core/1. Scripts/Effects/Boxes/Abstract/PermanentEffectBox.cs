using System.Collections;
using UnityEngine;

public class PermanentEffectBox : EffectBox
{
    public override EffectType EffectType => EffectType.Permanent;

    protected Coroutine _executable;

    public override void Deactivate()
    {
        Debug.Log("Deactivate");
        OnEnd?.Invoke(this);
    }

    public override void Activate(Character character)
    {
        base.Activate(character);
        _duration = 1;
    }

    protected override IEnumerator Timer()
    {
        yield return null;
    }
}
