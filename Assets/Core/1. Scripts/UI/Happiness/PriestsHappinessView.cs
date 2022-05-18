public class PriestsHappinessView : HappinessView
{
    protected override void Start()
    {
        base.Start();
        services.HappinessManager.OnPriestsHappinessChange += DisplayHappinessOfEstates;
    }

    protected override void DisplayHappinessOfEstates()
    {
        _slider.value = services.HappinessManager.PriestsHappiness / 100;
    }
}
