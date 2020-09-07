using System;
using Patterns.Singleton;
using UnityEngine;

namespace GameInput
{
    public class InputManager : Singleton<InputManager>
    {
        [SerializeField] private GameInputBase gameInput;

        private void Awake()
        {
            if (gameInput)
                gameInput.Init();
        }

        public GameInputBase Input => gameInput;
        
        public void SetInput(GameInputBase input)
        {
            gameInput = input;
            input.Init();
        }
    }
}