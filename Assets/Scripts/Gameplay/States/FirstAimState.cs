using System.Collections;
using System.Collections.Generic;
using GameInput;
using Patterns.State_Machine;
using UnityEngine;

namespace Gameplay.States
{
    public class FirstAimState : State<GameController>
    {
        private GameInputBase _previousInput;
        
        public FirstAimState(StateMachine<GameController> stateMachine) : base(stateMachine)
        {
            
        }

        public override void EnterState(GameController owner)
        {
            _previousInput = InputManager.Instance.Input;
            InputManager.Instance.SetInput(owner.NoneInput);  // turns off input

            var cameraController = Object.FindObjectOfType<CameraController>();  // finding cameracontroller

            owner.StartCoroutine(AimingForTarget(owner, cameraController)); // aiming for first target
        }

        private IEnumerator AimingForTarget(GameController owner, CameraController cameraController)
        {
            cameraController.SetLookRotation(owner.CurrentTarget.transform.position, owner.Settings.AutoAimTime);

            yield return new WaitForSeconds(owner.Settings.AutoAimTime);
            
            InputManager.Instance.SetInput(_previousInput); // set input back to what it was before

            stateMachine.SetState(new ShootTargetState(stateMachine));  // set shoot target state
        }
    }
}
