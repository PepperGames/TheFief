using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class IndustrialStructure : Structure
{
    [SerializeField] private GameObject ui;
    public void OnMouseDown()
    {
        if (ui == null)
            return;

        if (ui.activeInHierarchy)
        {
            ui.SetActive(false);
        }
        else
        {
            ui.SetActive(true);
        }
    }
    //TODO удалить
    private void Update()
    {
        ProduceResource();
    }

    public abstract void ProduceResource();

    public override void Upgrade()
    {
        if (CanBeUpgrade())
        {
            base.Upgrade();
        }
    }

}
