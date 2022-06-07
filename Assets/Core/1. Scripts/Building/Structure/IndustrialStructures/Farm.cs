public class Farm : IndustrialStructure
{
    public override void ProduceResource()
    {
        accumulatedResources += new Resources { Food = CalculateProductivityPerHour() };
    }

    public override void Upgrade()
    {
        base.Upgrade();
        switch (lvl)
        {
            case 2:
                _overallPerformancePerHouse = 100;
                CharacterPlaces.IncreaseNumberOfPlaces(2);
                break;

            case 3:
                _overallPerformancePerHouse = 150;
                CharacterPlaces.IncreaseNumberOfPlaces(2);
                break;
            
            default:
                break;
        }
    }
}
