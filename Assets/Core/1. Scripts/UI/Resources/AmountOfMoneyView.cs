public class AmountOfMoneyView : AmountOfResourcesView
{
    protected override void DisplayAmountOfResources(Resources resources)
    {
        text.text = resources.Money.ToString();
    }

    protected override void DisplayAmountOfResources()
    {
        text.text = services.ResourcesManager.Resources.Money.ToString();
    }
}
