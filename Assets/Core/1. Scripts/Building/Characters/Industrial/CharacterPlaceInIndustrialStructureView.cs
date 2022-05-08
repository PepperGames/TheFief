using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPlaceInIndustrialStructureView : MonoBehaviour
{
    public Sprite unknownPortrait;
    public string unknownName;

    public Image portrait;
    public TMP_Text nameText;

    public Button addCharacterButton;
    public Button kikoutCharacterButton;

    private Services services;

    private IndustrialStructure _industrialStructure;
    private Character _character;

    public void Initialize(Services services, IndustrialStructure industrialStructure)
    {
        this.services = services;
        _industrialStructure = industrialStructure;
        portrait.sprite = unknownPortrait;
        nameText.text = unknownName;

        addCharacterButton.gameObject.SetActive(true);
        kikoutCharacterButton.gameObject.SetActive(false);

        addCharacterButton.onClick.AddListener(WaitForCharacterSelect);
    }

    public void Initialize(Services services, IndustrialStructure industrialStructure, Character character)
    {
        if (character != null)
        {
            this.services = services;
            _industrialStructure = industrialStructure;
            _character = character;

            portrait.sprite = character.Portrait;
            nameText.text = character.CharacterName;

            kikoutCharacterButton.gameObject.SetActive(true);
            addCharacterButton.gameObject.SetActive(false);

            kikoutCharacterButton.onClick.AddListener(KikoutCharacter);
        }
        else
        {
            Initialize(services, industrialStructure);
        }
    }

    private void WaitForCharacterSelect()
    {
        services.UIController.ListOfAblebodiedCharacters.Open(_industrialStructure);
    }

    private void KikoutCharacter()
    {
        _industrialStructure.CharacterPlaces.KickOut(_character);
    }
}
