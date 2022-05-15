public abstract class ResidentialStructure : Structure
{
    public override void Upgrade()
    {
        if (CanBeUpgrade())
        {
            base.Upgrade();
            CharacterPlaces.IncreaseNumberOfPlaces(2);
        }
    }
}
