using Zenject;

public class ProjectileSlowdownEffectBox : RenewableEffectBox
{
    protected ProjectileSlowdownEffect _projectileSlowdownEffect;
    [Inject] readonly ProjectileSlowdownEffect.Factory _projectileSlowdownEffectFactory;

    public override void Activate()
    {
        _projectileSlowdownEffect = _projectileSlowdownEffectFactory.Create();
        _projectileSlowdownEffect.Activate();
        base.Activate();
    }

    public override void Deactivate()
    {
        _projectileSlowdownEffect.Deactivate();
        base.Deactivate();
    }
}
