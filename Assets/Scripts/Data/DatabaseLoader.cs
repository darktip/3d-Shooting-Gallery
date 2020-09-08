using System;
using System.IO;
using Gameplay.Settings;
using UnityEngine;

namespace Data
{
    // Monobehaviour responsible for loading data to Database
    // and overwriting json data to SO gamesettings
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
                JsonUtility.FromJsonOverwrite(json, gameSettings); // overwrites all serializable fields from json
            }
        }
    }
}
