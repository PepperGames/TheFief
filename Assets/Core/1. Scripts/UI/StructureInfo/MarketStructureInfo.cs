using Zenject;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MarketStructureInfo : StructureInfo
{
    [Inject] private Services _services;

    [SerializeField] private Market _market;

    // ресы на золото
    [SerializeField] private Slider _sellFoodSlider;
    [SerializeField] private Slider _sellWoodSlider;
    [SerializeField] private Slider _sellStoneSlider;
    [SerializeField] private Slider _sellMetalSlider;

    [SerializeField] private TMP_Text _purchaseOfGoldCountText;
    [SerializeField] private Button _confirmPurchaseOfGoldButton;


    private void Start()
    {
        _confirmPurchaseOfGoldButton.onClick.AddListener(ConfirmPurchaseOfGold);
        //Resources qe = new Resources();
        //qe.Money;
        //qe.Food;
        //qe.Wood;
        //qe.Stone;
        //qe.Metal;
    }

    private void Update()
    {
        SetMaxSliderValues();
        _purchaseOfGoldCountText.text = _market.CalculateTheGoldReceived(GetResourcesForSale()).ToString();
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
        Resources resources = new Resources() { Food = _sellFoodSlider.value, Wood = _sellWoodSlider.value, Stone = _sellStoneSlider.value, Metal = _sellMetalSlider.value, };
        return resources;
    }

    private void SetMaxSliderValues()
    {
        _sellFoodSlider.maxValue = _services.ResourcesManager.Resources.Food;
        _sellWoodSlider.maxValue = _services.ResourcesManager.Resources.Wood;
        _sellStoneSlider.maxValue = _services.ResourcesManager.Resources.Stone;
        _sellMetalSlider.maxValue = _services.ResourcesManager.Resources.Metal;
    }
}
