using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureCost : MonoBehaviour
{
    [SerializeField] protected Resources[] resources;

    public Resources GetAmountOfResourcesForBuild()
    {
        return resources[0];
    }

    public Resources GetAmountOfResourcesForUpdate(int lvl)
    {
        if (lvl > 0 && lvl < resources.Length)
        {
            return resources[lvl];
        }
        else
        {
            throw new System.Exception("incorrect lvl");
        }
    }

    public int GetMaxLvl()
    {
        return resources.Length;
    }

}
