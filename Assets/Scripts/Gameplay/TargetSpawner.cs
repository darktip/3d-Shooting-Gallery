using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class TargetSpawner : MonoBehaviour
    {
        [SerializeField] private GameSettings sphereSettings;

        [SerializeField] private GameObject targetPrefab;

        private List<Target> _targetsList;

        public IEnumerable<Target> Targets => _targetsList;

        public bool TargetsReady { get; private set; }

        private void Awake()
        {
            _targetsList = new List<Target>();
        }

        void Start()
        {
            Spawn();
        }

        private void OnEnable()
        {
            Target.OnTargetShot += OnTargetShot;
        }

        private void OnDisable()
        {
            Target.OnTargetShot -= OnTargetShot;
        }

        public void OnTargetShot(Target target)
        {
            _targetsList.Remove(target); // remove target form targets list if it was shot
        }

        public Target GetRandomTarget()  // gets random from the list
        {
            return _targetsList[Random.Range(0, _targetsList.Count)];
        }

        public void Spawn()        // method that instantiates targets on sphere
        {
            int vertical = (int) (Mathf.PI / 2 / sphereSettings.Spacing);                        // determining how much targets we need on each meridian

            for (int i = 0; i <= vertical; i++)                                                  // running through each meridians
            {
                float r = Mathf.Cos(Mathf.PI / 2 - i * sphereSettings.Spacing);               // cos value that works as weight of circumference of each parallel form equator to pole
                                                                                                 // because on equator you need biggest amount of targets and on pole - only one 
                int horizontal = (int) (r * 2 * Mathf.PI / sphereSettings.Spacing);              // so we multiplying this weight by targets we need on each parallel to have weighted
                                                                                                 // (evenly spread) amount of targets through all the sphere
                horizontal = horizontal == 0 ? 1 : horizontal;                                   // for pole it need to be at least one

                for (int j = 0; j < horizontal; j++)                                             // running through parallels
                {
                    VectorSphere sphericalPos = new VectorSphere(i * sphereSettings.Spacing,     //determining position on the sphere with each target
                        j * 2 * Mathf.PI / horizontal, sphereSettings.Radius);

                    var target = Instantiate(targetPrefab, transform);                      // instantiate

                    var targetComponent = target.GetComponent<Target>();
                    // setting speed opposite for neighbouring parallels 
                    targetComponent.Init(sphereSettings, sphericalPos, sphereSettings.TargetsSpeed * Mathf.Pow(-1, i) / horizontal);

                    _targetsList.Add(targetComponent);                                            // adding to the list
                }
            }

            TargetsReady = true;
        }
    }
}