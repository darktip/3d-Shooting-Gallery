using System;
using GameInput;
using UnityEngine;

namespace Gameplay
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private LayerMask shootLayer;
        [SerializeField] private GameSettings sphereSettings;

        private GameInputBase _input => InputManager.Instance.Input;
        private Camera _camera;

        private float _maxDist;

        private void Start()
        {
            _camera = Camera.main;
            _maxDist = sphereSettings ? sphereSettings.Radius : 500;
        }

        private void Update()
        {
            if (_input.Shoot())
            {
                Shoot();
            }
        }

        protected virtual void Shoot()
        {
            Ray ray = _camera.ScreenPointToRay(_input.CursorPosition());

            Debug.DrawRay(ray.origin, ray.direction);
            if (Physics.Raycast(ray, out RaycastHit hit, _maxDist))
            {
                var shootable = hit.collider.GetComponent<IShootable>();
                shootable?.Shot();
            }
        }
    }
}