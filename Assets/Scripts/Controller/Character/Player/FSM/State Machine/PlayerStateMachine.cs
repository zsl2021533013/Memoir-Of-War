using Controller.Character.Player.Player_State.Player_Base_State;

namespace Controller.Character.Player.FSM.State_Machine
{
    public class PlayerStateMachine
    {
        public PlayerState CurrentState { get; private set; }

        public void Initialize(PlayerState playerState)
        {
            CurrentState = playerState;
            CurrentState.OnEnter();
        }

        public void TranslateToState(PlayerState playerState)
        {
            CurrentState.OnExit();
            CurrentState = playerState;
            CurrentState.OnEnter();
        }

        public void OnUpdate()
        {
            CurrentState.OnUpdate();
        }

        public void OnFixedUpdate()
        {
            CurrentState.OnFixedUpdate();
        }
    }
}