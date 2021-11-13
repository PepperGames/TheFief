public class AmountOfStoneView : AmountOfResourcesView
{
    protected override void DisplayAmountOfResources(Resources resources)
    {
        text.text = resources.Stone.ToString();
    }
}
