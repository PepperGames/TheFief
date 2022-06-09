using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FaithView : MonoBehaviour
{
    [Inject] protected Services services;
    [SerializeField] protected Slider _slider;

    protected void Start()
    {
        services.FaithManager.OnFaithChange += DisplayFaith;
    }

    protected void DisplayFaith()
    {
        _slider.value = services.HappinessManager.PeasantsHappiness / 100;
    }
}
