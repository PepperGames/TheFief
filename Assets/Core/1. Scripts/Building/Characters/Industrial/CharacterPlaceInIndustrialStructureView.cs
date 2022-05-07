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

    private IndustrialStructure industrialStructure;

    public void Initialize(Services services, IndustrialStructure industrialStructure)
    {
        this.services = services;
        this.industrialStructure = industrialStructure;

        portrait.sprite = unknownPortrait;
        nameText.text = unknownName;

        addCharacterButton.gameObject.SetActive(true);
        kikoutCharacterButton.gameObject.SetActive(false);

        addCharacterButton.onClick.AddListener(WaitForCharacterSelect);
    }

    public void Initialize(Services services, IndustrialStructure industrialStructure, Character character)
    {
        Debug.Log("character" + character);

        if (character != null)
        {
            this.services = services;
            this.industrialStructure = industrialStructure;

            portrait.sprite = character.Portrait;
            nameText.text = character.CharacterName;

            kikoutCharacterButton.gameObject.SetActive(true);
            addCharacterButton.gameObject.SetActive(false);
        }
        else
        {
            Initialize(services, industrialStructure);
        }
    }

    private void WaitForCharacterSelect()
    {
        services.UIController.ListOfAblebodiedCharacters.Open(industrialStructure);
    }
}
