using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputDebuggerBridge : IBridge
{
    private int spawnCount;

    public InputDebuggerBridge(bool fake)
    {
        this.spawnCount = 0;
    }

    public void SpawnDebuggerAtInputPressed()
    {
        ref var presenterManager = ref PresenterManagerRepository.GetManager<PresenterManager>(
            PresenterManagerType.InputDebuggerPresenterManager
        );

        ref var inputData = ref SingletonComponentRepository.GetComponent<InputDataComponent>(
            SingletonComponentType.Input
        );

        GameObject presenter;

        for (int inputIdx = 0; inputIdx < InputDataComponent.MAX_INPUTS; inputIdx++)
        {
            if (!inputData.isActives.Get(inputIdx))
                continue;

            var inputState = inputData.inputStates.Get(inputIdx);

            // Only check collisions on JustPressed state
            if (inputState.State != InputState.JustPressed)
            {
                continue;
            }

            presenter = presenterManager.GetOrCreatePresenter(spawnCount);

            presenter.transform.position = inputData.inputStates.Get(inputIdx).Position;
            Debug.Break();
        }
    }
}
