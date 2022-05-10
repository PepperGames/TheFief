using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterData _characterData;
    [SerializeField] private AiAgent aiAgent;
    [SerializeField] private Structure _workPlace;
    [SerializeField] private Structure _livingPlace;

    public CharacterData CharacterData => _characterData;
    public AiAgent AiAgent => aiAgent;
    public Structure WorkPlace => _workPlace;
    public Structure LivingPlace => _livingPlace;

    public Action<Structure> OnCharacterAddToWorkplace;
    public Action<Structure> OnCharacterAddToLivingPlace;

    public Action<Structure> OnCharacterKickOutFromWorkplace;
    public Action<Structure> OnCharacterKickOutFromLivingPlace;

    public Action OnChangeWorkplace;
    public Action OnChangeLivingPlace;

    public Action<Character> OnDie;

    public void Initialize(CharacterData characterData)
    {
        _characterData = characterData;
        _characterData.Age.OnDeathFromOldAge += Die;
    }

    public void SetWorkplace(Structure workplace)
    {
        _workPlace = workplace;

        OnChangeWorkplace?.Invoke();
        OnCharacterAddToWorkplace?.Invoke(workplace);
    }

    public void SetLivingPlace(Structure livingPlace)
    {
        _livingPlace = livingPlace;

        OnChangeLivingPlace?.Invoke();
        OnCharacterAddToLivingPlace?.Invoke(livingPlace);
    }

    public void KickOutFromWorkplace(Structure workplace)
    {
        OnCharacterKickOutFromWorkplace?.Invoke(workplace);

        _workPlace = null;

        OnChangeWorkplace?.Invoke();
    }

    public void KickOutFromLivingPlace(Structure livingPlace)
    {
        OnCharacterKickOutFromLivingPlace?.Invoke(livingPlace);

        _livingPlace = null;

        OnChangeLivingPlace?.Invoke();
    }

    public void Die()
    {
        Debug.Log("Die");

        if (LivingPlace != null)
        {
            LivingPlace.CharacterPlaces.KickOut(this);

        }

        if (WorkPlace != null)
        {
            WorkPlace.CharacterPlaces.KickOut(this);
        }

        _characterData.Age.OnDeathFromOldAge -= Die;

        OnDie?.Invoke(this);

        Destroy(gameObject);
    }
}
