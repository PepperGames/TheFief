using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPlaceInIndustrialStructureView : MonoBehaviour
{
    public Sprite unknownPortrait;
    public string unknownName;

    public Image portrait;
    public TMP_Text nameText;

    public void Initialize()
    {
        portrait.sprite = unknownPortrait;
        nameText.text = unknownName;
    }

    public void Initialize(Character character)
    {
        Debug.Log("character" + character);
        if (character != null)
        {
            portrait.sprite = character.Portrait;
            nameText.text = character.CharacterName;
        }
        else
        {
            Initialize();
        }
    }
}
