using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public abstract class AmountOfResourcesView : MonoBehaviour
{
    [Inject] [SerializeField] protected ResourcesManager resourcesManager;
    [SerializeField] protected TMP_Text text;
    private void Start()
    {
        resourcesManager.OnResourcesChange += DisplayAmountOfResources;
    }

    protected abstract void DisplayAmountOfResources(Resources resources);
}
