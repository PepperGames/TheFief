public class CharacterPlaceInResidentialStructureView : CharacterPlaceInStructureView
{
    protected override void WaitForCharacterSelect()
    {
        _services.UIController.ListOfLivingCharacters.Open(_structure);
    }
}
