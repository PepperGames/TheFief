public class Sawmill : IndustrialStructure
{
    public override void ProduceResource()
    {
        services.ResourcesManager.AddResources(new Resources { Wood = 1 });
        Break(1f);
    }
}
