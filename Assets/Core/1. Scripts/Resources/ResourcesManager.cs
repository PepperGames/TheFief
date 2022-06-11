using System;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] private Resources _resources;
    [SerializeField] private Resources _maxResources;

    public Action<float> OnMoneyChangeFor;
    public Action<float> OnFoodChangeFor;
    public Action<float> OnWoodChangeFor;
    public Action<float> OnStoneChangeFor;
    public Action<float> OnMetalChangeFor;

    public Resources Resources => _resources;

    public Action<Resources> OnResourcesChange;

    public void AddResources(Resources addedResources)
    {
        _resources += addedResources;

        FixOnMax();


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
            OnResourcesChange?.Invoke(_resources);
        }
    }

    private void FixOnMax()
    {
        if (_resources.Money > _maxResources.Money)
        {
            _resources.Money = _maxResources.Money;
        }

        if (_resources.Food > _maxResources.Food)
        {
            _resources.Food = _maxResources.Food;
        }

        if (_resources.Wood > _maxResources.Wood)
        {
            _resources.Wood = _maxResources.Wood;
        }

        if (_resources.Stone > _maxResources.Stone)
        {
            _resources.Stone = _maxResources.Stone;
        }

        if (_resources.Metal > _maxResources.Metal)
        {
            _resources.Metal = _maxResources.Metal;
        }
    }

    public bool SpendResources(Resources neededResources)
    {
        if (NotEnoughResources(neededResources))
            return false;

        _resources -= neededResources;

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
            OnResourcesChange?.Invoke(_resources);
        }

        return true;
    }

    public bool EnoughResources(Resources neededResources)
    {
        return _resources > neededResources;
    }

    private bool NotEnoughResources(Resources neededResources)
    {
        return !EnoughResources(neededResources);
    }

    public void PrintDebug()
    {
        Debug.Log("Money " + _resources.Money + " Food " + _resources.Food + "\n" +
            "Wood " + _resources.Wood + " Stone " + _resources.Stone + " Metal " + _resources.Metal);
    }
}
