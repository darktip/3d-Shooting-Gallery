using Patterns.State_Machine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.States
{
    public class EndGameState : State<GameController>
    {
        public EndGameState(StateMachine<GameController> stateMachine) : base(stateMachine)
        {
            
        }

        public override void EnterState(GameController owner)
        {
            SceneManager.LoadScene("Game");
            Debug.Log("End of the Game!");
        }
    }
}
