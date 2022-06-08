using UnityEngine;

public class AntiMissileEffectBox : NonRenewableEffectBox
{
    [SerializeField] protected GameObject _antiMissileTurretPrefab;
    protected GameObject _antiMissileTurret;

    public override void Activate()
    {
        _antiMissileTurret = Instantiate(_antiMissileTurretPrefab, new Vector2(-2.5f, -4f), Quaternion.identity);
        base.Activate();
    }

    public override void Deactivate()
    {
        Destroy(_antiMissileTurret);
        base.Deactivate();
    }
}
