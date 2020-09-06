using System;
using Gameplay;
using UnityEngine;

namespace Visuals
{
    [RequireComponent(typeof(LineRenderer))]
    public class NextTargetVisualization : MonoBehaviour
    {
        [SerializeField] private int numberOfSegments = 10;
        
        private LineRenderer _lineRenderer;

        private Target _previousTarget;
        private Target _nextTarget;
        
        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = numberOfSegments + 1;
            _lineRenderer.enabled = false;
        }

        private void OnEnable()
        {
            Target.OnTargetSelect += OnTargetSelected;
        }

        private void OnDisable()
        {
            Target.OnTargetSelect -= OnTargetSelected;
        }
        
        void Update()
        {
            UpdateVisualization();
        }

        public void UpdateVisualization()
        {
            if (_previousTarget == null || _nextTarget == null)
                return;

            _lineRenderer.enabled = true;
            
            Vector3[] positions = new Vector3[numberOfSegments + 1];
            
            for (int i = 0; i <= numberOfSegments; i++)
            {
                Vector3 p1 = _previousTarget.SpherePosition.ToCartesian();
                Vector3 p2 = _nextTarget.SpherePosition.ToCartesian();

                Vector3 np = Vector3.Slerp(p1, p2, i / (float) numberOfSegments);

                positions[i] = np;
            }

            _lineRenderer.SetPositions(positions);
        }

        private void OnTargetSelected(Target target)
        {
            _previousTarget = _nextTarget;
            _nextTarget = target;
        }
    }
}
