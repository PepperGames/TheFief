using System.Collections;
using UnityEngine;

public abstract class RenewableEffectBox : EffectBox
{
    public override EffectType EffectType => EffectType.Renewable;

    protected Coroutine _executable;

    public override void Activate()
    {
        _duration = _initialDuration;

        _executable = StartCoroutine(Timer());
    }

    protected override IEnumerator Timer()
    {
        //Debug.Log("Execute");
        while (_duration > 0)
        {
            _duration -= Time.deltaTime;
            yield return null;
        }
        Deactivate();
    }

    public virtual void ExtendTime()
    {
        _duration += _initialDuration;
    }

    public override void Deactivate()
    {
        //Debug.Log("Deactivate");
        StopCoroutine(_executable);
        OnEnd?.Invoke(this);
    }
}
