using UnityEngine;

public abstract class CharacterTrait : MonoBehaviour
{
    [SerializeField] protected Sprite _sprite;
    [SerializeField] protected Character _character;
    [SerializeField] protected EffectBox _effectBox;
    public Sprite Sprite => _sprite;

    public virtual void Initialiize(Character character)
    {
        _character = character;
    }
}
