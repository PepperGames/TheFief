using UnityEngine;
using UnityEngine.UI;

public class CharacterTraitView : MonoBehaviour
{
    [SerializeField] private Image _image;

    [SerializeField] private CharacterTrait _characterTrait;

    public void Initialize(CharacterTrait characterTrait)
    {
        _characterTrait = characterTrait;
        InitializeInfo();
    }

    private void InitializeInfo()
    {
        _image.sprite = _characterTrait.Sprite;
    }
}
