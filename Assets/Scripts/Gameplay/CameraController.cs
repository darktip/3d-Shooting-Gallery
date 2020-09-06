using GameInput;
using UnityEngine;

namespace Gameplay
{
    public class CameraController : MonoBehaviour
    {
        private GameInputBase _input;

        private float _verticalLookRotation;

        void Start()
        {
            _input = InputManager.Instance.Input;
        }

        void Update()
        {
            _verticalLookRotation += -_input.CameraVerticalAxis();
            _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, -90, 90);

            transform.Rotate(Vector3.up, _input.CameraHorizontalAxis(), Space.World);

            Vector3 localEuler = transform.localEulerAngles;
            transform.localEulerAngles = new Vector3(_verticalLookRotation, localEuler.y, localEuler.z);
        }
    }
}