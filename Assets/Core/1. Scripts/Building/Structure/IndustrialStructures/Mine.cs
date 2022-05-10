public class Mine : IndustrialStructure
{
    public override void ProduceResource()
    {
        accumulatedResources += new Resources { Metal = CalculateProductivityPerHour() };
        Break(0.1f);
    }
}
