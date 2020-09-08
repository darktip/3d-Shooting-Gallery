using System;
using UnityEngine;

namespace GameInput
{
    // On Android use gyro input
    [CreateAssetMenu(fileName = "New Gyro Input", menuName = "Input/GyroInput", order = 50)]
    public class GameInputGyro : GameInputBase
    {
        // field for specific adjustments
        [SerializeField] private float gyroIntensity = 1;
        [SerializeField] private bool flipVertical;
        [SerializeField] private bool flipHorizontal;

        public override void Init()
        {
            Input.gyro.enabled = true; // enable gyro on init
        }

        public override float CameraHorizontalAxis()
        {
            float f = flipHorizontal ? -1 : 1;                       // if flipped multiply float to -1 to get opposite value
            return - Input.gyro.rotationRate.y * gyroIntensity * f;  // reading gyro and multiplying by intensity and flip values
         }

        public override float CameraVerticalAxis()                   // same as above but another axis
        {
            float f = flipVertical ? -1 : 1;
            return - Input.gyro.rotationRate.x * gyroIntensity * f;
        }

        public override bool Shoot()                                 // shoot when first finger is on began state
        {
            return Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began;
        }

        public override Vector2 CursorPosition()                
        {
            return new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);     // return center of the screen 
        }
    }
}