using System;

[Serializable]
public abstract class Effect
{
    public abstract void Activate();
    public abstract void Deactivate();
}

public enum EffectType
{
    Renewable,
    NonRenewable,
    Permanent
}
