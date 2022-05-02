using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(StructureCost))]
[RequireComponent(typeof(Durability))]
public abstract class Structure : BasicStructure, IUpgradable, IBreakable
{
    [SerializeField] protected int lvl;
    [SerializeField] protected int maxLvl;

    protected Durability durability;

    public Action OnUpgrade;
    protected virtual void Start()
    {
        maxLvl = structureCost.GetMaxLvl();
        //StartCoroutine("WaitForUpgrade");
        lvl = 1;
        durability = GetComponent<Durability>();
    }

    //IEnumerator WaitForUpgrade() //TODO удалить
    //{
    //    yield return new WaitForSeconds(2f);
    //    Upgrade();
    //    Debug.Log("WaitForUpgrade Done");
    //}

    public bool CanBeUpgrade()
    {
        int newLvl = lvl + 1;

        if (newLvl <= maxLvl)
        {
            if (services.ResourcesManager.EnoughResources(structureCost.GetAmountOfResourcesForUpdate(newLvl)))
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
        services.ResourcesManager.SpendResources(structureCost.GetAmountOfResourcesForUpdate(nexLvl));
        structureCost.IncreaseCurrentCost(structureCost.GetAmountOfResourcesForUpdate(nexLvl));
    }

    public void Break(float percent)
    {
        durability.CurrentDurability -= percent;
    }

    public bool Repair(float percent)
    {
        if (services.ResourcesManager.EnoughResources(0.75f * (percent / 100) * structureCost.CurrentCost))
        {
            services.ResourcesManager.SpendResources(0.75f * (percent / 100) * structureCost.CurrentCost);
            durability.CurrentDurability += percent;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RepairCompletely()
    {
        Repair(durability.MissingStrength);
    }
}
