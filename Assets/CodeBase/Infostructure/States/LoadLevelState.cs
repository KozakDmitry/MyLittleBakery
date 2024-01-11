namespace Assets.CodeBase.Infostructure.States
{
    public class LoadLevelState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public LoadLevelState(GameStateMachine gameStateMachine)
        {
            _stateMachine = gameStateMachine;
        }
        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}