public class LivingCharactersView : CharactersView
{
    public override void InitializeUI()
    {
        _portrait.sprite = _character.CharacterData.Portrait;
        _nameText.text = _character.CharacterData.CharacterName;

        if (_character.LivingPlace == null)
        {
            _addCharacterButton.gameObject.SetActive(true);
        }
        else
        {
            _addCharacterButton.gameObject.SetActive(false);
        }
    }

    protected override void OnEventsSubscribe()
    {
        if (_character.LivingPlace == null)
        {
            _addCharacterButton.onClick.AddListener(AddToStructure);
        }
        base.OnEventsSubscribe();
    }
}