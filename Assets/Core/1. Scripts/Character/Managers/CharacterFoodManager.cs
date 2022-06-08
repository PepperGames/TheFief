using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterFoodManager : MonoBehaviour
{
    [Inject] private Services services;
    [SerializeField] private float _attractionRate = 0.1f;

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
