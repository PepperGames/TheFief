using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Farm : IndustrialStructure
{
    public override void ProduceResource()
    {
        resourcesManager.AddResources(new Resources { Food = 1 });
        Break(0.1f);
        //Repair(1f);
    }
}
