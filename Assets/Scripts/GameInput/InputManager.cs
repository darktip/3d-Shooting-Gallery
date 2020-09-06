using Patterns.Singleton;
using UnityEngine;

namespace GameInput
{
    public class InputManager : Singleton<InputManager>
    {
        [SerializeField] private GameInputBase gameInput;

        public GameInputBase Input => gameInput;
        
        public void SetInput(GameInputBase input)
        {
            gameInput = input;
        }
    }
}