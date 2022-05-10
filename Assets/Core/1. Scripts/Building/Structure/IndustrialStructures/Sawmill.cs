public class Sawmill : IndustrialStructure
{
    public override void ProduceResource()
    {
        accumulatedResources += new Resources { Wood = CalculateProductivityPerHour() };
        Break(0.1f);
    }
}
