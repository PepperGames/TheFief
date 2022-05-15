using UnityEngine;
using Zenject;

public class Market : PublicStructure
{
    [Inject] private Services _services;

    private float _goldForSale = 0;

    public float a;

    public float CalculateTheGoldReceived(Resources resourcesForSale)
    {
        Resources resources = resourcesForSale / GoldValue.sellingPriceOfGold;
        float result = resources.Food + resources.Wood + resources.Stone + resources.Metal;
        return result;
    }

    public void ConfirmPurchaseOfGold(Resources resourcesForSale) // ресы на золото
    {
        if (_services.ResourcesManager.EnoughResources(resourcesForSale))
        {
            Resources resources = new Resources() { Money = CalculateTheGoldReceived(resourcesForSale) };
            _services.ResourcesManager.AddResources(resources);
            _services.ResourcesManager.SpendResources(resourcesForSale);
        }
    }

    public void ConfirmGoldSale() //золото на ресы
    {

    }
}
