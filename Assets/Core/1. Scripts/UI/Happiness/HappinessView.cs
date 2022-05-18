using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class HappinessView : MonoBehaviour
{
    [Inject] protected Services services;
    [SerializeField] protected Slider _slider;

    protected virtual void Start()
    {
        //Estates estates1 = Estates.Peasants;
        //Estates estates2 = Estates.Merchants;
        //Estates estates3 = Estates.Priests;
        //Estates estates4 = Estates.Peers;
    }

    protected abstract void DisplayHappinessOfEstates();
}
