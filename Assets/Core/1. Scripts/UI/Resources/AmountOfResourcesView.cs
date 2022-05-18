using TMPro;
using UnityEngine;
using Zenject;

public abstract class AmountOfResourcesView : MonoBehaviour
{
    [Inject] protected Services services;

    [SerializeField] protected TMP_Text text;

    private void Start()
    {
        services.ResourcesManager.OnResourcesChange += DisplayAmountOfResources;
    }

    protected abstract void DisplayAmountOfResources(Resources resources);

    private void OnDestroy()
    {
        services.ResourcesManager.OnResourcesChange -= DisplayAmountOfResources;
    }
}
