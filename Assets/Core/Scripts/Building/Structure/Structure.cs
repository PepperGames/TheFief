using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(StructureCost))]
public abstract class Structure : BasicStructure, IUpgradable
{
    [SerializeField] protected int lvl;
    [SerializeField] protected int maxLvl;

    [Inject] [SerializeField] protected ResourcesManager resourcesManager;

    public Action OnUpgrade;
    protected virtual void Start()
    {
        maxLvl = structureCost.GetMaxLvl();
        StartCoroutine("WaitForUpgrade");
        lvl = 1;
    }

    IEnumerator WaitForUpgrade() //TODO удалить
    {
        yield return new WaitForSeconds(2f);
        Upgrade();
        Debug.Log("WaitForUpgrade Done");
    }

    public bool CanBeUpgrade()
    {
        int newLvl = lvl + 1;

        if (newLvl <= maxLvl)
        {
            if (resourcesManager.EnoughResources(structureCost.GetAmountOfResourcesForUpdate(newLvl)))
            {
                Debug.Log("Can Be Upgrade");
                return true;
            }
        }
        Debug.Log("Can Not Be Upgrade");
        return false;
    }

    public virtual void Upgrade()
    {
        lvl++;
        int nexLvl = lvl;
        resourcesManager.SpendResources(structureCost.GetAmountOfResourcesForUpdate(nexLvl));
        structureCost.IncreaseCurrentCost(structureCost.GetAmountOfResourcesForUpdate(nexLvl));
    }
}
