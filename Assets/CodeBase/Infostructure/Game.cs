
using Assets.CodeBase.Infostructure.States;

namespace Assets.CodeBase.Infostructure
{
    public class Game
    {
        public GameStateMachine _stateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            _stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner));
        }
    }
}
