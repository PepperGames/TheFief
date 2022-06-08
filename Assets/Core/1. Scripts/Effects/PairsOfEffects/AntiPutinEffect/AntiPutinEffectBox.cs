using UnityEngine;

public class AntiPutinEffectBox : NonRenewableEffectBox
{
    [SerializeField] protected GameObject _antiPutinTurretPrefab;
    protected GameObject _antiPutinTurret;

    public override void Activate()
    {
        _antiPutinTurret = Instantiate(_antiPutinTurretPrefab, new Vector2(2.5f, -4f), Quaternion.identity);
        base.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}
