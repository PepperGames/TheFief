using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AblebodiedCharactersView : MonoBehaviour
{
    public Image portrait;
    public TMP_Text nameText;
    public TMP_Text workplaceText;

    public void Initialize(Character character)
    {
        Debug.Log("character" + character);
        if (character != null)
        {
            portrait.sprite = character.Portrait;
            nameText.text = character.CharacterName;
        }
    }
}
