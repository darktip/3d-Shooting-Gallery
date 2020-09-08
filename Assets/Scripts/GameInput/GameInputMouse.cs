using System;
using UnityEngine;

namespace GameInput
{
    // On PC use mouse
    [CreateAssetMenu(fileName = "New Mouse Input", menuName = "Input/MouseInput", order = 50)]
    public class GameInputMouse : GameInputBase
    {
        // field for specific adjustments
        [SerializeField] private int shootMouseButton;
        [SerializeField] private float mouseIntensity;
        [SerializeField] private bool flipVertical;
        [SerializeField] private bool flipHorizontal;
        
        private void OnDisable()
        {
            Cursor.visible = true;                        // On disable  unlock cursor
            Cursor.lockState = CursorLockMode.None;      
        }

        public override void Init()                       // lock cursor on init
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public override float CameraHorizontalAxis()      // reads horizontal axis
        {
            float f = flipHorizontal ? -1 : 1;
            return Input.GetAxis("Mouse X") * mouseIntensity * f;
        }

        public override float CameraVerticalAxis()        // reads vertical axis
        {
            float f =  flipVertical ? -1 : 1;
            return - Input.GetAxis("Mouse Y") * mouseIntensity * f;
        }

        public override bool Shoot()                      // shoot with specified mouse button
        {
            return Input.GetMouseButtonDown(shootMouseButton);
        }

        public override Vector2 CursorPosition()          // returns position of cursor on screen
        {                                                 // because cursor is locked it will be on center
            return Input.mousePosition;
        }
    }
}