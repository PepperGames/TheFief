using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BabyWasBornPopup : MonoBehaviour, IClosable
{
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Image _portraitImage;

    [SerializeField] private Button _closeButton;

    [SerializeField] private float _duration;

    public void Initialize(CharacterData motherData, CharacterData fatherData, CharacterData childData)
    {
        _descriptionText.text = $"Congratulations, <b>{motherData.CharacterName}</b> and <b>{fatherData.CharacterName}</b> had a baby <b>{childData.CharacterName}</b>";
        _portraitImage.sprite = childData.Portrait;

        _closeButton.onClick.AddListener(Close);
    }

    private void Update()
    {
        _duration -= Time.deltaTime;
        if (_duration <= 0)
        {
            Close();
        }
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
