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

    [SerializeField] protected CharacterPlacesInStructure _characterPlaces;

    [SerializeField] protected StructureInfo ui;

    public Durability Durability => durability;

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

    public virtual void OnMouseDown()
    {
        if (ui == null)
            return;

        ui.ShowOrHide();
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
        durability.Break(percent);
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
        Debug.Log("OnDemolish");
        base.OnDemolish();
        OnEventsUnscribe();
        CharacterPlaces.KickOutAll();
    }
}
