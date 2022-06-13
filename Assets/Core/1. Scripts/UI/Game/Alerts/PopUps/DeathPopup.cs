using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeathPopup : MonoBehaviour, IClosable
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Text _description;

    public void Initialize(string name, int age)
    {
        _closeButton.onClick.AddListener(Close);
        _description.text = $"<b>{name}</b> died at the age <b>{age}</b>. <br> Our condolebces";
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
