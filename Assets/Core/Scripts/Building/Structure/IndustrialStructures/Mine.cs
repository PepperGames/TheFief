using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : IndustrialStructure
{
    public override void ProduceResource()
    {
        resourcesManager.AddResources(new Resources { Metal = 1 });
        Break(1f);
        Repair(1f);
    }
}
