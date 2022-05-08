using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private string characterName;
    public string CharacterName => characterName;

    [SerializeField] private Sprite portrait;
    public Sprite Portrait => portrait;   

    [SerializeField] private AiAgent aiAgent;
    public AiAgent AiAgent => aiAgent;

    [SerializeField] private Structure workplace;
    public Structure Workplace => workplace;

    [SerializeField] private Structure livingPlace;
    public Structure LivingPlace => livingPlace;

    public Action<Structure> OnCharacterAddToWorkplace;
    public Action<Structure> OnCharacterAddToLivingPlace;

    public Action<Structure> OnCharacterKickOutFromWorkplace;
    public Action<Structure> OnCharacterKickOutFromLivingPlace;

    public Action OnChangeWorkplace;
    public Action OnChangeLivingPlace;

    public void SetWorkplace(Structure workplace)
    {
        this.workplace = workplace;

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

        this.workplace = null;

        OnChangeWorkplace?.Invoke();
    }

    public void KickOutFromLivingPlace(Structure livingPlace)
    {
        OnCharacterKickOutFromLivingPlace?.Invoke(livingPlace);

        this.livingPlace = null;

        OnChangeLivingPlace?.Invoke();
    }
}
