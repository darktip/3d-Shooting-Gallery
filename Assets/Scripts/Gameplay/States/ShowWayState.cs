﻿using Patterns.State_Machine;
using UnityEngine;

namespace Gameplay.States
{
    public class ShowWayState : State<GameController>
    {
        private float _showWayTime;

        private GameController _owner;
        
        public ShowWayState(StateMachine<GameController> stateMachine) : base(stateMachine)
        {
        
        }

        public override void EnterState(GameController owner)
        {
            _showWayTime = Time.time + owner.Settings.ShowWayTime;
            _owner = owner;
            Target.OnTargetShot += OnTargetShot;

            SelectNewTarget(owner);
        }

        public override void ExitState(GameController owner)
        {
            Target.OnTargetShot -= OnTargetShot;
        }

        public override void UpdateState(GameController owner)
        {
            if (Time.time > _showWayTime)
            {
                stateMachine.SetState(new ShootTargetState(stateMachine));
            }
        }

        private void SelectNewTarget(GameController owner)
        {
            int count = 10;
            while (true)
            {
                var target = owner.Spawner.GetRandomTarget();
                if (target.IsNeighbour(owner.CurrentTarget) && --count > 0)
                    continue;

                target.Select();
                break;
            }
        }

        protected virtual void OnTargetShot(Target target)
        {
            if (target == _owner.CurrentTarget)
            {
                _owner.IncrementScore();

                if (_owner.Score == _owner.Settings.WinTargetsCount)
                {
                    stateMachine.SetState(new EndGameState(stateMachine));
                }
                else
                {
                    stateMachine.SetState(new ShowWayState(stateMachine));
                }
            }
            else
            {
                stateMachine.SetState(new EndGameState(stateMachine));
            }
        }
    }
}
