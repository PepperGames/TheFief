using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPlaceInStructureView : MonoBehaviour
{
    [SerializeField] protected Sprite unknownPortrait;
    [SerializeField] protected string unknownName;

    [SerializeField] protected Image portrait;
    [SerializeField] protected TMP_Text nameText;

    [SerializeField] protected Button addCharacterButton;
    [SerializeField] protected Button _kikoutCharacterButton;

    protected Services _services;

    protected Structure _structure;
    protected Character _character;

    public virtual void Initialize(Services services, Structure structure)
    {
        _services = services;
        _structure = structure;

        portrait.sprite = unknownPortrait;
        nameText.text = unknownName;

        addCharacterButton.gameObject.SetActive(true);
        _kikoutCharacterButton.gameObject.SetActive(false);

        addCharacterButton.onClick.AddListener(WaitForCharacterSelect);
    }

    public virtual void Initialize(Services services, Structure structure, Character character)
    {
        if (character != null)
        {
            _services = services;
            _structure = structure;
            _character = character;

            portrait.sprite = character.CharacterData.Portrait;
            nameText.text = character.CharacterData.CharacterName;

            _kikoutCharacterButton.gameObject.SetActive(true);
            addCharacterButton.gameObject.SetActive(false);

            _kikoutCharacterButton.onClick.AddListener(KikoutCharacter);
        }
        else
        {
            Initialize(services, structure);
        }
    }

    protected virtual void WaitForCharacterSelect()
    {
        _services.UIController.ListOfAblebodiedCharacters.Open(_structure);
    }

    protected virtual void KikoutCharacter()
    {
        _structure.CharacterPlaces.KickOut(_character);
    }
}
