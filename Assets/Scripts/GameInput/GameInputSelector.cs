using System;
using UnityEngine;

namespace GameInput
{
    // class that selects input method on Awake
    // by target platform
    [RequireComponent(typeof(InputManager))]
    public class GameInputSelector : MonoBehaviour
    {
        [SerializeField] private GameInputMouse mouseInput;
        [SerializeField] private GameInputGyro gyroInput;

        private void Awake()
        {
#if UNITY_ANDROID && ! UNITY_EDITOR
            GetComponent<InputManager>().SetInput(gyroInput);
#else
            GetComponent<InputManager>().SetInput(mouseInput);
#endif
        }
    }
}