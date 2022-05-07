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

    public void SetWorkplace(Structure workplace)
    {
        this.workplace = workplace;
        OnCharacterAddToWorkplace?.Invoke(workplace);
    }

    public void SetLivingPlace(Structure livingPlace)
    {
        this.livingPlace = livingPlace;
        OnCharacterAddToLivingPlace?.Invoke(livingPlace);
    }

    public void KickOutFromWorkplace(Structure workplace)
    {
        this.workplace = null;
        OnCharacterAddToWorkplace?.Invoke(workplace);
    }

    public void KickOutFromLivingPlace(Structure livingPlace)
    {
        this.livingPlace = null;
        OnCharacterAddToLivingPlace?.Invoke(livingPlace);
    }
}
