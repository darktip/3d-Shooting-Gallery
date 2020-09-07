using System;
using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Windows
{
    public class EndScreen : HighScoreMenu
    {
        [SerializeField] private Button restartButton;

        private AsyncOperation _unloadingGame;
        
        protected override void Awake()
        {
            base.Awake();

            UIController.Instance.MenuCamera.enabled = true;
            _unloadingGame = SceneManager.UnloadSceneAsync(StringConstants.GameScene);

            restartButton.onClick.AddListener(Restart);
            restartButton.interactable = false;
        }

        private void Update()
        {
            if (_unloadingGame.isDone)
                restartButton.interactable = true;
        }

        protected override void Back()
        {
            UIController.Instance.OpenScreen(UIController.Instance.GameWindows.mainWindow);
        }

        protected virtual void Restart()
        {
            SceneManager.LoadScene(StringConstants.GameScene, LoadSceneMode.Additive);
            UIController.Instance.HideCurrentScreen();
        }
    }
}
