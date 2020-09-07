using System;
using GameInput;
using Gameplay.Settings;
using Gameplay.States;
using Patterns.State_Machine;
using UnityEngine;

namespace Gameplay
{
    public class GameController : MonoBehaviour
    {
        [Header("Input Handlers")] [SerializeField]
        private GameInputMouse mouseInput;

        [SerializeField] private GameInputNone noneInput;

        [Header("Game Settings")] [SerializeField]
        private GameSettings gameSettings;

        [Header("Scene References")] [SerializeField]
        private TargetSpawner targetSpawner;

        private StateMachine<GameController> _stateMachine;
        public GameInputNone NoneInput => noneInput;
        public TargetSpawner Spawner => targetSpawner;
        public GameSettings Settings => gameSettings;

        public Target CurrentTarget { get; private set; }
        public int Score { get; private set; }

        public float FullTime { get; private set; }
        public float Time { get; private set; }

        private void Awake()
        {
            _stateMachine = new StateMachine<GameController>(this);
        }

        private void OnEnable()
        {
            Target.OnTargetSelect += OnTargetSelect;
            Target.OnTargetShot += OnTargetShot;
        }

        private void OnDisable()
        {
            Target.OnTargetSelect -= OnTargetSelect;
            Target.OnTargetShot -= OnTargetShot;
        }

        void Start()
        {
            _stateMachine.SetState(new StartGameState(_stateMachine));
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        public void ProceedTime(float deltaTime)
        {
            Time += deltaTime;
            FullTime += deltaTime;
        }

        public void IncrementScore()
        {
            Score++;
        }

        public int CalculateFinalScore()
        {
            return (int) (FullTime * 10 / gameSettings.WinTargetsCount);
        }

        private void OnTargetShot(Target target)
        {
            Time = 0f;

            if (Score == gameSettings.ScoreToStartMovingTargets)
            {
                foreach (var t in targetSpawner.Targets)
                {
                    t.Move(true);
                }
            }
        }

        private void OnTargetSelect(Target target)
        {
            CurrentTarget = target;
        }
    }
}