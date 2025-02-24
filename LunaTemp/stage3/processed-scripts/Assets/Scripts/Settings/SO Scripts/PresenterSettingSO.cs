using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PresenterSetting", menuName = "Setting/Presenter Settings")]
public class PresenterSettingSO : ScriptableObject
{
    [Header("Music Note Presenter")]
    public GameObject shortMusicNotePresenterPrefab;
    public GameObject longMusicNotePresenterPrefab;

    [Header("Input Deugger Presenter")]
    public GameObject inputDebuggerPresenterPrefab;

    [Header("Lane Line presenter")]
    public GameObject laneLinePresenter;

    [Header("Intro Note Presenter")]
    public GameObject introNotePressenyer;
}
