using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class InputSystem : IGameSystem
    {
        private const int MAX_INPUTS = 2;
        private const string LOG_PREFIX = "[Input System] ";
        private bool wasMousePressed;

        public bool IsEnabled { get; set; } = true;
        public World World { get; set; }

        public EGameState GameStateToExecute => EGameState.IngamePlaying;

        public void SetWorld(World world)
        {
            World = world;
        }

        public void RunInitialize()
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

                World.CreateEntityWithComponents(
                    Archetype.Registry.Input,
                    new object[] { inputComponent }
                );
            }
        }

        public void RunUpdate(float deltaTime)
        {
            ArchetypeStorage inputStorage = World.GetStorage(Archetype.Registry.Input);
            var inputStates = inputStorage.GetComponents<InputStateComponent>();

            // Update previous states
            UpdatePreviousStates(inputStates);

            // Process new input
            if (Input.touchCount > 0)
            {
                ProcessTouchInput(inputStates);
            }
            else
            {
                ProcessMouseInput(inputStates);
            }
        }

        private void UpdatePreviousStates(InputStateComponent[] inputStates)
        {
            for (int i = 0; i < MAX_INPUTS; i++)
            {
                if (inputStates[i].State == InputState.JustReleased)
                {
                    inputStates[i].State = InputState.None;
                    inputStates[i].FrameCount = 0;
                    inputStates[i].IsActive = false;
                }
                else if (inputStates[i].State == InputState.JustPressed)
                {
                    inputStates[i].State = InputState.Held;
                }
            }
        }

        private void ProcessMouseInput(InputStateComponent[] inputStates)
        {
            bool isCurrentlyPressed = Input.GetMouseButton(0);
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            ref var currentState = ref inputStates[0];
            InputState newState = DetermineNewInputState(isCurrentlyPressed, wasMousePressed);

            // Only update if state changed or position changed
            if (newState != currentState.State || worldPos != currentState.Position)
            {
                UpdateInputState(ref currentState, worldPos, newState);
            }

            wasMousePressed = isCurrentlyPressed;
        }

        private void ProcessTouchInput(InputStateComponent[] inputStates)
        {
            int touchCount = Mathf.Min(Input.touchCount, MAX_INPUTS);

            // Reset unused input slots
            for (int i = touchCount; i < MAX_INPUTS; i++)
            {
                if (inputStates[i].IsActive)
                {
                    inputStates[i].State = InputState.None;
                    inputStates[i].IsActive = false;
                }
            }

            // Process active touches
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

                UpdateInputState(ref inputStates[i], worldPos, newState);
            }
        }

        private void UpdateInputState(
            ref InputStateComponent state,
[Bridge.Ref]             Vector2 position,
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

        public void RunCleanup()
        {
            // Nothing to cleanup for now
        }
    }
}
