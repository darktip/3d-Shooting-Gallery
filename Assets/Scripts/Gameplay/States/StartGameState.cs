using System.Collections;
using System.Collections.Generic;
using Patterns.State_Machine;
using UnityEngine;

namespace Gameplay.States
{
    public class StartGameState : State<GameController>
    {
        public StartGameState(StateMachine<GameController> stateMachine) : base(stateMachine)
        {
            
        }

        public override void EnterState(GameController owner)
        {
            owner.StartCoroutine(WaitForTargetSpawner(owner));
        }

        private IEnumerator WaitForTargetSpawner(GameController owner)
        {
            yield return new WaitUntil(() => owner.Spawner.TargetsReady);
            
            var randomTarget = owner.Spawner.GetRandomTarget();
            randomTarget.Select();

            stateMachine.SetState(new FirstAimState(stateMachine));
        }
    }
}
