using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : IndustrialStructure
{
    public override void ProduceResource()
    {
        services.ResourcesManager.AddResources(new Resources { Metal = 1 });
        Break(1f);
    }
}
