public class Sawmill : IndustrialStructure
{
    public override void ProduceResource()
    {
        accumulatedResources += new Resources { Wood = CalculateProductivityPerHour() };
    }

    public override void Upgrade()
    {
        base.Upgrade();
        switch (lvl)
        {
            case 2:
                _overallPerformancePerHouse = 70;
                CharacterPlaces.IncreaseNumberOfPlaces(2);
                break;

            case 3:
                _overallPerformancePerHouse = 100;
                CharacterPlaces.IncreaseNumberOfPlaces(2);
                break;

            default:
                break;
        }
    }
}
