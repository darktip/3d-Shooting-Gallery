using System;
using System.Collections.Generic;
using Constants;
using Patterns.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    // controls windows lifecycle
    public class UIController : Singleton<UIController>
    {
        [SerializeField] private RectTransform screenHolder;
        [SerializeField] private GameWindows gameWindows;
        [SerializeField] private Canvas canvas;

        private WindowBase _lastScreen;
        public GameWindows GameWindows => gameWindows;

        private Camera _camera;
        public Camera MenuCamera => _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                if(scene.name == StringConstants.GameScene)
                    _camera.enabled = false;
            };
        }

        private void Start()
        {
            OpenScreen(gameWindows.mainWindow);      // open main menu on start
        }

        public void Update()
        {
            canvas.gameObject.SetActive(screenHolder.transform.childCount > 0); // hide canvas if no screen
        }

        public void HideCurrentScreen() // destroys previous screen
        {
            if (_lastScreen != null)
            {
                _lastScreen.Hide();
                Destroy(_lastScreen.gameObject);
            }

            _lastScreen = null;
        }

        public void OpenScreen(WindowBase window)
        {
            HideCurrentScreen();     // hide previous screen

            var instance = WindowBase.Create(window);  // instantiate new
            window.Show();                                        // call show method

            instance.transform.SetParent(screenHolder, false);  // set parent to windowHolder

            _lastScreen = instance.GetComponent<WindowBase>();                   // set reference to last screen
        }
    }
}