using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityTransformBridge
{
    private Dictionary<int, GameObject> gameObjects = new();
    private Transform tilesParent;
    private GameObject tilePrefab;

    public UnityTransformBridge(Transform tilesParent, GameObject tilePrefab)
    {
        this.tilesParent = tilesParent;
        this.tilePrefab = tilePrefab;
    }

    public void SyncToUnity(ref MusicNoteTransformData musicNoteTransformData)
    {
        int entityId = -1;
        for (int i = 0; i < musicNoteTransformData.count; i++)
        {
            entityId = musicNoteTransformData.entityIDs.Get(i);
            if (!gameObjects.TryGetValue(entityId, out GameObject go))
            {
                go = GameObject.Instantiate(tilePrefab, tilesParent);
                gameObjects[entityId] = go;
            }

            go.transform.position = musicNoteTransformData.positions.Get(i);
        }
    }

    public void Cleanup()
    {
        foreach (var go in gameObjects.Values)
        {
            if (go != null)
                GameObject.Destroy(go);
        }
        gameObjects.Clear();
    }
}
