public class StonePit : IndustrialStructure
{
    public override void ProduceResource()
    {
        accumulatedResources += new Resources { Stone = 1 };
        Break(0.1f);
    }
}
