using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTileManager : MonoBehaviour
{
    [SerializeField]
    private Transform tileParent;

    [SerializeField]
    private GameObject tilePrefab;
    private MusicTileWorld musicTileWorld;

    // Start is called before the first frame update
    void Start()
    {
        musicTileWorld = new MusicTileWorld(100, tileParent, tilePrefab);

        musicTileWorld.AddNote(0, 1, 0.1f);
        musicTileWorld.AddNote(1, 2, 0.2f);
        musicTileWorld.AddNote(3, 3, 0.3f);
        musicTileWorld.AddNote(4, 4, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        musicTileWorld.Update();
    }

    private void OnDestroy()
    {
        musicTileWorld.Cleanup();
    }
}
