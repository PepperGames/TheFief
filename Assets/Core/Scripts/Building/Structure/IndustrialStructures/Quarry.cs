using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quarry : IndustrialStructure
{
    public override void ProduceResource()
    {
        resourcesManager.AddResources(new Resources { Stone = 1 });
    }
}
