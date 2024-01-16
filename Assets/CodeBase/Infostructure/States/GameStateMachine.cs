using Assets.CodeBase.Infostructure.Services;
using Assets.CodeBase.Infostructure.Services.ProgressService;
using Assets.CodeBase.Infostructure.Services.SaveLoadService;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Infostructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private readonly SceneLoader sceneLoader;
        private IExitableState _activeState;


        public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen,AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this,services),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IProgressService>(), services.Single<ISaveLoadService>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this,loadingScreen, sceneLoader),
                [typeof(GameLoopState)] = new GameLoopState(this)

            };
            this.sceneLoader = sceneLoader;
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;

        }
        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;


        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }
      
    }
}