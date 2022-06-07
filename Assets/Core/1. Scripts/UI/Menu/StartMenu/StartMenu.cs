using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _choiceOfRuler;

    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;

    private void Start()
    {
        _startMenu.SetActive(true);
        _choiceOfRuler.SetActive(false);

        _startGameButton.onClick.AddListener(ShowChoiceOfRuler);
        _playButton.onClick.AddListener(Play);
        _exitButton.onClick.AddListener(Exit);
    }

    public void ShowChoiceOfRuler()
    {
        _startMenu.SetActive(false);
        _choiceOfRuler.SetActive(true);
    }

    public void Play()
    {
        string sceneName = "MainScene";
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
