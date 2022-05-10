public class Farm : IndustrialStructure
{
    public override void ProduceResource()
    {
        accumulatedResources += new Resources { Food = CalculateProductivityPerHour() };
        Break(0.1f);
    }
}
