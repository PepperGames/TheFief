using UnityEngine;
using Zenject;

public class ProjectileSlowdownEffect : Effect
{
    [Inject] [SerializeField] private Services _services;

    private readonly float _bonus = -3.5f;

    public override void Activate()
    {
        //_globalValues.BonusSpeed += _bonus;
    }

    public override void Deactivate()
    {
        //_globalValues.BonusSpeed -= _bonus;
    }

    public class Factory : PlaceholderFactory<ProjectileSlowdownEffect>
    {
    }
}
