using System;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachineChart
{
    public class StateChart
    {
        private CompositeState _rootState;
        private Dictionary<BaseState, TransitionSet> _transitions;

        public StateChart(CompositeState root)
        {
            _rootState = root;
            _transitions = new Dictionary<BaseState, TransitionSet>();

            root.Enter();
        }

        public void AddTransition(BaseState from, BaseState to, Func<bool> condition)
        {
            if (!_transitions.TryGetValue(from, out var transitionSet))
            {
                transitionSet = new TransitionSet();
                _transitions[from] = transitionSet;
            }

            _transitions[from].Add(new Transition(to, condition));
        }

        public void Update()
        {
            CheckTransitions(_rootState);
            _rootState.Update();
        }

        public void FixedUpdate() => _rootState.FixedUpdate();

        public void CheckTransitions(CompositeState state)
        {
            if (state.CurrentState is CompositeState compositeState)
            {
                CheckTransitions(compositeState);
            }

            if (_transitions.TryGetValue(state.CurrentState, out var transitionSet))
            {
                for (int i = 0; i < transitionSet.Count; i++)
                {
                    ref readonly Transition transition = ref transitionSet.GetTransition(i);
                    if(transition.Condition())
                    {
                        state.SetState(transition.To);
                        return; 
                    }
                }
            }
        }
    }
}
