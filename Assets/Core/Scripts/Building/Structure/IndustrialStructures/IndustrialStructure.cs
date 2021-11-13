using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class IndustrialStructure : Structure
{
    [Inject] [SerializeField] protected ResourcesManager resourcesManager;

    //TODO удалить
    private void Update()
    {
        ProduceResource();
    }

    public abstract void ProduceResource();
}
