using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class CharacterAttractor : MonoBehaviour
{
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
            services.UIController.AlertList.CreateAttractedToTownCharacterPopup(services);
        }
    }

    private void OnDestroy()
    {
        InGameTime.OnHourChange += TryToAttract;
    }
}
