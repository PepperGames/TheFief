using System.Collections;
using UnityEngine;

public class NonRenewableEffectBox : EffectBox
{
    public override EffectType EffectType => EffectType.NonRenewable;

    protected Coroutine _executable;

    public override void Activate(Character character)
    {
        base.Activate(character);
        _duration = _initialDuration;

        _executable = StartCoroutine(Timer());
    }

    protected override IEnumerator Timer()
    {
        while (_duration > 0)
        {
                _duration -= Time.deltaTime;
            yield return null;
        }
        Deactivate();
    }

    public override void Deactivate()
    {
        if (_executable != null)
        {
            StopCoroutine(_executable);
        }
        OnEnd?.Invoke(this);
    }
}
