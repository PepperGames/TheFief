public class Mine : IndustrialStructure
{
    public override void ProduceResource()
    {
        accumulatedResources += new Resources { Metal = CalculateProductivityPerHour() };
    }

    public override void Upgrade()
    {
        base.Upgrade();
        switch (lvl)
        {
            case 2:
                _overallPerformancePerHouse = 20;
                CharacterPlaces.IncreaseNumberOfPlaces(2);
                break;

            case 3:
                _overallPerformancePerHouse = 30;
                CharacterPlaces.IncreaseNumberOfPlaces(2);
                break;

            default:
                break;
        }
    }
}
