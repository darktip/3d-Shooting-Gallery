﻿using System;
using System.Collections;
using Gameplay;
using Gameplay.Settings;
using UnityEngine;

namespace Visuals
{
    [RequireComponent(typeof(LineRenderer))]
    public class NextTargetVisualization : MonoBehaviour
    {
        [SerializeField] private int numberOfSegments = 10;    // number of points to constuct line
        [SerializeField] private GameSettings gameSettings;

        private LineRenderer _lineRenderer;

        private Target _previousTarget;                        // references to next and previous
        private Target _nextTarget;                            // targets to draw line

        private Coroutine _animationCoroutine; // coroutine for animation of line appearance

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = numberOfSegments + 1;    // setting up line
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
            UpdateVisualization();        // update on update (because targets will be moving and it should update line)
        }

        public void UpdateVisualization()
        {
            if (_previousTarget == null || _nextTarget == null)  // if no previous return
                return;

            _lineRenderer.enabled = true;

            Vector3[] positions = new Vector3[numberOfSegments + 1];

            // calculating points by spherical interpolation of cartesian coordinates of prev and next target
            // can't linearly interpolationg between spherical values because line will not be on Big circle
            // which is smallest distance from point to point on sphere
            for (int i = 0; i <= numberOfSegments; i++)
            {
                Vector3 p1 = _previousTarget.SpherePosition.ToCartesian();
                Vector3 p2 = _nextTarget.SpherePosition.ToCartesian();

                Vector3 np = Vector3.Slerp(p1, p2, i / (float) numberOfSegments);

                positions[i] = np;
            }

            _lineRenderer.SetPositions(positions);  // setting new points
        }

        public virtual void AnimateLine()
        {
            if (_animationCoroutine != null)
                StopCoroutine(_animationCoroutine);

            _animationCoroutine = StartCoroutine(LineAnimation());
        }

        private IEnumerator LineAnimation()        // Animatie gradient of line renderer
        {
            var keys = new GradientAlphaKey[2];
            var colKeys = _lineRenderer.colorGradient.colorKeys;

            keys[0].alpha = 1;
            keys[1].alpha = 0;
            keys[0].time = 0;
            keys[1].time = 0;

            float t = 0f;
            float time = gameSettings.ShowWayTime;
            while (t <= time)
            {
                float val = Mathf.Lerp(0, 1, t / time);
                keys[1].alpha = val;
                keys[1].time = val;

                Gradient c = new Gradient();
                c.SetKeys(colKeys, keys);

                _lineRenderer.colorGradient = c;
                t += Time.deltaTime;
                yield return null;
            }
        }

        private void OnTargetSelected(Target target)
        {
            _previousTarget = _nextTarget;
            _nextTarget = target;

            AnimateLine(); // start animation when new target is selected
        }
    }
}