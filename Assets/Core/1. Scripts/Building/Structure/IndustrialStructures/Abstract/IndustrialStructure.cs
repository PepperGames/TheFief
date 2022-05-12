using UnityEngine;

public abstract class IndustrialStructure : Structure
{
    [SerializeField] private IndustrialStructureInfo ui;

    [SerializeField] protected Resources accumulatedResources;

    [SerializeField] protected float _overallPerformance;

    protected override void Start()
    {
        base.Start();
        accumulatedResources = new Resources();
    }

    public void OnMouseDown()
    {
        if (ui == null)
            return;

        ui.ShowOrHide();
    }

    protected float CalculateProductivityPerHour()
    {
        float result = 0;
        float _performancePerWorker = _overallPerformance / CharacterPlaces.numberOfPlaces;

        foreach (Character characters in CharacterPlaces.Characters)
        {
            float t = _performancePerWorker * MatrixEstateToPrestige.GetCoefficient(characters.CharacterData.Estates, Estate);
            result += t;
        }

        Debug.Log("1 " + result);
        float performancePercentageFromWear = 0;
        if (Durability.CurrentDurability >= 1)
        {
            performancePercentageFromWear = Mathf.Log(Durability.CurrentDurability, Durability.MaxDurability);
        }
        Debug.Log("performancePercentageFromWear " + performancePercentageFromWear);
        result *= performancePercentageFromWear;
        Debug.Log("2 " + result);
        return result;
    }

    public virtual void TryProduceResource()
    {
        if (InGameTime.Hour > ConstantValues.beginingWorkDayTime && InGameTime.Hour <= ConstantValues.endWorkingDayTime)
        {
            ProduceResource();
        }
    }

    public abstract void ProduceResource();

    public virtual void TryIssueAccumulatedResource()
    {
        if (InGameTime.Hour == ConstantValues.endWorkingDayTime)
        {
            IssueAccumulatedResource();
        }
    }

    public virtual void IssueAccumulatedResource()
    {
        services.ResourcesManager.AddResources(accumulatedResources);
        accumulatedResources.Reset();
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
        InGameTime.OnHourChange += TryProduceResource;
        InGameTime.OnHourChange += TryIssueAccumulatedResource;
    }

    protected override void OnEventsUnscribe()
    {
        base.OnEventsUnscribe();
        InGameTime.OnHourChange -= TryProduceResource;
        InGameTime.OnHourChange -= TryIssueAccumulatedResource;
    }

    protected override void OnDemolish()
    {
        Debug.Log("OnDemolish");
        base.OnDemolish();
        IssueAccumulatedResource();
        CharacterPlaces.KickOutAll();
    }
}
