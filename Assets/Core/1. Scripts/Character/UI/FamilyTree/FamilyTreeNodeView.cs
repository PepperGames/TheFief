using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FamilyTreeNodeView : MonoBehaviour
{
    [SerializeField] private Image _portrait;
    [SerializeField] private TMP_Text _nameText;

    public void Initialize(Sprite portrait, string characterName)
    {
        _portrait.sprite = portrait;
        _nameText.text = characterName;
    }
}
