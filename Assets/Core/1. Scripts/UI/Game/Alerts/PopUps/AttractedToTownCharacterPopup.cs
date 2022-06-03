using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AttractedToTownCharacterPopup : MonoBehaviour, IClosable
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _portraitImage;


    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _closeButton;

    private CharacterData _characterData;
    private Services _services;

    public void Initialize(Services services, CharacterData characterData)
    {
        _characterData = characterData;
        _services = services;

        _text.text = "Name: " + characterData.CharacterName +
            "<br>Age: " + characterData.Age.years.ToString() +
            "<br>Gender: " + characterData.Gender.ToString() +
            "<br>Estates: " + characterData.Estates.ToString();

        _portraitImage.sprite = characterData.Portrait;

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
