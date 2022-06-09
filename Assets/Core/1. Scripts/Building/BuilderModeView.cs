using System;
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

    public bool IsActive
    {
        get
        {
            return isActive;
        }
        private set
        {
            isActive = value;
            OnModeChange?.Invoke();
        }
    }
    public static BuilderModeView Instance => _instance;

    public Action OnModeChange;

    private void Start()
    {
        _instance = this;
        _button.onClick.AddListener(ChangeMod);
        IsActive = false;
        _buttonImage.sprite = _notActiveSprite;
    }

    private void ChangeMod()
    {
        if (IsActive == true)
        {
            IsActive = false;
            _buttonImage.sprite = _notActiveSprite;
        }
        else
        {
            IsActive = true;
            _buttonImage.sprite = _activeSprite;
        }
    }
}
