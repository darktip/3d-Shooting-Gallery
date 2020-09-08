using System;
using UnityEngine;

namespace Patterns.Singleton
{
    // Singleton
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance; // one static instance

        public static T Instance
        {
            get
            {
                if (!_instance)                        // if no instance
                {
                    var instances = FindObjectsOfType<T>();  // find all monobehaviour with T type

                    if (instances.Length > 1)                    // if more than one was found
                    {
                        _instance = instances[0];                // selecting first and throwing error to console
                        
                        Debug.LogError(
                            $"There is more than one instance of singleton of type {typeof(T).Name}. But should be only one!");
                    }
                    else if (instances.Length == 1)              // if exact one select it
                    {
                        _instance = instances[0];

                        Debug.Log($"Found instance of singleton of type {typeof(T).Name}!");
                    }
                    else
                    {                                                                                // if no
                        var newSingleton = new GameObject($"{typeof(T).Name} Singleton");      // create new GO
                        var component = newSingleton.AddComponent<T>();                               // add singleton component
                        _instance = component;
                        DontDestroyOnLoad(newSingleton);                                              // dont destroy it on load

                        Debug.Log($"Created instance of singleton of type {typeof(T).Name}!");
                    }
                }

                return _instance;                                                                    // and return instance
            }
        }

        protected virtual void OnDestroy() // removes reference on destroy
        {
            _instance = null;
        }
    }
}