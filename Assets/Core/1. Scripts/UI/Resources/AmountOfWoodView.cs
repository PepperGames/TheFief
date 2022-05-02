public class AmountOfWoodView : AmountOfResourcesView
{
    protected override void DisplayAmountOfResources(Resources resources)
    {
        text.text = resources.Wood.ToString();
    }
}
