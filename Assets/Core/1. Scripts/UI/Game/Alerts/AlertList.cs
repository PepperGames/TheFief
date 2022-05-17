using UnityEngine;

public class AlertList : MonoBehaviour
{
    [SerializeField] protected Transform content;

    [Header("CharacterManager")]
    [SerializeField] private DeathPopup _deathPopupPrefab;
    [SerializeField] private NewCharacterInTownPopup _newCharacterInTownPopupPrefabs;
    [SerializeField] private BabyWasBornPopup _babyWasBornPopupPrefabs;
    [SerializeField] private LeaveFromTownPopup _leaveFromTownPopupPrefabs;

    [Header("CharacterAttractor")]
    [SerializeField] private AttractedToTownCharacterPopup _attractedToTownCharacterPopupPrefab;

    public void ClearContent()
    {
        foreach (Transform character in content)
        {
            Destroy(character.gameObject);
        }
    }

    public GameObject Create(GameObject popupItem)
    {
        GameObject popup = Instantiate(popupItem, content);
        return popup;
    }

    public NewCharacterInTownPopup CreateNewCharacterInTownPopup(CharacterData characterData)
    {
        NewCharacterInTownPopup newCharacterInTownPopup = Instantiate(_newCharacterInTownPopupPrefabs, content);
        newCharacterInTownPopup.Initialize(characterData);
        return newCharacterInTownPopup;
    }

    public DeathPopup CreateDeathPopup(Character character)
    {
        DeathPopup deathPopup = Instantiate(_deathPopupPrefab, content);
        deathPopup.Initialize(character.CharacterData.Portrait, character.CharacterData.CharacterName, character.CharacterData.Age.years);
        return deathPopup;
    }

    public LeaveFromTownPopup CreateLeaveFromTownPopup(Character character)
    {
        LeaveFromTownPopup leaveFromTownPopup = Instantiate(_leaveFromTownPopupPrefabs, content);
        leaveFromTownPopup.Initialize(character.CharacterData.Portrait, character.CharacterData.CharacterName);
        return leaveFromTownPopup;
    }

    public AttractedToTownCharacterPopup CreateAttractedToTownCharacterPopup(Services services)
    {
        AttractedToTownCharacterPopup attractedToTownCharacter = Instantiate(_attractedToTownCharacterPopupPrefab, content);
        attractedToTownCharacter.Initialize(services, services.CharacterManager.GenerateRandomCharacterData());
        return attractedToTownCharacter;
    }

    public BabyWasBornPopup CreateBabyWasBornPopup(CharacterData motherData, CharacterData fatherData, CharacterData childData)
    {
        BabyWasBornPopup babyWasBornPopup = Instantiate(_babyWasBornPopupPrefabs, content);
        babyWasBornPopup.Initialize(motherData, fatherData, childData);
        return babyWasBornPopup;
    }
}
