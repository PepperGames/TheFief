using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeathPopup : MonoBehaviour, IClosable
{
    [SerializeField] private Image _portraitImage;
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Text _description;

    public void Initialize(Sprite portrait, string name, int age)
    {
        _closeButton.onClick.AddListener(Close);
        _portraitImage.sprite = portrait;
        _description.text = $"{name} died at the age {age}. Our condolebces";
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
