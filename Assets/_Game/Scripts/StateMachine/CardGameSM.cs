using Scripts.CardGame;
using Scripts.StateMachine.Base;

namespace Scripts.StateMachine
{
    public class CardGameSM : StateMachineBase
    {
        private void Start()
        {
            ChangeState<SetupCardGameState>();
        }
    }
}