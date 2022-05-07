using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPlaceInIndustrialStructureView : MonoBehaviour
{
    public Sprite unknownPortrait;
    public string unknownName;

    public Image portrait;
    public TMP_Text nameText;

    private Services services;

    public void Initialize(Services services)
    {
        this.services = services;

        portrait.sprite = unknownPortrait;
        nameText.text = unknownName;
    }

    public void Initialize(Services services, Character character)
    {
        Debug.Log("character" + character);

        if (character != null)
        {
            this.services = services;

            portrait.sprite = character.Portrait;
            nameText.text = character.CharacterName;
        }
        else
        {
            Initialize(services);
        }
    }
}
