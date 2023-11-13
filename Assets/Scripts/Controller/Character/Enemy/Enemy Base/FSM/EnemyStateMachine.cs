using Controller.Character.Enemy.Enemy_Base.State;
using UnityEngine;

namespace Controller.Character.Enemy.Enemy_Base.FSM
{
    public class EnemyStateMachine
    {
        public EnemyState CurrentState { get; private set; }

        public void Init(EnemyState state)
        {
            CurrentState = state;
            CurrentState.OnEnter();
        }

        public void TranslateToState(EnemyState state)
        {
            CurrentState.OnExit();
            CurrentState = state;
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