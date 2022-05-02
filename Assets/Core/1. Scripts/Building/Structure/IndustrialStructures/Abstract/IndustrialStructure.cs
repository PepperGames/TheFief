using UnityEngine;

public abstract class IndustrialStructure : Structure
{
    [SerializeField] private GameObject ui;

    protected Resources accumulatedResources;

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

    //TODO удалить
    private void Update()
    {
        ProduceResource();
    }

    public abstract void ProduceResource();

    public virtual void IssueAccumulatedResource()
    {
        services.ResourcesManager.AddResources(accumulatedResources);
    }

    public override void Upgrade()
    {
        if (CanBeUpgrade())
        {
            base.Upgrade();
        }
    }

}
