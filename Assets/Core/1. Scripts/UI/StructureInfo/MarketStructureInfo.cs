using Zenject;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MarketStructureInfo : StructureInfo
{
    [Inject] private Services _services;

    [Header("Market Structure Info")]
    [SerializeField] private Market _market;


    [Header("buy Gold")] // ресы на золото
    [SerializeField] private Slider _sellFoodSlider;
    [SerializeField] private Slider _sellWoodSlider;
    [SerializeField] private Slider _sellStoneSlider;
    [SerializeField] private Slider _sellMetalSlider;

    [SerializeField] private TMP_Text _purchaseOfGoldCountText;
    [SerializeField] private Button _confirmPurchaseOfGoldButton;

    [Header("Sell Gold")] // золото на ресы
    [SerializeField] private Slider _buyFoodSlider;
    [SerializeField] private Slider _buyWoodSlider;
    [SerializeField] private Slider _buyStoneSlider;
    [SerializeField] private Slider _buyMetalSlider;

    [SerializeField] private TMP_Text _goldSaleCountText;
    [SerializeField] private Button _confirmGoldSaleButton;

    protected override void Start()
    {
        base.Start();

        _confirmPurchaseOfGoldButton.onClick.AddListener(ConfirmPurchaseOfGold);
        _confirmGoldSaleButton.onClick.AddListener(ConfirmConfirmGoldSale);
    }

    private void Update()
    {
        SetMaxPurchaseOfGoldSliderValues();
        _purchaseOfGoldCountText.text = _market.CalculateTheGoldReceived(GetResourcesForSale()).ToString();


        SetMaxSellSliderValues();
        _goldSaleCountText.text = _market.CalculateTheGoldSpent(GetMoneyForSale()).ToString();
    }

    private void ConfirmPurchaseOfGold()
    {
        _market.ConfirmPurchaseOfGold(GetResourcesForSale());
        _sellFoodSlider.value = 0;
        _sellWoodSlider.value = 0;
        _sellStoneSlider.value = 0;
        _sellMetalSlider.value = 0;
    }

    private Resources GetResourcesForSale()
    {
        Resources resources = new Resources() { Food = _sellFoodSlider.value, Wood = _sellWoodSlider.value, Stone = _sellStoneSlider.value, Metal = _sellMetalSlider.value };
        return resources;
    }

    private void SetMaxPurchaseOfGoldSliderValues()
    {
        _sellFoodSlider.maxValue = _services.ResourcesManager.Resources.Food;
        _sellWoodSlider.maxValue = _services.ResourcesManager.Resources.Wood;
        _sellStoneSlider.maxValue = _services.ResourcesManager.Resources.Stone;
        _sellMetalSlider.maxValue = _services.ResourcesManager.Resources.Metal;
    }


    // .....................................................................................................................................

    private void ConfirmConfirmGoldSale()
    {
        _market.ConfirmGoldSale(GetMoneyForSale());
        _buyFoodSlider.value = 0;
        _buyWoodSlider.value = 0;
        _buyStoneSlider.value = 0;
        _buyMetalSlider.value = 0;
    }

    private Resources GetMoneyForSale()
    {
        Resources resources = new Resources() { Food = _buyFoodSlider.value, Wood = _buyWoodSlider.value, Stone = _buyStoneSlider.value, Metal = _buyMetalSlider.value };
        return resources;
    }

    private void SetMaxSellSliderValues()
    {
        _buyFoodSlider.maxValue = _services.ResourcesManager.Resources.Food;
        _buyWoodSlider.maxValue = _services.ResourcesManager.Resources.Wood;
        _buyStoneSlider.maxValue = _services.ResourcesManager.Resources.Stone;
        _buyMetalSlider.maxValue = _services.ResourcesManager.Resources.Metal;
    }
}
