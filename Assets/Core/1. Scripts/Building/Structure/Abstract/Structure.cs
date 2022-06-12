using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(StructureCost))]
[RequireComponent(typeof(Durability))]
public abstract class Structure : BasicStructure, IUpgradable, IBreakable
{
    [SerializeField] protected int lvl;
    [SerializeField] protected int maxLvl;
    [SerializeField] protected Estates _estate;
    [SerializeField] protected StructireName _structireName;

    [SerializeField] protected Durability durability;

    [SerializeField] protected CharacterPlacesInStructure _characterPlaces;

    [SerializeField] protected StructureInfo ui;

    public Durability Durability => durability;
    public int LVL => lvl;
    public int MaxLvl => maxLvl;

    public CharacterPlacesInStructure CharacterPlaces => _characterPlaces;

    public virtual Estates Estate => _estate;
    public virtual StructireName StructireName => _structireName;

    public Action OnLvlUpgrade;

    protected virtual void Start()
    {
        //maxLvl = StructureCost.GetMaxLvl();
        //lvl = 1;
        //durability = GetComponent<Durability>();
        OnEventsSubscribe();
    }

    public virtual void OnMouseDown()
    {
        if (ui == null)
            return;

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            ui.ShowOrHide();
        }
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

        OnLvlUpgrade?.Invoke();
    }

    protected abstract void OnUpgrade();

    public void Break(float percent)
    {
        durability.Break(percent);
    }

    public Resources GetRepairCost()
    {
        return GetRepairCost(durability.MissingStrength);
    }

    public Resources GetRepairCost(float percent)
    {
        return 0.75f * (percent / 100) * StructureCost.CurrentCost;
    }

    public bool Repair(float percent)
    {
        if (services.ResourcesManager.EnoughResources(GetRepairCost(percent)))
        {
            services.ResourcesManager.SpendResources(GetRepairCost(percent));
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
        OnLvlUpgrade += OnUpgrade;
    }

    protected virtual void OnEventsUnscribe()
    {
        OnLvlUpgrade -= OnUpgrade;
    }

    protected override void OnDemolish()
    {
        Debug.Log("OnDemolish");
        base.OnDemolish();
        OnEventsUnscribe();
        CharacterPlaces.KickOutAll();
    }
}

public enum StructireName
{
    Farm,
    Mine,
    Sawmill,
    StonePit,
    Church,
    Market,
    WoodenHut
}
