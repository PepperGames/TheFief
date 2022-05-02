using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePit : IndustrialStructure
{
    public override void ProduceResource()
    {
        services.ResourcesManager.AddResources(new Resources { Stone = 1 });
        Break(1f);
    }
}
