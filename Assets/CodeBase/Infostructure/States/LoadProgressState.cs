using Assets.CodeBase.Data;
using Assets.CodeBase.Infostructure.Services.ProgressService;
using Assets.CodeBase.Infostructure.Services.SaveLoadService;
using System;

namespace Assets.CodeBase.Infostructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IProgressService progressService, ISaveLoadService saveLoadService)
        {
            _stateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }
        public void Enter()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<LoadLevelState, string>("Menu");
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.playerData =
                _saveLoadService.LoadAll()
                ?? NewProgress();
        }

        private PlayerData NewProgress()
        {
            PlayerData playerData = new PlayerData();
            return playerData;
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}