public class StonePit : IndustrialStructure
{
    public override void ProduceResource()
    {
        accumulatedResources += new Resources { Stone = CalculateProductivityPerHour() };
    }

    public override void Upgrade()
    {
        base.Upgrade();
        switch (lvl)
        {
            case 2:
                _overallPerformancePerHouse = 50;
                CharacterPlaces.IncreaseNumberOfPlaces(2);
                break;

            case 3:
                _overallPerformancePerHouse = 75;
                CharacterPlaces.IncreaseNumberOfPlaces(2);
                break;

            default:
                break;
        }
    }
}
