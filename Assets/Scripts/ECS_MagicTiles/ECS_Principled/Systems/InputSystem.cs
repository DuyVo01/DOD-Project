using ECS_MagicTile.Components;
using UnityEngine;
using static ECS_MagicTile.DelegateTypes;

namespace ECS_MagicTile
{
    public class InputSystem : GameSystemBase
    {
        private const int MAX_INPUTS = 2;
        private const string LOG_PREFIX = "[Input System] ";
        private bool wasMousePressed;

        public EGameState GameStateToExecute => EGameState.IngamePlaying;

        protected override void Initialize()
        {
            // Create input entities
            for (int i = 0; i < MAX_INPUTS; i++)
            {
                var inputComponent = new InputStateComponent
                {
                    IsActive = false,
                    Position = Vector2.zero,
                    PreviousPosition = Vector2.zero,
                    State = InputState.None,
                    FrameCount = 0,
                };

                World.CreateEntity(inputComponent);
            }
        }

        protected override void Execute(float deltaTime)
        {
            // Update previous states
            World
                .CreateQuery()
                .ForEach<InputStateComponent>(
                    (ref InputStateComponent inputState) =>
                    {
                        if (inputState.State == InputState.JustReleased)
                        {
                            inputState.State = InputState.None;
                            inputState.FrameCount = 0;
                            inputState.IsActive = false;
                        }
                        else if (inputState.State == InputState.JustPressed)
                        {
                            inputState.State = InputState.Held;
                        }
                    }
                );

            // Process new input
            if (Input.touchCount > 0)
            {
                ProcessTouchInput();
            }
            else
            {
                ProcessMouseInput();
            }
        }

        private void ProcessMouseInput()
        {
            bool isCurrentlyPressed = Input.GetMouseButton(0);
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // We'll use the query system to find and update the first input state
            bool updated = false;
            int index = 0;

            World
                .CreateQuery()
                .ForEach<InputStateComponent>(
                    (ref InputStateComponent inputState, int entityId) =>
                    {
                        if (!updated && index == 0) // Process only the first input
                        {
                            InputState newState = DetermineNewInputState(
                                isCurrentlyPressed,
                                wasMousePressed
                            );

                            // Only update if state changed or position changed
                            if (newState != inputState.State || worldPos != inputState.Position)
                            {
                                UpdateInputState(ref inputState, worldPos, newState);
                            }

                            updated = true;
                        }
                        index++;
                    }
                );

            wasMousePressed = isCurrentlyPressed;
        }

        private void ProcessTouchInput()
        {
            int touchCount = Mathf.Min(Input.touchCount, MAX_INPUTS);

            // First reset all input states
            int index = 0;
            World
                .CreateQuery()
                .ForEach<InputStateComponent>(
                    (ref InputStateComponent inputState, int entityId) =>
                    {
                        if (index >= touchCount && inputState.IsActive)
                        {
                            inputState.State = InputState.None;
                            inputState.IsActive = false;
                        }
                        index++;
                    }
                );

            // Now process active touches
            for (int i = 0; i < touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                Vector2 worldPos = Camera.main.ScreenToWorldPoint(touch.position);
                InputState newState;

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        newState = InputState.JustPressed;
                        break;
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        newState = InputState.Held;
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        newState = InputState.JustReleased;
                        break;
                    default:
                        newState = InputState.None;
                        break;
                }

                // Find the corresponding input state entity
                int touchIndex = i;
                index = 0;
                World
                    .CreateQuery()
                    .ForEach<InputStateComponent>(
                        (ref InputStateComponent inputState, int entityId) =>
                        {
                            if (index == touchIndex)
                            {
                                UpdateInputState(ref inputState, worldPos, newState);
                            }
                            index++;
                        }
                    );
            }
        }

        private void UpdateInputState(
            ref InputStateComponent state,
            Vector2 position,
            InputState newState
        )
        {
            state.PreviousPosition = state.Position;
            state.Position = position;
            state.State = newState;
            state.IsActive = newState != InputState.None;

            if (newState != state.State)
            {
                state.FrameCount = 0;
            }
            state.FrameCount++;
        }

        private InputState DetermineNewInputState(bool isPressed, bool wasPressed)
        {
            if (isPressed && !wasPressed)
                return InputState.JustPressed;
            if (!isPressed && wasPressed)
                return InputState.JustReleased;
            if (isPressed)
                return InputState.Held;
            return InputState.None;
        }
    }
}
