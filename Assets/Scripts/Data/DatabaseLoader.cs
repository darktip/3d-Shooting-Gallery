using System;
using System.IO;
using Gameplay.Settings;
using UnityEngine;

namespace Data
{
    public class DatabaseLoader : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        
        private void OnEnable()
        {
            Database.Instance.Load();
            OverrideGameSettingsThroughJson();
        }

        private void OverrideGameSettingsThroughJson()
        {
            string json = Database.Instance.GetSettingsJson();

            if (!string.IsNullOrWhiteSpace(json))
            {
                JsonUtility.FromJsonOverwrite(json, gameSettings);
            }
        }
    }
}
