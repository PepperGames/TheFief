using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicStructure : Structure
{
    protected override void Start()
    {
        base.Start();
    }

    public void OnMouseDown()
    {
        if (ui == null)
            return;

        ui.ShowOrHide();
    }

    public override void Upgrade()
    {
        if (CanBeUpgrade())
        {
            base.Upgrade();
            CharacterPlaces.numberOfPlaces += 2;
        }
    }

    protected override void OnEventsSubscribe()
    {
        base.OnEventsSubscribe();
    }

    protected override void OnEventsUnscribe()
    {
        base.OnEventsUnscribe();
    }

    protected override void OnDemolish()
    {
        Debug.Log("OnDemolish");
        base.OnDemolish();
        CharacterPlaces.KickOutAll();
    }
}
