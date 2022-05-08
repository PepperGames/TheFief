using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPlaceInResidentialStructureView : MonoBehaviour
{
    public Sprite unknownPortrait;
    public string unknownName;

    public Image portrait;
    public TMP_Text nameText;

    public Button addCharacterButton;
    public Button kikoutCharacterButton;

    private Services services;

    private ResidentialStructure _residentialStructure;
    private Character _character;

    public void Initialize(Services services, ResidentialStructure residentialStructure)
    {
        this.services = services;
        _residentialStructure = residentialStructure;
        portrait.sprite = unknownPortrait;
        nameText.text = unknownName;

        addCharacterButton.gameObject.SetActive(true);
        kikoutCharacterButton.gameObject.SetActive(false);

        addCharacterButton.onClick.AddListener(WaitForCharacterSelect);
    }

    public void Initialize(Services services, ResidentialStructure residentialStructure, Character character)
    {
        if (character != null)
        {
            this.services = services;
            _residentialStructure = residentialStructure;
            _character = character;

            portrait.sprite = character.Portrait;
            nameText.text = character.CharacterName;

            kikoutCharacterButton.gameObject.SetActive(true);
            addCharacterButton.gameObject.SetActive(false);

            kikoutCharacterButton.onClick.AddListener(KikoutCharacter);
        }
        else
        {
            Initialize(services, residentialStructure);
        }
    }

    private void WaitForCharacterSelect()
    {
        services.UIController.ListOfLivingCharacters.Open(_residentialStructure);
    }

    private void KikoutCharacter()
    {
        _residentialStructure.CharacterPlaces.KickOut(_character);
    }
}
