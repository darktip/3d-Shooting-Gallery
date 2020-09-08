using System;
using UnityEngine;

namespace GameInput
{
    // Base class for all user inputs
    public abstract class GameInputBase : ScriptableObject
    {
        public abstract void Init();                         // for initial initialization if needed
        public abstract float CameraHorizontalAxis();        // rotate camera left-right
        public abstract float CameraVerticalAxis();          // rotate camera up-down
        public abstract bool Shoot();                        // shoot button
        public abstract Vector2 CursorPosition();            // position of cursor on the screen
    }
}
