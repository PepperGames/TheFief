using System;
using System.Collections;
using UnityEngine;

public abstract class EffectBox : MonoBehaviour
{
    [SerializeField] protected float _initialDuration;
    protected float _duration;
    [SerializeField] protected Sprite _sprite;
    [SerializeField] protected Character _character;
    public Sprite Sprite => _sprite;

    public abstract EffectType EffectType { get; }

    public Action<EffectBox> OnEnd;

    public float Duration
    {
        get { return _duration; }
        protected set { _duration = value; }
    }

    public virtual void Activate(Character character)
    {
        _character = character;
    }

    protected abstract IEnumerator Timer();
    public abstract void Deactivate();
}
