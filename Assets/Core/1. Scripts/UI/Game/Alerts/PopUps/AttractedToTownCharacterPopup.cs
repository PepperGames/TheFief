using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AttractedToTownCharacterPopup : MonoBehaviour, IClosable
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _portraitImage;
    [SerializeField] private TMP_Text _ageText;
    [SerializeField] private TMP_Text _genderText;
    [SerializeField] private TMP_Text _estatesText;

    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _closeButton;

    private CharacterData _characterData;
    private Services _services;

    public void Initialize(Services services, CharacterData characterData)
    {
        _characterData = characterData;
        _services = services;

        _nameText.text = characterData.CharacterName;
        _portraitImage.sprite = characterData.Portrait;
        _ageText.text = characterData.Age.years.ToString();
        _genderText.text = characterData.Gender.ToString();
        _estatesText.text = characterData.Estates.ToString();

        _closeButton.onClick.AddListener(Close);
        _acceptButton.onClick.AddListener(Accept);
    }

    public void Close()
    {
        Destroy(gameObject);
    }

    public void Accept()
    {
        _services.CharacterManager.SpawnCharacter(_characterData);
        Destroy(gameObject);
    }
}
