using System;

public abstract class ResidentialStructure : Structure
{
    public Action<ResidentialStructure> OnDemolishAction;

    public override void Upgrade()
    {
        if (CanBeUpgrade())
        {
            base.Upgrade();
        }
    }

    protected override void OnDemolish()
    {
        base.OnDemolish();
        OnDemolishAction?.Invoke(this);
    }
}
