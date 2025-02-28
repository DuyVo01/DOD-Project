using System;

namespace StateMachineChart
{
    public readonly struct Transition
    {
        public BaseState To { get; }
        public Func<bool> Condition { get; }

        public Transition(BaseState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }

    public sealed class TransitionSet
    {
        private Transition[] _transitions;
        private int _count;
        private const int DEFAULT_CAPACITY = 4;

        public int Count => _count;

        public TransitionSet(int capacity = DEFAULT_CAPACITY)
        {
            _transitions = new Transition[capacity];
            _count = 0;
        }

        public void Add(Transition transition)
        {
            if (_count == _transitions.Length)
            {
                Array.Resize(ref _transitions, _count * 2);
            }
            _transitions[_count++] = transition;
        }

        public ref readonly Transition GetTransition(int index)
        {
            return ref _transitions[index];
        }
    }
}
