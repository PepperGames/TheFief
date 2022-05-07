using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AblebodiedCharactersView : MonoBehaviour
{
    public Image portrait;
    public TMP_Text nameText;
    public TMP_Text workplaceText;

    public Button addCharacterButton;

    private Services services;

    private IndustrialStructure industrialStructure;
    private Character character;

    public void Initialize(IndustrialStructure industrialStructure, Character character)
    {
        Debug.Log("character" + character);
        if (character != null)
        {
            this.character = character;
            this.industrialStructure = industrialStructure;
            portrait.sprite = character.Portrait;
            nameText.text = character.CharacterName;
            if (character.Workplace == null)
            {
                addCharacterButton.gameObject.SetActive(true);
                addCharacterButton.onClick.AddListener(AddToWorkplace);
            }
            else
            {
                addCharacterButton.gameObject.SetActive(false);
            }
        }
    }

    private void AddToWorkplace()
    {
        Debug.Log(character);
        Debug.Log(industrialStructure);
        Debug.Log(industrialStructure.CharacterPlaces);
        industrialStructure.CharacterPlaces.AddCharacter(character);
    }
}
