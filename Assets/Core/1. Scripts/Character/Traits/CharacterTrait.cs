using UnityEngine;

public abstract class CharacterTrait : MonoBehaviour
{
    [SerializeField] protected Sprite _sprite;
    [SerializeField] protected Character _character;
    [SerializeField] protected EffectBox _effectBox;

    protected bool _isActive = false;

    public bool visible = true;

    public Sprite Sprite => _sprite;

    public virtual void Initialiize(Character character)
    {
        _character = character;
        Subscribe();
    }

    protected abstract void Subscribe();
    protected abstract void Activate();
    protected abstract void Deactivate();
}
