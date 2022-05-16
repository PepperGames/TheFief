using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeaveFromTownPopup : MonoBehaviour, IClosable
{
    [SerializeField] private Image _portraitImage;
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Text _description;

    public void Initialize(Sprite portrait, string name)
    {
        _closeButton.onClick.AddListener(Close);
        _portraitImage.sprite = portrait;
        _description.text = $"<b>{name}</b> left town because he was unhappy";
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
