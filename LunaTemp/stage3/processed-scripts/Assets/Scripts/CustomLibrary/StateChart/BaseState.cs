using UnityEngine;

namespace StateMachineChart
{
    public abstract class BaseState
    {
        public virtual void Enter() { }

        public virtual void Exit() { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }
    }
}
