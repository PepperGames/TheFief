public class LivingCharactersView : CharactersView
{
    public override void InitializeUI()
    {
        _portrait.sprite = _character.CharacterData.Portrait;
        _nameText.text = _character.CharacterData.CharacterName;

        if (_character.LivingPlace == null)
        {
            if (_addCharacterButton != null)
            {
                _addCharacterButton.gameObject.SetActive(true);
            }
        }
        else
        {
            if (_addCharacterButton != null)
            {
                _addCharacterButton.gameObject.SetActive(false);
            }
        }
    }

    protected override void OnEventsSubscribe()
    {
        if (_character.LivingPlace == null)
        {
            _addCharacterButton.onClick.AddListener(AddToStructure);
        }
        base.OnEventsSubscribe();
        _character.OnChangeLivingPlace += InitializeUI;
        _structure.CharacterPlaces.OnCharacterListChange += InitializeUI;
    }

    protected override void OnEventsUnscribe()
    {
        base.OnEventsUnscribe();
        _character.OnChangeLivingPlace -= InitializeUI;
        _structure.CharacterPlaces.OnCharacterListChange -= InitializeUI;
        _addCharacterButton.onClick.RemoveAllListeners();
    }
}
