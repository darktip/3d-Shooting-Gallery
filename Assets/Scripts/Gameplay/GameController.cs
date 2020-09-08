using System;
using GameInput;
using Gameplay.Settings;
using Gameplay.States;
using Patterns.State_Machine;
using UnityEngine;

namespace Gameplay
{
    // class holding valuable information for game
    public class GameController : MonoBehaviour
    {
        // input for noneInput
        [SerializeField] private GameInputNone noneInput;
        
        [Header("Game Settings")] [SerializeField]             // reference to game settings SO
        private GameSettings gameSettings;

        [Header("Scene References")] [SerializeField]          // reference to targets spawner
        private TargetSpawner targetSpawner;

        private StateMachine<GameController> _stateMachine;    // state machine for controlling gameplay loop
        public GameInputNone NoneInput => noneInput;
        public TargetSpawner Spawner => targetSpawner;
        public GameSettings Settings => gameSettings;

        public Target CurrentTarget { get; private set; }     // Current target you need to shoot
        public int Score { get; private set; }                // targets hit score

        public float FullTime { get; private set; }           // full time from start
        public float Time { get; private set; }               // time from new target appeared

        private void Awake()
        {
            _stateMachine = new StateMachine<GameController>(this);  // creating state machine
        }

        private void OnEnable()
        {
            Target.OnTargetSelect += OnTargetSelect;                // subscribing to select and shot static target events
            Target.OnTargetShot += OnTargetShot;
        }

        private void OnDisable()
        {
            Target.OnTargetSelect -= OnTargetSelect;                // unsubscribing from events because they are static
            Target.OnTargetShot -= OnTargetShot;                    // and won't be deleted with GO destruction
        }

        void Start()
        {
            _stateMachine.SetState(new StartGameState(_stateMachine));    // start game state
        }

        private void Update()
        {
            _stateMachine.Update();            // update state machine
        }

        public void ProceedTime(float deltaTime)    // proceeds time
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

            if (Score == gameSettings.ScoreToStartMovingTargets)  // if we shot enough targets - start moving
            {
                foreach (var t in targetSpawner.Targets)
                {
                    t.Move(true);
                }
            }
        }

        private void OnTargetSelect(Target target)
        {
            CurrentTarget = target;                                // update current target reference
        }
    }
}