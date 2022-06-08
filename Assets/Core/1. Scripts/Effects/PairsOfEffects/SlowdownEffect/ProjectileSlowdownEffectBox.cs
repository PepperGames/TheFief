using Zenject;

public class ProjectileSlowdownEffectBox : RenewableEffectBox
{
    protected ProjectileSlowdownEffect _projectileSlowdownEffect;
    [Inject] readonly ProjectileSlowdownEffect.Factory _projectileSlowdownEffectFactory;

    public override void Activate(Character character)
    {
        _projectileSlowdownEffect = _projectileSlowdownEffectFactory.Create();
        _projectileSlowdownEffect.Activate();
        base.Activate(character);
    }

    public override void Deactivate()
    {
        _projectileSlowdownEffect.Deactivate();
        base.Deactivate();
    }
}
