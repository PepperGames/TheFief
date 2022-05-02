public class AmountOfFoodView : AmountOfResourcesView
{
    protected override void DisplayAmountOfResources(Resources resources)
    {
        text.text = resources.Food.ToString();
    }
}
