using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterStatuses _characterStatus = CharacterStatuses.Alive;

    [SerializeField] private CharacterData _characterData;
    [SerializeField] private CharacterTraitsManager _characterTraitsManager;
    [SerializeField] private EffectsManager _effectsManager;

    [SerializeField] private AiAgent aiAgent;
   /* [SerializeField]*/ private Structure _workPlace;
   /* [SerializeField]*/ private Structure _livingPlace;

    [SerializeField] protected CharacterInfo ui;

    public CharacterStatuses CharacterStatus => _characterStatus;

    public CharacterData CharacterData => _characterData;
    public AiAgent AiAgent => aiAgent;
    public Structure WorkPlace => _workPlace;
    public Structure LivingPlace => _livingPlace;
    public CharacterTraitsManager CharacterTraitsManager => _characterTraitsManager;
    public EffectsManager EffectsManager => _effectsManager;

    public Action<Structure> OnCharacterAddToWorkplace;
    public Action<Structure> OnCharacterAddToLivingPlace;

    public Action<Structure> OnCharacterKickOutFromWorkplace;
    public Action<Structure> OnCharacterKickOutFromLivingPlace;

    public Action OnChangeWorkplace;
    public Action OnChangeLivingPlace;

    public Action<Character> OnDie;
    public Action<Character> OnLeaveFromTown;

    public void Initialize(CharacterData characterData)
    {
        _characterStatus = CharacterStatuses.Alive;

        _characterData = characterData;
        characterData.FamilyTies.SetYourselfUpAsAChild(this);

        _characterData.Age.OnDeathFromOldAge += Die;
        _characterData.Happiness.OnLowHappiness += LeaveFromTown;
    }

    public virtual void OnMouseDown()
    {
        if (ui == null)
            return;

        ui.ShowOrHide();
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

    private void KickOutFromAll()
    {
        if (LivingPlace != null)
        {
            LivingPlace.CharacterPlaces.KickOut(this);

        }

        if (WorkPlace != null)
        {
            WorkPlace.CharacterPlaces.KickOut(this);
        }
    }

    public void Die()
    {
        Debug.Log("Die");
        _characterStatus = CharacterStatuses.Dead;

        KickOutFromAll();

        OnEventsUnscribe();

        gameObject.SetActive(false);
    }

    public void LeaveFromTown()
    {
        Debug.Log("LeaveFromTown");
        _characterStatus = CharacterStatuses.LeftTown;

        KickOutFromAll();

        OnEventsUnscribe();

        OnLeaveFromTown?.Invoke(this);

        gameObject.SetActive(false);
    }

    private void OnEventsUnscribe()
    {
        _characterData.Age.OnDeathFromOldAge -= Die;
        _characterData.Happiness.OnLowHappiness -= LeaveFromTown;

    }
}

public enum CharacterStatuses
{
    Alive = 0,
    Dead = 1,
    LeftTown = 2
}
