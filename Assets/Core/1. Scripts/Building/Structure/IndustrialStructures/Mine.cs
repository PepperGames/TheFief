public class Mine : IndustrialStructure
{
    public override void ProduceResource()
    {
        accumulatedResources += new Resources { Metal = 1 };
        Break(0.1f);
    }
}
