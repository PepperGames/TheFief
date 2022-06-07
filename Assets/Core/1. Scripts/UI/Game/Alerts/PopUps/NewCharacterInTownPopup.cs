using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewCharacterInTownPopup : MonoBehaviour, IClosable
{
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Image _portraitImage;

    [SerializeField] private Button _closeButton;

    [SerializeField] private float _duration;

    public void Initialize(CharacterData characterData)
    {
        _descriptionText.text = $"Congratulations, <b>{characterData.CharacterName}</b> is a new resident of the Town";
        _portraitImage.sprite = characterData.Portrait;

        _closeButton.onClick.AddListener(Close);
    }

    private void Update()
    {
        _duration -= Time.deltaTime * InGameSpeed.Speed; 
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
