public class StonePit : IndustrialStructure
{
    public override void ProduceResource()
    {
        accumulatedResources += new Resources { Stone = CalculateProductivityPerHour() };
    }
}
