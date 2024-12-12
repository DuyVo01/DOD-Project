using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTileManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset midiContent;

    [SerializeField]
    private Transform tileParent;

    [SerializeField]
    private GameObject tilePrefab;
    private MusicTileWorld musicTileWorld;

    // Start is called before the first frame update
    void Start()
    {
        musicTileWorld = new MusicTileWorld(64, tileParent, tilePrefab);
        musicTileWorld.PopulateNoteData(midiContent.text);
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
