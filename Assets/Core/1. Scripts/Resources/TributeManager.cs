using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TributeManager : MonoBehaviour
{
    [SerializeField] private Resources accumulatedResources;

    [SerializeField] float _peasantsTribute;
    [SerializeField] float _merchantsTribute;
    [SerializeField] float _priestsTribute;
    [SerializeField] float _peersTribute;

    [Inject] private Services _services;

    private void Start()
    {
        InGameTime.OnHourChange += TryProduceResource;
    }

    public virtual void TryProduceResource()
    {
        if (InGameTime.Hour == 21)
        {
            ProduceResource();
            IssueAccumulatedResource();
        }
    }

    private void IssueAccumulatedResource()
    {
        _services.ResourcesManager.AddResources(accumulatedResources);
        accumulatedResources.Reset();
    }

    private void ProduceResource()
    {
        accumulatedResources += new Resources { Money = CalculateProductivity() };
    }

    private float CalculateProductivity()
    {
        float result = 0;

        foreach (Character characters in _services.CharacterManager.AllCharacters)
        {
            float t = GetQuantityFromEstate(characters.CharacterData.Estates);
            result += t;
        }

        return result;
    }

    private float GetQuantityFromEstate(Estates estates)
    {
        switch (estates)
        {
            case Estates.Peasants:
                return _peasantsTribute;

            case Estates.Merchants:
                return _merchantsTribute;

            case Estates.Priests:
                return _priestsTribute;

            case Estates.Peers:
                return _peersTribute; //Приносят золото, при наличии крестьян. = (количество крестьян / количество пэров)*сколько золота приносит один пэр.

            default:
                return 0;
        }
    }

    private void OnDestroy()
    {
        InGameTime.OnHourChange -= TryProduceResource;
    }
}
