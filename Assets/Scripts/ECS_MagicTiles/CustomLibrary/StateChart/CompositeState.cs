using System;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachineChart
{
    public class CompositeState : BaseState
    {
        private BaseState[] _substates;
        private BaseState _currentState;
        private int _substateCount;
        private const int DEFAULT_CAPACITY = 8;

        public BaseState CurrentState => _currentState;

        public CompositeState(int capacity = DEFAULT_CAPACITY)
        {
            _substates = new BaseState[capacity];
            _substateCount = 0;
        }

        public override void Enter()
        {
            if (_currentState == null && _substateCount > 0)
            {
                SetState(_substates[0]);
            }
        }

        public override void Exit() { }

        public override void FixedUpdate() => _currentState?.FixedUpdate();

        public override void Update() => _currentState?.Update();

        public void AddSubstate(BaseState state)
        {
            if(_substateCount == _substates.Length)
            {
                Array.Resize(ref _substates, _substateCount * 2);
            }
            _substates[_substateCount++] = state;
        }

        public void SetState(BaseState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }
}
