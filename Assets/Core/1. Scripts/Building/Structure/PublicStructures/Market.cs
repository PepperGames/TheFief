using UnityEngine;
using Zenject;

public class Market : IndustrialStructure
{
    [Inject] private Services _services;

    [SerializeField] [Range(1, 100)] private float _bonusForOneCharacter = 1;

    private float GetBonusPercentage()
    {
        float result = 0;

        foreach (Character characters in CharacterPlaces.Characters)
        {
            result += _bonusForOneCharacter * MatrixEstateToPrestige.GetCoefficient(characters.CharacterData.Estates, Estate);
        }

        Debug.Log("1 " + result);

        float performancePercentageFromWear = 0;
        if (Durability.CurrentDurability >= 1)
        {
            performancePercentageFromWear = Mathf.Log(Durability.CurrentDurability, Durability.MaxDurability);
        }

        Debug.Log("performancePercentageFromWear " + performancePercentageFromWear);
        result *= performancePercentageFromWear;
        Debug.Log("2 " + result);
        return result;
    }

    //..................................................................................................................

    public float CalculateTheGoldReceived(Resources resourcesForSale)
    {
        Resources resources = resourcesForSale / GoldValue.purchasePriceOfGold;
        float result = resources.Food + resources.Wood + resources.Stone + resources.Metal;
        return result + result * (GetBonusPercentage() / 100f);
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

    public Resources CalculateResourcesCanBeBoughtWithTheRemainingGold(Resources resourcesForBuy)
    {
        float remainingGold = _services.ResourcesManager.Resources.Money - CalculateTheGoldSpent(resourcesForBuy);
        //return resourcesForBuy + (GoldValue.sellingPriceOfGold * remainingGold);
        return new Resources()
        {
            Food = remainingGold * GoldValue.sellingPriceOfGold.Food + resourcesForBuy.Food,
            Wood = remainingGold * GoldValue.sellingPriceOfGold.Wood + resourcesForBuy.Wood,
            Stone = remainingGold * GoldValue.sellingPriceOfGold.Stone + resourcesForBuy.Stone,
            Metal = remainingGold * GoldValue.sellingPriceOfGold.Metal + resourcesForBuy.Metal
        };
    }

    public float CalculateTheGoldSpent(Resources resourcesForBuy)
    {
        Resources resources = resourcesForBuy / GoldValue.sellingPriceOfGold;
        float result = resources.Food + resources.Wood + resources.Stone + resources.Metal;
        return result - result * (GetBonusPercentage() / 100f);
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

    public override void ProduceResource()
    {
        accumulatedResources += new Resources { Money = CalculateProductivityPerHour() };
    }

    protected override void OnUpgrade()
    {
        switch (lvl)
        {
            case 2:
                _overallPerformancePerHouse = 10;
                CharacterPlaces.IncreaseNumberOfPlaces(2);
                break;

            case 3:
                _overallPerformancePerHouse = 15;
                CharacterPlaces.IncreaseNumberOfPlaces(2);
                break;

            default:
                break;
        }
    }
}
