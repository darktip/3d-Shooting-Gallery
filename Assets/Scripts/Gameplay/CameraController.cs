using System.Collections;
using GameInput;
using UnityEngine;

namespace Gameplay
{
    public class CameraController : MonoBehaviour
    {
        private GameInputBase _input => InputManager.Instance.Input;

        private float _verticalLookRotation;

        private bool _blockUpdate = false;

        void Update()
        {
            if (_blockUpdate)
                return;

            _verticalLookRotation += _input.CameraVerticalAxis();
            _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, -90, 90);

            transform.Rotate(Vector3.up, _input.CameraHorizontalAxis(), Space.World);

            Vector3 localEuler = transform.localEulerAngles;
            transform.localEulerAngles = new Vector3(_verticalLookRotation, localEuler.y, localEuler.z);
        }

        public void SetLookRotation(Vector3 position, float time)
        {
            StartCoroutine(RotateOnAngles(position, time));
        }

        private IEnumerator RotateOnAngles(Vector3 position, float time)
        {
            _blockUpdate = true;

            Vector3 lookDir = (position - transform.position).normalized;

            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.LookRotation(lookDir, Vector3.up);

            float t = 0f;
            while (t <= time)
            {
                var newRotation = Quaternion.Slerp(startRotation, endRotation, t / time);
                var euler = newRotation.eulerAngles;
                euler.z = 0f;

                transform.localEulerAngles = euler;
                t += Time.deltaTime;
                yield return null;
            }

            _verticalLookRotation = ConvertToSignedAngle(transform.localEulerAngles.x);
            _blockUpdate = false;
        }

        private float ConvertToSignedAngle(float positiveAngle)
        {
            if (positiveAngle >= 0 && positiveAngle <= 90)
                return positiveAngle;
            else 
                return positiveAngle - 360f;
        }
    }
}