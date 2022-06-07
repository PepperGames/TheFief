public class WoodenHut : ResidentialStructure
{
    public override void Upgrade()
    {
        base.Upgrade();
        switch (lvl)
        {
            case 2:
                _estate = Estates.Priests;
                CharacterPlaces.IncreaseNumberOfPlaces(2);
                break;

            case 3:
                _estate = Estates.Peers;
                CharacterPlaces.IncreaseNumberOfPlaces(2);
                break;

            default:
                break;
        }
    }
}
