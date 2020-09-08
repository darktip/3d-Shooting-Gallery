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
            _shootUntilTime = Time.time + owner.Settings.BetweenTargetsTime;  // time until target is active
            _owner = owner;
            
            Target.OnTargetShot += OnTargetShot;             // subscribe to event when shot target
        }

        public override void ExitState(GameController owner)
        {
            Target.OnTargetShot -= OnTargetShot;               // unsubscribe
        }

        public override void UpdateState(GameController owner)
        {
            owner.ProceedTime(Time.deltaTime);                // proceed time on controller

            if (Time.time > _shootUntilTime)
            {
                stateMachine.SetState(new ShowWayState(stateMachine));    // if run out of time - select new state that shows way to target
            }
        }

        protected virtual void OnTargetShot(Target target)  // on shoot some target
        {
            if (target == _owner.CurrentTarget) // if shot target that in selected as next
            {
                _owner.IncrementScore();            // increment score

                if (_owner.Score == _owner.Settings.WinTargetsCount) // if score is enough - win
                {
                    stateMachine.SetState(new EndGameState(stateMachine, true));
                }
                else
                {
                    stateMachine.SetState(new ShowWayState(stateMachine)); // else show way to new target
                }
            }
            else
            {
                stateMachine.SetState(new EndGameState(stateMachine, false));  // if shot not right target - loose
            }
        }
    }
}
