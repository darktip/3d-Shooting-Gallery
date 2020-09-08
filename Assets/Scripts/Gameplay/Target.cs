using System;
using Gameplay.Settings;
using UnityEngine;

namespace Gameplay
{
    // component that each target would be holding
    public class Target : MonoBehaviour, IShootable
    {
        private GameSettings _sphereSettings;            // reference to game settings
        private VectorSphere _spherePos;                 // position in spherical coords

        private float _speed;                            // speed of target
        private bool _move;                              // if should move

        public VectorSphere SpherePosition => _spherePos;

        public static event Action<Target> OnTargetSelect;       // static events
        public static event Action<Target> OnTargetShot;
        
        public void Init(GameSettings sphereSettings, VectorSphere spherePos, float speed)
        {
            _sphereSettings = sphereSettings;
            _spherePos = spherePos;
            _speed = speed;
            
            // initialize position and look at center
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
                _spherePos.phi += _speed * Time.deltaTime;     // update phi value for horizontal movement
             
                transform.localPosition = _spherePos.ToCartesian();  // convert to cartesian
                transform.LookAt(Vector3.zero);                      // look at center
            }
        }

        public bool IsNeighbour(Target other)                       // determining if targets are neighbours 
        {
            // could be done with sphere cast, but if we have spacing between each target we can calculate distance
            // each target should have to neighbours and comparing with actual distance between targets
            float distance = Vector3.Distance(other._spherePos.ToCartesian(), _spherePos.ToCartesian());
            return distance <= GetDistanceToNeighbours();
        }

        public void Select()
        {
            OnTargetSelect?.Invoke(this);        // select next target for shooting
        }

        public void Shot()                            // shooting target. Invoking events with (this) passing
        {
            OnTargetShot?.Invoke(this);
            gameObject.SetActive(false);
        }

        protected virtual float GetDistanceToNeighbours()
        {
            // calculated distance formula on sheet of paper
            // and multiplied by 1.25 to have some bigger sphere of neghbours
            // works with all spacing values, gets neighbours left-right-top-bot and diagonal
            return 2 * _sphereSettings.Radius * Mathf.Sin(_sphereSettings.Spacing * 1.25f / 2);
        }
    }
}
