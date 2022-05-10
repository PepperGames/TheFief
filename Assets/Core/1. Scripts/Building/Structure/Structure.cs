using System;
using UnityEngine;

[RequireComponent(typeof(StructureCost))]
[RequireComponent(typeof(Durability))]
public abstract class Structure : BasicStructure, IUpgradable, IBreakable
{
    [SerializeField] protected int lvl;
    [SerializeField] protected int maxLvl;
    [SerializeField] protected Estates _estate;  

      [SerializeField] protected Durability durability;

    public Durability Durability => durability;

    [SerializeField] protected CharacterPlacesInStructure _characterPlaces;
    public CharacterPlacesInStructure CharacterPlaces => _characterPlaces;
    public virtual Estates Estate => _estate;

    public Action OnUpgrade;

    protected virtual void Start()
    {
        maxLvl = StructureCost.GetMaxLvl();
        //lvl = 1;
        //durability = GetComponent<Durability>();
        OnEventsSubscribe();
    }

    public virtual bool CanBeUpgrade()
    {
        int newLvl = lvl + 1;

        if (newLvl <= maxLvl)
        {
            if (services.ResourcesManager.EnoughResources(StructureCost.GetAmountOfResourcesForUpdate(newLvl)))
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
        services.ResourcesManager.SpendResources(StructureCost.GetAmountOfResourcesForUpdate(nexLvl));
        StructureCost.IncreaseCurrentCost(StructureCost.GetAmountOfResourcesForUpdate(nexLvl));
    }

    public void Break(float percent)
    {
        durability.CurrentDurability -= percent;
    }

    public bool Repair(float percent)
    {
        if (services.ResourcesManager.EnoughResources(0.75f * (percent / 100) * StructureCost.CurrentCost))
        {
            services.ResourcesManager.SpendResources(0.75f * (percent / 100) * StructureCost.CurrentCost);
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

    protected virtual void OnEventsSubscribe()
    {

    }

    protected virtual void OnEventsUnscribe()
    {

    }

    protected override void OnDemolish()
    {
        base.OnDemolish();
        OnEventsUnscribe();
    }
}
