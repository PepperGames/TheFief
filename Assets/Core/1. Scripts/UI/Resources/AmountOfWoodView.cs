public class AmountOfWoodView : AmountOfResourcesView
{
    protected override void DisplayAmountOfResources(Resources resources)
    {
        text.text = resources.Wood.ToString();
    }

    protected override void DisplayAmountOfResources()
    {
        text.text = services.ResourcesManager.Resources.Wood.ToString();
    }
}