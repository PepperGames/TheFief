using UnityEngine;
using Zenject;

public class ExtraLifeEffectBox : PermanentEffectBox
{
    protected ExtraLifeEffect _extraLifeEffect;
    [Inject] readonly ExtraLifeEffect.Factory _extraLifeEffectFactory;
    [SerializeField] private int _lives;

    public int Lives => _lives;

    public override void Activate(Character character)
    {
        _extraLifeEffect = _extraLifeEffectFactory.Create();
        Debug.Log(_extraLifeEffect);
        _extraLifeEffect.Activate(Lives);
        base.Activate(character);
    }

    public override void Deactivate()
    {
        _extraLifeEffect.Deactivate();
        base.Deactivate();
    }
}