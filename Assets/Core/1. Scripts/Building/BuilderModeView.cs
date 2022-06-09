using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuilderModeView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _buttonImage;

    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _notActiveSprite;

    private static BuilderModeView _instance;
    private bool isActive = false;

    public bool IsActive => isActive;
    private static BuilderModeView Instance => _instance;

    private void Start()
    {
        _instance = this;
        _button.onClick.AddListener(ChangeMod);
    }

    private void ChangeMod()
    {
        if (isActive == true)
        {
            isActive = false;
            _buttonImage.sprite = _notActiveSprite;
        }
        else
        {
            isActive = true;
            _buttonImage.sprite = _activeSprite;
        }
    }
}
