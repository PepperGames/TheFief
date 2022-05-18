public class PeersHappinessView : HappinessView
{
    protected override void Start()
    {
        base.Start();
        services.HappinessManager.OnPeersHappinessChange += DisplayHappinessOfEstates;
    }

    protected override void DisplayHappinessOfEstates()
    {
        _slider.value = services.HappinessManager.PeersHappiness / 100;
    }
}
