using UnityEngine;

namespace Gameplay.Settings
{
    // Scriptable object that holds gameplay settings
    [CreateAssetMenu(fileName = "New Game Settings", menuName = "Game Settings", order = 50)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private float radius;                    // radius of constructed sphere in meters
        [SerializeField] private float spacing;                   // spacing between each target on sphere in radians
        [SerializeField] private float targetsSpeed;              // speed of target when they will move
        [SerializeField] private int winTargetsCount;             // how much target to shoot to win
        [SerializeField] private float betweenTargetsTime;        // time between each target
        [SerializeField] private float autoAimTime;               // time to aim to first target
        [SerializeField] private float showWayTime;               // time on which game timer stops to show way to target
        [SerializeField] private int scoreToStartMovingTargets;   // how much targets to shoot to targets starting moving
        
        // public properties for accessing values;
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
