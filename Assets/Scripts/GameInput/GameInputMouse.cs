using System;
using UnityEngine;

namespace GameInput
{
    [CreateAssetMenu(fileName = "New Mouse Input", menuName = "Input/MouseInput", order = 50)]
    public class GameInputMouse : GameInputBase
    {
        [SerializeField] private int shootMouseButton;
        [SerializeField] private float mouseIntensity;
        [SerializeField] private bool flipVertical;
        [SerializeField] private bool flipHorizontal;
        
        private void OnDisable()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public override void Init()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public override float CameraHorizontalAxis()
        {
            float f = flipHorizontal ? -1 : 1;
            return Input.GetAxis("Mouse X") * mouseIntensity * f;
        }

        public override float CameraVerticalAxis()
        {
            float f =  flipVertical ? -1 : 1;
            return - Input.GetAxis("Mouse Y") * mouseIntensity * f;
        }

        public override bool Shoot()
        {
            return Input.GetMouseButtonDown(shootMouseButton);
        }

        public override Vector2 CursorPosition()
        {
            return Input.mousePosition;
        }
    }
}