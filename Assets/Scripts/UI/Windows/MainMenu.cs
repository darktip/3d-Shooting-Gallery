using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Windows
{
    public class MainMenu : WindowBase, IWindow
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button highScoreButton;
        [SerializeField] private Button exitButton;

        private GameWindows _windows;
        
        private void Awake()
        {
            _windows = UIController.Instance.GameWindows;

            startGameButton.onClick.AddListener(StartGame);
            highScoreButton.onClick.AddListener(HighScore);
            exitButton.onClick.AddListener(Exit);
        }

        private void HighScore()
        {
            UIController.Instance.OpenScreen(_windows.highScoreWindow);
        }

        private void StartGame()
        {
            SceneManager.LoadScene(StringConstants.GameScene, LoadSceneMode.Additive);
            UIController.Instance.HideCurrentScreen();
        }

        private void Exit()
        {
            Application.Quit();
        }
    }
}
