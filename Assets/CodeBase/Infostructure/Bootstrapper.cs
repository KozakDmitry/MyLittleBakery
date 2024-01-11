using Assets.CodeBase.Infostructure.States;
using UnityEngine;

namespace Assets.CodeBase.Infostructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public Game _game;
        private void Awake()
        {
            _game = new Game(this);
            _game._stateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}
