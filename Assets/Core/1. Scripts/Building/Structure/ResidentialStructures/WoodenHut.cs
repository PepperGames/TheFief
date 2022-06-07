public class WoodenHut : ResidentialStructure
{
    protected override void OnUpgrade()
    {
        switch (lvl)
        {
            case 2:
                _estate = Estates.Priests;
                CharacterPlaces.NumberOfPlaces = 10;
                break;

            case 3:
                _estate = Estates.Peers;
                CharacterPlaces.NumberOfPlaces = 15;
                break;

            default:
                break;
        }
    }
}
