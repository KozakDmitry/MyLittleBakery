namespace CodeBase.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public LoadProgressState(GameStateMachine gameStateMachine)
        {
            _stateMachine = gameStateMachine;
        }
        public void Enter()
        {
            
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}