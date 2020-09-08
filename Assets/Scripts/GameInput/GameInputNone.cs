using UnityEngine;

namespace GameInput
{
    // Null object for no input,
    // we can change to this if we want player not have any input
    [CreateAssetMenu(fileName = "None Input", menuName = "Input/NoneInput", order = 50)]
    public class GameInputNone : GameInputBase
    {
        public override void Init()
        {
            
        }

        public override float CameraHorizontalAxis()
        {
            return 0;
        }

        public override float CameraVerticalAxis()
        {
            return 0;
        }

        public override bool Shoot()
        {
            return false;
        }

        public override Vector2 CursorPosition()
        {
            return Vector2.zero;
        }
    }
}
