using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class IndustrialStructure : Structure
{
    //[Inject] [SerializeField] protected ResourcesManager resourcesManager;

    //TODO удалить
    private void Update()
    {
        ProduceResource();
    }

    public abstract void ProduceResource();

    public override void Improve()
    {
        if (CanBeImprove())
        {
            base.Improve();
        }
    }

    //IEnumerator WaitForImprove()
    //{
    //    yield return new WaitForSeconds(4f);
    //    Improve();
    //}
}
