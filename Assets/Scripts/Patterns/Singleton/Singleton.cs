using System;
using UnityEngine;

namespace Patterns.Singleton
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (!_instance)
                {
                    var instances = FindObjectsOfType<T>();

                    if (instances.Length > 1)
                    {
                        _instance = instances[0];
                        
                        Debug.LogError(
                            $"There is more than one instance of singleton of type {typeof(T).Name}. But should be only one!");
                    }
                    else if (instances.Length == 1)
                    {
                        _instance = instances[0];

                        Debug.Log($"Found instance of singleton of type {typeof(T).Name}!");
                    }
                    else
                    {
                        var newSingleton = new GameObject($"{typeof(T).Name} Singleton");
                        var component = newSingleton.AddComponent<T>();
                        _instance = component;
                        DontDestroyOnLoad(newSingleton);

                        Debug.Log($"Created instance of singleton of type {typeof(T).Name}!");
                    }
                }

                return _instance;
            }
        }

        protected virtual void OnDestroy()
        {
            _instance = null;
        }
    }
}