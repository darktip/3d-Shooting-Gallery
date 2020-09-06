using UnityEngine;

namespace Gameplay
{
    public class TargetSpawner : MonoBehaviour
    {
        [SerializeField] private SphereSettings sphereSettings;

        [SerializeField] private GameObject targetPrefab;
    
        void Start()
        {
            Spawn();
        }

        public void Spawn()
        {
            int vertical = (int) (Mathf.PI / 2 / sphereSettings.Spacing);

            for (int i = 0; i <= vertical; i++)
            {
                float r = Mathf.Cos(Mathf.PI / 2 - i * sphereSettings.Spacing);

                int horizontal = (int) (r * 2 * Mathf.PI / sphereSettings.Spacing);
                horizontal = horizontal == 0 ? 1 : horizontal;

                for (int j = 0; j < horizontal; j++)
                {
                    VectorSphere sphericalPos = new VectorSphere(i * sphereSettings.Spacing,j * 2 * Mathf.PI / horizontal, sphereSettings.Radius);
            
                    var target = Instantiate(targetPrefab, transform);
                
                    var targetComponent = target.GetComponent<Target>();
                    targetComponent.Init(sphereSettings, sphericalPos, 2f * Mathf.Pow(-1, i) / horizontal);
                }   
            }
        }
    }
}