public class MerchantsHappinessView : HappinessView
{
    protected override void Start()
    {
        base.Start();
        services.HappinessManager.OnMerchantsHappinessChange += DisplayHappinessOfEstates;
    }

    protected override void DisplayHappinessOfEstates()
    {
        _slider.value = services.HappinessManager.MerchantsHappiness / 100;
    }
}
