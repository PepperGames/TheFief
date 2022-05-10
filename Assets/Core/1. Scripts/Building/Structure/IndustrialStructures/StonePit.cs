public class StonePit : IndustrialStructure
{
    public override void ProduceResource()
    {
        accumulatedResources += new Resources { Stone = CalculateProductivityPerHour() };
        Break(0.1f);
    }
}
