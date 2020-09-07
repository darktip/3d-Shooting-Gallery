using UnityEngine;

namespace Gameplay.Settings
{
    [CreateAssetMenu(fileName = "New Game Settings", menuName = "Game Settings", order = 50)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private float radius;
        [SerializeField] private float spacing;
        [SerializeField] private float targetsSpeed;
        [SerializeField] private int winTargetsCount;
        [SerializeField] private float betweenTargetsTime;
        [SerializeField] private float autoAimTime;
        [SerializeField] private float showWayTime;
        [SerializeField] private int scoreToStartMovingTargets;
        
        public float Spacing => spacing;
        public float Radius => radius;
        public float TargetsSpeed => targetsSpeed;
        public int WinTargetsCount => winTargetsCount;
        public float BetweenTargetsTime => betweenTargetsTime;
        public float AutoAimTime => autoAimTime;
        public float ShowWayTime => showWayTime;
        public int ScoreToStartMovingTargets => scoreToStartMovingTargets;
    }
}
