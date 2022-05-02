public class Sawmill : IndustrialStructure
{
    public override void ProduceResource()
    {
        accumulatedResources += new Resources { Wood = 1 };
        Break(0.1f);
    }
}
