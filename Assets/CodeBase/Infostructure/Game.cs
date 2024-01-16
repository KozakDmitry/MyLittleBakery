
using Assets.CodeBase.Infostructure.Services;
using Assets.CodeBase.Infostructure.States;

namespace Assets.CodeBase.Infostructure
{
    public class Game
    {
        public GameStateMachine _stateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen)
        {
            _stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingScreen, AllServices.Container);
        }
    }
}
