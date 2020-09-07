using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Constants;
using Patterns.Singleton;
using TMPro;
using UnityEngine;

namespace Data
{
    public class Database : Singleton<Database>
    {
        [SerializeField] private SaveData _data;
        public SaveData Data => _data;

        public void Load()
        {
            _data = new SaveData();

            try
            {
                using (var fs =
                    new FileStream(Path.Combine(Application.persistentDataPath, StringConstants.SaveDataPath),
                        FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    try
                    {
                        _data = (SaveData) formatter.Deserialize(fs);
                        Debug.Log("Database loaded!");
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Failed to deserialize database: " + e.Message);
                    }
                }
            }
            catch (FileNotFoundException)
            {
            }
        }

        public void Save()
        {
            using (var fs = new FileStream(Path.Combine(Application.persistentDataPath, StringConstants.SaveDataPath),
                FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                try
                {
                    formatter.Serialize(fs, _data);
                    Debug.Log("Database saved!");
                }
                catch (Exception e)
                {
                    Debug.Log("Failed to serialize database: " + e.Message);
                }
            }
        }

        public void AddScore(int score)
        {
            _data.scores.Add(score);
            Save();
        }

        public string GetSettingsJson()
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, StringConstants.JsonStreamingAssetsPath);

            try
            {
                if (Application.platform == RuntimePlatform.Android) //Need to extract file from apk first
                {
                    WWW reader = new WWW(filePath);
                    while (!reader.isDone)
                    {
                    }

                    return reader.text;
                }
                else
                {
                    return File.ReadAllText(filePath);
                }
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}