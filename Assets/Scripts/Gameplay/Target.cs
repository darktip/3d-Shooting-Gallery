using System;
using UnityEngine;

namespace Gameplay
{
    public class Target : MonoBehaviour, IShootable
    {
        private GameSettings _sphereSettings;
        private VectorSphere _spherePos;

        private float _speed;
        private bool _move;

        public VectorSphere SpherePosition => _spherePos;

        public static event Action<Target> OnTargetSelect;
        public static event Action<Target> OnTargetShot; 
        
        public void Init(GameSettings sphereSettings, VectorSphere spherePos, float speed)
        {
            _sphereSettings = sphereSettings;
            _spherePos = spherePos;
            _speed = speed;
            
            transform.localPosition = _spherePos.ToCartesian();
            transform.LookAt(Vector3.zero);
        }

        public void Move(bool move)
        {
            _move = move;
        }

        public void Update()
        {
            if (_move)
            {
                _spherePos.phi += _speed * Time.deltaTime;
             
                transform.localPosition = _spherePos.ToCartesian();
                transform.LookAt(Vector3.zero);
            }
        }

        public bool IsNeighbour(Target other)
        {
            float distance = Vector3.Distance(other._spherePos.ToCartesian(), _spherePos.ToCartesian());
            return distance <= GetDistanceToNeighbours();
        }

        public void Select()
        {
            OnTargetSelect?.Invoke(this);
        }

        public void Shot()
        {
            OnTargetShot?.Invoke(this);
            gameObject.SetActive(false);
        }

        protected virtual float GetDistanceToNeighbours()
        {
            return 2 * _sphereSettings.Radius * Mathf.Sin(_sphereSettings.Spacing * 1.25f / 2);
        }
    }
}
