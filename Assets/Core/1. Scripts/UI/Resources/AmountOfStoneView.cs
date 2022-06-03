public class AmountOfStoneView : AmountOfResourcesView
{
    protected override void DisplayAmountOfResources(Resources resources)
    {
        text.text = resources.Stone.ToString();
    }

    protected override void DisplayAmountOfResources()
    {
        text.text = services.ResourcesManager.Resources.Stone.ToString();
    }
}

