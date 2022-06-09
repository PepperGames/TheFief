using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class CharacterAttractor : MonoBehaviour
{
    [Inject] private Services services;

    [Range(0, 1)] [SerializeField] private float _baseAttractionRate = 0.05f;
    public float acquiredAttractionRate = 0;

    public float AttractionRate => _baseAttractionRate + acquiredAttractionRate;

    private void Start()
    {
        InGameTime.OnHourChange += TryToAttract;
    }

    private void TryToAttract()
    {
        float chance = Random.Range(0, 1.5f);
        if (chance <= AttractionRate)
        {
            services.UIController.AlertList.CreateAttractedToTownCharacterPopup(services);
        }
    }

    private void OnDestroy()
    {
        InGameTime.OnHourChange += TryToAttract;
    }
}
