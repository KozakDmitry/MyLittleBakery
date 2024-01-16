using System;

namespace Assets.CodeBase.Infostructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly LoadingScreen _loadingScreen;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine gameStateMachine, LoadingScreen loadingScreen, SceneLoader sceneLoader)
        {
            _stateMachine = gameStateMachine;
            _loadingScreen = loadingScreen;
            _sceneLoader = sceneLoader;
        }
        public void Enter(string nameScene)
        {
            _loadingScreen.Show();
            _sceneLoader.LoadScene(nameScene, onLoaded);

        }

        private void onLoaded()
        {
            InitUIRoot();
            InitLocalization();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InitUIRoot()
        {
            throw new NotImplementedException();
        }

        private void InitLocalization()
        {
            throw new NotImplementedException();
        }

        public void Exit() =>
            _loadingScreen.Hide();
    }
}