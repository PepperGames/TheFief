using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class CharacterAttractor : MonoBehaviour
{
    [SerializeField] private CharacterManager _characterManager;
    [SerializeField] private AttractedToTownCharacterPopup _attractedToTownCharacterPopupPrefab;

    [SerializeField] private float _attractionRate = 0.1f;

    [Inject] private Services services;

    private void Start()
    {
        InGameTime.OnHourChange += TryToAttract;
    }

    private void TryToAttract()
    {
        float chance = Random.Range(0, 1f);
        if (chance <= _attractionRate)
        {
            CreateAlert();
        }
    }

    private void CreateAlert()
    {
        AttractedToTownCharacterPopup attractedToTownCharacter = services.UIController.AlertList.Create(_attractedToTownCharacterPopupPrefab.gameObject).GetComponent<AttractedToTownCharacterPopup>();
        attractedToTownCharacter.Initialize(services, _characterManager.GenerateRandomCharacterData());
    }

    private void OnDestroy()
    {
        InGameTime.OnHourChange += TryToAttract;
    }
}
