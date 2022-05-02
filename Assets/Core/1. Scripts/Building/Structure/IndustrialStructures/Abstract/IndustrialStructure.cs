using UnityEngine;

public abstract class IndustrialStructure : Structure
{
    [SerializeField] private GameObject ui;

    [SerializeField] protected Resources accumulatedResources;

    protected override void Start()
    {
        base.Start();
        accumulatedResources = new Resources();
    }

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
}
