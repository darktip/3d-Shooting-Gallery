using System.IO;
using Data;
using Patterns.State_Machine;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.States
{
    public class EndGameState : State<GameController>
    {
        private bool _win;
        
        public EndGameState(StateMachine<GameController> stateMachine, bool win) : base(stateMachine)
        {
            _win = win;
        }

        public override void EnterState(GameController owner)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            if (_win)
            {
                Database.Instance.AddScore(owner.CalculateFinalScore());
                UIController.Instance.OpenScreen(UIController.Instance.GameWindows.winScreen);
            }
            else
            {
                UIController.Instance.OpenScreen(UIController.Instance.GameWindows.looseScreen);
            }
        }
    }
}
