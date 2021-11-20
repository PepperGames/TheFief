using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class IndustrialStructure : Structure
{
    ////TODO удалить
    //private void Update()
    //{
    //    ProduceResource();
    //}

    public abstract void ProduceResource();

    public override void Upgrade()
    {
        if (CanBeUpgrade())
        {
            base.Upgrade();
        }
    }

}
