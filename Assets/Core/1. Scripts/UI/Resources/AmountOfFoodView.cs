public class AmountOfFoodView : AmountOfResourcesView
{
    protected override void DisplayAmountOfResources(Resources resources)
    {
        text.text = resources.Food.ToString();
    }

    protected override void DisplayAmountOfResources()
    {
        text.text =services.ResourcesManager.Resources.Food.ToString();
    }
}
