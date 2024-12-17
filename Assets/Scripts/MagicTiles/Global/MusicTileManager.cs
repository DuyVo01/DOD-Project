using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTileManager : MonoBehaviour
{
    private MusicTileWorld musicTileWorld;

    // Start is called before the first frame update
    void Start()
    {
        musicTileWorld = new MusicTileWorld();
        musicTileWorld.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        musicTileWorld.Update();
    }
}
