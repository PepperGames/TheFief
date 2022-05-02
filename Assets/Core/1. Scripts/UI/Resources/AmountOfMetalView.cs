public class AmountOfMetalView : AmountOfResourcesView
{
    protected override void DisplayAmountOfResources(Resources resources)
    {
        text.text = resources.Metal.ToString();
    }
}
