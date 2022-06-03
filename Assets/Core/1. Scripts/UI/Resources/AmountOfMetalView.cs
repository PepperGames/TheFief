public class AmountOfMetalView : AmountOfResourcesView
{
    protected override void DisplayAmountOfResources(Resources resources)
    {
        text.text = resources.Metal.ToString();
    }

    protected override void DisplayAmountOfResources()
    {
        text.text = services.ResourcesManager.Resources.Wood.ToString();
    }
}
