public class PublicStructure : Structure
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
        throw new System.NotImplementedException();
    }
}
