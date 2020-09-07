using Patterns.State_Machine;
using UnityEngine;

namespace Gameplay.States
{
    public class ShootTargetState : State<GameController>
    {
        private float _shootUntilTime;
        
        private GameController _owner;
        
        public ShootTargetState(StateMachine<GameController> stateMachine) : base(stateMachine)
        {
            
        }

        public override void EnterState(GameController owner)
        {
            _shootUntilTime = Time.time + owner.Settings.BetweenTargetsTime;
            _owner = owner;
            
            Target.OnTargetShot += OnTargetShot;
        }

        public override void ExitState(GameController owner)
        {
            Target.OnTargetShot -= OnTargetShot;
        }

        public override void UpdateState(GameController owner)
        {
            owner.ProceedTime(Time.deltaTime);

            if (Time.time > _shootUntilTime)
            {
                stateMachine.SetState(new ShowWayState(stateMachine));
            }
        }

        protected virtual void OnTargetShot(Target target)
        {
            if (target == _owner.CurrentTarget)
            {
                _owner.IncrementScore();

                if (_owner.Score == _owner.Settings.WinTargetsCount)
                {
                    stateMachine.SetState(new EndGameState(stateMachine, true));
                }
                else
                {
                    stateMachine.SetState(new ShowWayState(stateMachine));
                }
            }
            else
            {
                stateMachine.SetState(new EndGameState(stateMachine, false));
            }
        }
    }
}
