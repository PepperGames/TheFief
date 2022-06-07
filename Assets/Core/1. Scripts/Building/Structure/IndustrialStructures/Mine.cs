public class Mine : IndustrialStructure
{
    public override void ProduceResource()
    {
        accumulatedResources += new Resources { Metal = CalculateProductivityPerHour() };
    }

    protected override void OnUpgrade()
    {
        switch (lvl)
        {
            case 2:
                _overallPerformancePerHouse = 20;
                CharacterPlaces.NumberOfPlaces += 2;
                break;

            case 3:
                _overallPerformancePerHouse = 30;
                CharacterPlaces.NumberOfPlaces += 2;
                break;

            default:
                break;
        }
    }
}
