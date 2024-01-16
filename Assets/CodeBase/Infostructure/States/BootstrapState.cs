
using Assets.CodeBase.Infostructure.Services;
using Assets.CodeBase.Infostructure.Services.ProgressService;
using Assets.CodeBase.Infostructure.Services.SaveLoadService;
using System;

namespace Assets.CodeBase.Infostructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine, AllServices services)
        {
            _stateMachine = gameStateMachine;
            _services = AllServices.Container;
            _services = services;
            RegisterServices();
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IProgressService>(new ProgressService());
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService());
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