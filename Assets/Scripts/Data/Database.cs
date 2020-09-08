using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Constants;
using Patterns.Singleton;
using TMPro;
using UnityEngine;

namespace Data
{
    // class for loading and saving game data
    public class Database : Singleton<Database>
    {
        [SerializeField] private SaveData _data;
        public SaveData Data => _data;

        public void Load()
        {
            _data = new SaveData(); // create new data

            try // try catch if something goes wrong
            {
                using (var fs =
                    new FileStream(Path.Combine(Application.persistentDataPath, StringConstants.SaveDataPath),
                        FileMode.Open)) // opening file
                {
                    BinaryFormatter formatter = new BinaryFormatter(); 

                    try
                    {
                        _data = (SaveData) formatter.Deserialize(fs); // deserialize binary data to local variable
                        Debug.Log("Database loaded!");
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Failed to deserialize database: " + e.Message);
                    }
                }
            }
            catch (FileNotFoundException) // if file is not found we just end up with blank SaveData object
            {
            }
        }

        public void Save()
        {
            using (var fs = new FileStream(Path.Combine(Application.persistentDataPath, StringConstants.SaveDataPath),
                FileMode.Create)) // creating file
            {
                BinaryFormatter formatter = new BinaryFormatter();

                try
                {
                    formatter.Serialize(fs, _data); // serialize data to binary file
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
            _data.scores.Add(score); // add score to SaveData object
            Save();                  // and save to file
        }

        public string GetSettingsJson() // gets settings json from streamingAssetsPath
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, StringConstants.JsonStreamingAssetsPath);

            try
            {
                if (Application.platform == RuntimePlatform.Android) // if android using WWW to extract text, because File.ReadAllText won't work
                {                                                    // because streaming assets on android is not a folder but jar archive
                    WWW reader = new WWW(filePath);
                    while (!reader.isDone)                           // waiting until read all text
                    {
                    }

                    return reader.text;
                }
                else
                {
                    return File.ReadAllText(filePath);               // on windows just read file
                }
            }
            catch (Exception e)
            {
                return "";                                           // return empty if something goes wrong
            }
        }
    }
}