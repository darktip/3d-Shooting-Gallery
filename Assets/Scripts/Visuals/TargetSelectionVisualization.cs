using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay;
using UnityEngine;

namespace Visuals
{
    // visualizes new selected target by having all of it's materials 
    // emit color (lighting up target)
    [RequireComponent(typeof(Target))]
    public class TargetSelectionVisualization : MonoBehaviour
    {
        [SerializeField] private Color _emissionColor;
        
        private Target _target;
        private IEnumerable<Material> _materials;
        
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        private void Awake()
        {
            _target = GetComponent<Target>();
            _materials = GetComponentsInChildren<MeshRenderer>().Select(mr => mr.material);
        }

        private void OnEnable()
        {
            Target.OnTargetSelect += OnTargetSelect;
        }

        private void OnDisable()
        {
            Target.OnTargetSelect -= OnTargetSelect;    // don't forget to unsubscribe from static event
        }

        private void OnTargetSelect(Target target)
        {
            ColorChange(target == _target);
        }

        private void ColorChange(bool selected)
        {
            Color color = selected ? _emissionColor : Color.black;

            foreach (var mat in _materials)
            {
                mat.SetColor(EmissionColor, color * mat.color);
                mat.EnableKeyword("_EMISSION");
            }
        }
    }
}
