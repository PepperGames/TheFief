public class Farm : IndustrialStructure
{
    public override void ProduceResource()
    {
        services.ResourcesManager.AddResources(new Resources { Food = 1 });
        Break(0.1f);
    }
}
    