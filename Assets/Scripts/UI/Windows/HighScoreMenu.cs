using System;
using System.ComponentModel;
using Constants;
using Data;
using TMPro;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class HighScoreMenu : WindowBase
    {
        [SerializeField] protected Button backButton;
        [SerializeField] protected RectTransform scoreItemPrefab;
        [SerializeField] protected RectTransform scoreItemsPanel;

        private GameWindows _windows;

        protected virtual void Awake()
        {
            _windows = UIController.Instance.GameWindows;

            backButton.onClick.AddListener(Back);
        }

        protected virtual void Start()
        {
            // loading and sorting score data
            int[] scores = (from s in Database.Instance.Data.scores orderby s select s).ToArray();
            
            // creating score entries and setting height to evenly distribute across panel
            for (int i = 0, j = 0; i < IntConstants.HighScoreMenuScoreItems && j < scores.Length; i++, j++)
            {
                var si = Instantiate(scoreItemPrefab, scoreItemsPanel);
                si.sizeDelta = new Vector2(si.sizeDelta.x,
                    scoreItemsPanel.rect.height / IntConstants.HighScoreMenuScoreItems);

                var text = si.GetComponentInChildren<TextMeshProUGUI>();
                if (text)  // setting text
                {
                    text.text = scores[j].ToString();
                }
            }
        }

        protected virtual void Back()
        {
            UIController.Instance.OpenScreen(_windows.mainWindow);
        }
    }
}