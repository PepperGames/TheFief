public class PeasantsHappinessView : HappinessView
{
    protected override void Start()
    {
        base.Start();
        services.HappinessManager.OnPeasantsHappinessChange += DisplayHappinessOfEstates;
    }

    protected override void DisplayHappinessOfEstates()
    {
        _slider.value = services.HappinessManager.PeasantsHappiness / 100;
    }
}
