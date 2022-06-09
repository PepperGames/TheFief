public abstract class PublicStructure : Structure
{
    public override void Upgrade()
    {
        if (CanBeUpgrade())
        {
            base.Upgrade();
        }
    }

    protected override void OnUpgrade()
    {

    }
}
