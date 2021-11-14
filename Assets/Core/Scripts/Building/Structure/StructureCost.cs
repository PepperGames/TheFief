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

    public Resources GetAmountOfResourcesForUpdate(int nextLvl)
    {
        nextLvl -= 1;
        if (nextLvl > 0 && nextLvl < resources.Length)
        {
            return resources[nextLvl];
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
