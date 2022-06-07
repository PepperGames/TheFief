public class WoodenHut : ResidentialStructure
{
    protected override void OnUpgrade()
    {
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
