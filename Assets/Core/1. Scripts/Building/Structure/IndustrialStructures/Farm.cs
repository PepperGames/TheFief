public class Farm : IndustrialStructure
{
    public override void ProduceResource()
    {
        accumulatedResources += new Resources { Food = 1 };
        Break(0.1f);
    }
}
