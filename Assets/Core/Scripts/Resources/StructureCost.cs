using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureCost : MonoBehaviour
{
    [SerializeField] protected Resources constructionCost;
    [SerializeField] protected Resources[] upgradeCost;

    [SerializeField] protected Resources currentCost;
    public Resources CurrentCost
    {
        get { return currentCost; }
        private set { currentCost = value; }
    }

    public Resources GetAmountOfResourcesForBuild()
    {
        //currentCost = constructionCost;
        return constructionCost;
    }

    public Resources GetAmountOfResourcesForUpdate(int nextLvl)
    {
        int index = nextLvl - 2;
        int maxIndex = GetMaxLvl() - 2;
        print(index);

        if (index >= 0 && index <= maxIndex)
        {
            //currentCost += upgradeCost[index];
            return upgradeCost[index];
        }
        else
        {
            throw new System.Exception("incorrect lvl");
        }
    }

    public void IncreaseCurrentCost(Resources spended)
    {
        currentCost += spended;
    }

    public int GetMaxLvl()
    {
        return upgradeCost.Length + 1;
    }

}
