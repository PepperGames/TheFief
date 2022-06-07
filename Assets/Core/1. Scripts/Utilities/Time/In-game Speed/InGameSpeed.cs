using UnityEngine;
using UnityEngine.UI;

public class InGameSpeed : MonoBehaviour
{
    private static int _speed = 1;

    [Header("Button")]
    [SerializeField] private Button _speedX1Button;
    [SerializeField] private Button _speedX2Button;
    [SerializeField] private Button _speedX3Button;

    [Header("Image")]
    [SerializeField] private Image _speedX1Image;
    [SerializeField] private Image _speedX2Image;
    [SerializeField] private Image _speedX3Image;

    [Header("Active Sprite")]
    [SerializeField] private Sprite _speedX1Active;
    [SerializeField] private Sprite _speedX2Active;
    [SerializeField] private Sprite _speedX3Active;

    [Header("Not Active Sprite")]
    [SerializeField] private Sprite _speedX1NotActive;
    [SerializeField] private Sprite _speedX2NotActive;
    [SerializeField] private Sprite _speedX3NotActive;

    public static int Speed => _speed;

    private void Start()
    {
        ChangeSpeed(1);

        _speedX1Button.onClick.AddListener(() => { ChangeSpeed(1); });
        _speedX2Button.onClick.AddListener(() => { ChangeSpeed(2); });
        _speedX3Button.onClick.AddListener(() => { ChangeSpeed(3); });
    }

    private void ChangeSpeed(int speed)
    {
        _speed = speed;
        Display();
    }

    private void ClearAll()
    {
        _speedX1Image.sprite = _speedX1NotActive;
        _speedX2Image.sprite = _speedX2NotActive;
        _speedX3Image.sprite = _speedX3NotActive;
    }

    private void Display()
    {
        ClearAll();
        switch (_speed)
        {
            case 1:
                _speedX1Image.sprite = _speedX1Active;
                break;
            case 2:
                _speedX2Image.sprite = _speedX2Active;
                break;
            case 3:
                _speedX3Image.sprite = _speedX3Active;
                break;

            default:
                break;
        }
    }
}
