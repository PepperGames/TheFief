using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterData _characterData;
    [SerializeField] private AiAgent aiAgent;
    [SerializeField] private Structure workPlace;
    [SerializeField] private Structure livingPlace;

    public CharacterData CharacterData => _characterData;
    public AiAgent AiAgent => aiAgent;
    public Structure WorkPlace => workPlace;
    public Structure LivingPlace => livingPlace;

    public Action<Structure> OnCharacterAddToWorkplace;
    public Action<Structure> OnCharacterAddToLivingPlace;

    public Action<Structure> OnCharacterKickOutFromWorkplace;
    public Action<Structure> OnCharacterKickOutFromLivingPlace;

    public Action OnChangeWorkplace;
    public Action OnChangeLivingPlace;

    public Action<Character> OnDie;

    public void Initialize(CharacterData characterData )
    {
        _characterData = characterData;
    }

    public void SetWorkplace(Structure workplace)
    {
        this.workPlace = workplace;

        OnChangeWorkplace?.Invoke();
        OnCharacterAddToWorkplace?.Invoke(workplace);
    }

    public void SetLivingPlace(Structure livingPlace)
    {
        this.livingPlace = livingPlace;

        OnChangeLivingPlace?.Invoke();
        OnCharacterAddToLivingPlace?.Invoke(livingPlace);
    }

    public void KickOutFromWorkplace(Structure workplace)
    {
        OnCharacterKickOutFromWorkplace?.Invoke(workplace);

        this.workPlace = null;

        OnChangeWorkplace?.Invoke();
    }

    public void KickOutFromLivingPlace(Structure livingPlace)
    {
        OnCharacterKickOutFromLivingPlace?.Invoke(livingPlace);

        this.livingPlace = null;

        OnChangeLivingPlace?.Invoke();
    }

    public void Die()
    {
        LivingPlace.CharacterPlaces.KickOut(this);
        WorkPlace.CharacterPlaces.KickOut(this);

        OnDie?.Invoke(this);
    }
}
