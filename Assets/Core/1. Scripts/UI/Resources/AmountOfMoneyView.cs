public class AmountOfMoneyView : AmountOfResourcesView
{
    protected override void DisplayAmountOfResources(Resources resources)
    {
        text.text = resources.Money.ToString();
    }

    protected override void DisplayAmountOfResources()
    {
        text.text = CharacterRounder.Round(services.ResourcesManager.Resources.Money, 0);
    }
}
