using System;
using UnityEngine;

namespace GameInput
{
    public abstract class GameInputBase : ScriptableObject
    {
        public abstract void Init();
        public abstract float CameraHorizontalAxis();
        public abstract float CameraVerticalAxis();
        public abstract bool Shoot();
        public abstract Vector2 CursorPosition();
    }
}
