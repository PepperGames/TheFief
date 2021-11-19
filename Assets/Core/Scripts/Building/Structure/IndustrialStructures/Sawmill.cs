using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawmill : IndustrialStructure
{
    public override void ProduceResource()
    {
        resourcesManager.AddResources(new Resources { Wood = 1 });
    }
}
