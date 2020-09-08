using System.Collections;
using GameInput;
using UnityEngine;

namespace Gameplay
{
    // script for controlling camera with generic input
    public class CameraController : MonoBehaviour
    {
        private GameInputBase _input => InputManager.Instance.Input;  // accessing input

        private float _verticalLookRotation;                          // saving vertical rotation to clamp it in update

        private bool _blockUpdate = false;                            // block update if auto aim

        void Update()
        {
            if (_blockUpdate)
                return;

            _verticalLookRotation += _input.CameraVerticalAxis();                                    // getting vertical axis
            _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, -90, 90);    // and clamping to -90 and 90 values

            transform.Rotate(Vector3.up, _input.CameraHorizontalAxis(), Space.World); // just rotate on horizontal axis because no need to clamp

            Vector3 localEuler = transform.localEulerAngles;                                                        // converting to localEulerAngles and updating transform
            transform.localEulerAngles = new Vector3(_verticalLookRotation, localEuler.y, localEuler.z);
        }

        public void SetLookRotation(Vector3 position, float time)            // start coroutine for auto aim
        {
            StartCoroutine(RotateOnAngles(position, time));
        }

        private IEnumerator RotateOnAngles(Vector3 position, float time)
        {
            _blockUpdate = true;

            Vector3 lookDir = (position - transform.position).normalized;                            // direction for desired look

            Quaternion startRotation = transform.rotation;                                           // caching start rotation
            Quaternion endRotation = Quaternion.LookRotation(lookDir, Vector3.up);  // calculating look rotation from start to desired

            float t = 0f;
            while (t <= time)                                                                        // while time for aim
            {
                var newRotation = Quaternion.Slerp(startRotation, endRotation, t / time);  // lerping rotations
                var euler = newRotation.eulerAngles;
                euler.z = 0f;                                                                                   // zeroing out z rotation 

                transform.localEulerAngles = euler;                                                             // updating transform
                t += Time.deltaTime;
                yield return null;
            }

            _verticalLookRotation = ConvertToSignedAngle(transform.localEulerAngles.x);                        // setting _verticalLookRotation to new value 
            _blockUpdate = false;                                                                              // if not - then it will have rotation between 0 and 360
        }                                                                                                      // and high chance it will be clamped improperly in update

        private float ConvertToSignedAngle(float positiveAngle)
        {
            if (positiveAngle >= 0 && positiveAngle <= 90)  // if between 0 and 90 leave as is
                return positiveAngle;
            else                                            // if higher - minus 360 to convert to signed angle
                return positiveAngle - 360f;
        }
    }
}