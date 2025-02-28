using UnityEngine;

public struct InputSystem : IGameSystem
{
    private bool wasMousePressed;

    public InputSystem(bool fake = true)
    {
        wasMousePressed = false;
    }

    public void ProcessInput()
    {
        ref var inputData = ref SingletonComponentRepository.GetComponent<InputDataComponent>(
            SingletonComponentType.Input
        );

        UpdateInputStates(ref inputData);

        inputData.activeInputCount = 0;
        if (Input.touchCount > 0)
        {
            ProcessTouchInput(ref inputData);
        }
        else
        {
            ProcessMouseInput(ref inputData);
        }

        //LogInputDebugInfo(ref inputData);
    }

    private void ProcessMouseInput(ref InputDataComponent inputData)
    {
        // Always process mouse input every frame
        bool isCurrentlyPressed = Input.GetMouseButton(0);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var currentState = inputData.inputStates.Get(0);
        var newState = DetermineNewInputState(isCurrentlyPressed, wasMousePressed);

        // Only update if state changed or position changed
        if (newState != currentState.State || worldPos != currentState.Position)
        {
            UpdateInputSlot(ref inputData, 0, worldPos, newState);
            if (newState != InputState.None)
            {
                inputData.activeInputCount = 1;
            }
        }

        wasMousePressed = isCurrentlyPressed;
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

    private void ProcessTouchInput(ref InputDataComponent inputData)
    {
        int touchCount = Mathf.Min(Input.touchCount, InputDataComponent.MAX_INPUTS);

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

            UpdateInputSlot(ref inputData, i, worldPos, newState);
            inputData.activeInputCount++;
        }
    }

    private void UpdateInputSlot(
        ref InputDataComponent inputData,
        int slot,
        Vector2 position,
        InputState newState
    )
    {
        var currentState = inputData.inputStates.Get(slot);

        currentState.PreviousPosition = currentState.Position;
        currentState.Position = position;
        currentState.State = newState;

        if (newState != currentState.State)
        {
            currentState.FrameCount = 0;
        }
        currentState.FrameCount++;

        inputData.inputStates.Set(slot, currentState);
        inputData.isActives.Set(slot, newState != InputState.None);
    }

    private void UpdateInputStates(ref InputDataComponent inputData)
    {
        for (int i = 0; i < InputDataComponent.MAX_INPUTS; i++)
        {
            var state = inputData.inputStates.Get(i);

            if (state.State == InputState.JustReleased)
            {
                state.State = InputState.None;
                state.FrameCount = 0;
                inputData.inputStates.Set(i, state);
                inputData.isActives.Set(i, false);
            }
        }
    }

    private void LogInputDebugInfo(ref InputDataComponent inputData)
    {
        for (int i = 0; i < InputDataComponent.MAX_INPUTS; i++)
        {
            if (inputData.isActives.Get(i))
            {
                var state = inputData.inputStates.Get(i);
                Debug.Log(
                    $"Input {i} - State: {state.State}, Position: {state.Position}, Frame Count: {state.FrameCount}"
                );
            }
        }
    }
}
