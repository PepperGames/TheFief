public class CharacterPlaceInWorkingStructureView : CharacterPlaceInStructureView
{
    protected override void WaitForCharacterSelect()
    {
        _services.UIController.ListOfAblebodiedCharacters.Open(_structure);
    }
}
