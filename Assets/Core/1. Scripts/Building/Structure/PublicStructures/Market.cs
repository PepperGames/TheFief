using UnityEngine;
using Zenject;

public class Market : PublicStructure
{
    [Inject] private Services _services;

    private float _goldForSale = 0;

    public float a;

    public float CalculateTheGoldReceived(Resources resourcesForSale)
    {
        Resources resources = resourcesForSale / GoldValue.purchasePriceOfGold;
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

    //..................................................................................................................

    public float CalculateTheGoldSpent(Resources resourcesForBuy)
    {
        Resources resources = resourcesForBuy / GoldValue.sellingPriceOfGold;
        float result = resources.Food + resources.Wood + resources.Stone + resources.Metal;
        return result;
    }

    public void ConfirmGoldSale(Resources resourcesForBuy) //золото на ресы
    {
        Resources money = new Resources() { Money = CalculateTheGoldSpent(resourcesForBuy) };
        Debug.Log("money  " + money);
        if (_services.ResourcesManager.EnoughResources(money))
        {
            _services.ResourcesManager.SpendResources(money);
            _services.ResourcesManager.AddResources(resourcesForBuy);
        }
    }
}
