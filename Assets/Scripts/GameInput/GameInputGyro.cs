using System;
using UnityEngine;

namespace GameInput
{
    [CreateAssetMenu(fileName = "New Gyro Input", menuName = "Input/GyroInput", order = 50)]
    public class GameInputGyro : GameInputBase
    {
        [SerializeField] private float gyroIntensity = 1;
        [SerializeField] private bool flipVertical;
        [SerializeField] private bool flipHorizontal;

        public override void Init()
        {
            Input.gyro.enabled = true;
        }

        public override float CameraHorizontalAxis()
        {
            float f = flipHorizontal ? -1 : 1;
            return - Input.gyro.rotationRate.y * gyroIntensity * f;
        }

        public override float CameraVerticalAxis()
        {
            float f = flipVertical ? -1 : 1;
            return - Input.gyro.rotationRate.x * gyroIntensity * f;
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