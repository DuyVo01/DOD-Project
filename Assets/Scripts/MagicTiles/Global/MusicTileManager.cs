using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTileManager : MonoBehaviour
{
    private MusicTileWorld musicTileWorld;
    private LaneLineWorld laneLineWorld;

    // Start is called before the first frame update
    void Start()
    {
        musicTileWorld = new MusicTileWorld();
        laneLineWorld = new LaneLineWorld();
    }

    // Update is called once per frame
    void Update()
    {
        musicTileWorld.Update();
        laneLineWorld.Update();
    }
}
