public class AmountOfMoneyView : AmountOfResourcesView
{
    protected override void DisplayAmountOfResources(Resources resources)
    {
        text.text = resources.Money.ToString();
    }
}
