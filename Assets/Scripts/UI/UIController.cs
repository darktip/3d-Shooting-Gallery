using System;
using System.Collections.Generic;
using Constants;
using Patterns.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
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
            OpenScreen(gameWindows.mainWindow);
        }

        public void Update()
        {
            canvas.gameObject.SetActive(screenHolder.transform.childCount > 0);
        }

        public void HideCurrentScreen()
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
            HideCurrentScreen();

            var instance = WindowBase.Create(window);
            window.Show();

            instance.transform.SetParent(screenHolder, false);

            _lastScreen = instance.GetComponent<WindowBase>();
        }
    }
}