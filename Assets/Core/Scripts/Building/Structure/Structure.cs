using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(StructureCost))]
public abstract class Structure : BasicStructure, IImprovable
{
    [SerializeField] protected int lvl;
    [SerializeField] protected int maxLvl;

    public StructureCost structureCost;

    [Inject] [SerializeField] protected ResourcesManager resourcesManager;

    protected virtual void Start()
    {
        maxLvl = structureCost.GetMaxLvl();
        //StartCoroutine("WaitForImprove");
        //StartCoroutine("WaitForDemolish");
        lvl = 1;
    }

    public bool CanBeImprove()
    {
        int newLvl = lvl + 1;
        print(newLvl);
        if (newLvl <= maxLvl)
        {
            if (resourcesManager.EnoughResources(structureCost.GetAmountOfResourcesForUpdate(newLvl)))
            {
                Debug.Log("CanBeImprove");
                return true;
            }
        }
        Debug.Log("Not CanBeImprove");
        return false;
    }

    public virtual void Improve()
    {
        lvl++;
        resourcesManager.SpendResources(structureCost.GetAmountOfResourcesForUpdate(lvl));
    }

    //IEnumerator WaitForDemolish() //TODO удалить
    //{
    //    yield return new WaitForSeconds(4f);
    //    Demolish();
    //    Debug.Log("Demolish");
    //}
    IEnumerator WaitForImprove() //TODO удалить
    {
        yield return new WaitForSeconds(4f);
        Improve();
        Debug.Log("Improve");
    }
}
