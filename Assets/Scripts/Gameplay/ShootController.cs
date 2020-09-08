using GameInput;
using Gameplay.Settings;
using UnityEngine;

namespace Gameplay
{
    // class responsible for raycasting targets
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private LayerMask shootLayer;                // layer mask
        [SerializeField] private GameSettings sphereSettings;         // settings to determine raycast max distance

        private GameInputBase _input => InputManager.Instance.Input;
        private Camera _camera;                                 // caching camera

        private float _maxDist;

        private void Start()
        {
            _camera = Camera.main;
            _maxDist = sphereSettings ? sphereSettings.Radius : 500; // if no settings - put some high value
        }

        private void Update()
        {
            if (_input.Shoot())    // if shoot button pressed
            {
                Shoot();
            }
        }

        protected virtual void Shoot()
        {
            // simple raycast that checked is object we hit has 
            // interface we need, and calling Shot method if found
            
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