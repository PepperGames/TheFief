using UnityEngine;
public abstract class ResidentialStructure : Structure
{
    [SerializeField] private IndustrialStructureInfo ui;

    [SerializeField] private CharacterPlacesInResidentialStructure characterPlaces;

    public CharacterPlacesInResidentialStructure CharacterPlaces => characterPlaces;

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
