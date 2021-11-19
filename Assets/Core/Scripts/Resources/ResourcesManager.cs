using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] private Resources resources = new Resources() { Money = 100, Food = 100, Wood = 100, Stone = 100, Metal = 100 };

    public Action<float> OnMoneyChangeFor;
    public Action<float> OnFoodChangeFor;
    public Action<float> OnWoodChangeFor;
    public Action<float> OnStoneChangeFor;
    public Action<float> OnMetalChangeFor;

    public Action<Resources> OnResourcesChange;

    //private Resources testResources = new Resources() { Money = 100, Food = 100, Wood = 100, Stone = 100, Metal = 100 };
    //private void Start()
    //{
    //    resources += testResources;
    //    resources += testResources;
    //    resources += testResources;
    //    PrintDebug();
    //}

    public void AddResources(Resources addedResources)
    {
        resources += addedResources;

        if (addedResources.Money > 0)
        {
            OnMoneyChangeFor?.Invoke(addedResources.Money);
        }
        if (addedResources.Food > 0)
        {
            OnFoodChangeFor?.Invoke(addedResources.Food);
        }
        if (addedResources.Wood > 0)
        {
            OnWoodChangeFor?.Invoke(addedResources.Wood);
        }
        if (addedResources.Stone > 0)
        {
            OnStoneChangeFor?.Invoke(addedResources.Stone);
        }
        if (addedResources.Metal > 0)
        {
            OnMetalChangeFor?.Invoke(addedResources.Metal);
        }

        if (addedResources > 0)
        {
            OnResourcesChange?.Invoke(resources);
        }
    }

    public bool SpendResources(Resources neededResources)
    {
        if (NotEnoughResources(neededResources))
            return false;

        resources -= neededResources;

        if (neededResources.Money > 0)
        {
            OnMoneyChangeFor?.Invoke(-neededResources.Money);
        }
        if (neededResources.Food > 0)
        {
            OnFoodChangeFor?.Invoke(-neededResources.Food);
        }
        if (neededResources.Wood > 0)
        {
            OnWoodChangeFor?.Invoke(neededResources.Wood);
        }
        if (neededResources.Stone > 0)
        {
            OnStoneChangeFor?.Invoke(-neededResources.Stone);
        }
        if (neededResources.Metal > 0)
        {
            OnMetalChangeFor?.Invoke(-neededResources.Metal);
        }

        if (neededResources > 0)
        {
            OnResourcesChange?.Invoke(resources);
        }

        return true;
    }

    public bool EnoughResources(Resources neededResources)
    {
        return resources > neededResources;
    }

    private bool NotEnoughResources(Resources neededResources)
    {
        return !EnoughResources(neededResources);
    }

    public void PrintDebug()
    {
        Debug.Log("Money " + resources.Money + " Food " + resources.Food + "\n" +
            "Wood " + resources.Wood + " Stone " + resources.Stone + " Metal " + resources.Metal);
    }
}
