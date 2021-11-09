using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] private Resources resources = new Resources() { Money = 100, Food = 100, Wood = 100, Stone = 100, Metal = 100 };

    public Action<float> OnMoneyChange;
    public Action<float> OnFoodChange;
    public Action<float> OnWoodChange;
    public Action<float> OnStoneChange;
    public Action<float> OnMetalChange;

    public Action OnResourcesChange;

    private void Start()
    {
        PrintDebug();
    }

    public void AddResources(Resources newResources)
    {

    }

    public void PrintDebug()
    {
        Debug.Log("Money " + resources.Money + " Food " + resources.Food + "\n" +
            "Wood " + resources.Wood + " Stone " + resources.Stone + " Metal " + resources.Metal);
    }
}
