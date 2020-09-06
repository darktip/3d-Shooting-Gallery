using UnityEngine;

namespace GameInput
{
    [CreateAssetMenu(fileName = "New Gyro Input", menuName = "Input/GyroInput", order = 50)]
    public class GameInputGyro : GameInputBase
    {
        public override float CameraHorizontalAxis()
        {
            return Input.gyro.rotationRate.y;
        }

        public override float CameraVerticalAxis()
        {
            return Input.gyro.rotationRate.x;
        }

        public override bool Shoot()
        {
            return Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began;
        }

        public override Vector2 CursorPosition()
        {
            return new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        }
    }
}