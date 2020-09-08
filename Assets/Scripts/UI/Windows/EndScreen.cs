using System;
using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Windows
{
    // end screen derives from highscore menu
    // because it is exactly the same with oen new button 
    // and different text
    public class EndScreen : HighScoreMenu
    {
        [SerializeField] private Button restartButton;

        private AsyncOperation _unloadingGame;
        
        protected override void Awake()
        {
            base.Awake();

            // Unload game scene on start
            UIController.Instance.MenuCamera.enabled = true;
            _unloadingGame = SceneManager.UnloadSceneAsync(StringConstants.GameScene);

            restartButton.onClick.AddListener(Restart);
            restartButton.interactable = false;
        }

        private void Update()
        {
            if (_unloadingGame.isDone)                // not show restart button before scene is unloaded
                restartButton.interactable = true;
        }

        protected override void Back()
        {
            UIController.Instance.OpenScreen(UIController.Instance.GameWindows.mainWindow);
        }

        protected virtual void Restart()
        {
            // load scene and hide current screen
            SceneManager.LoadScene(StringConstants.GameScene, LoadSceneMode.Additive);
            UIController.Instance.HideCurrentScreen();
        }
    }
}
