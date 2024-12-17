using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDebugger : PersistentSingleton<GizmoDebugger>
{
    [SerializeField]
    private float gizmosSize;
    private Vector2[,] cornersToDraw;
    private int count;

    public void InitData(int capacity)
    {
        cornersToDraw = new Vector2[capacity, 4];
        count = capacity;
    }

    public void UpdateData(int index, int cornerIndex, Vector2 corner)
    {
        cornersToDraw[index, cornerIndex] = corner;
    }

    void OnDrawGizmos()
    {
        if (cornersToDraw == null || cornersToDraw.Length == 0)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            Gizmos.DrawWireSphere(cornersToDraw[i, 0], gizmosSize);
            Gizmos.DrawWireSphere(cornersToDraw[i, 1], gizmosSize);
            Gizmos.DrawWireSphere(cornersToDraw[i, 2], gizmosSize);
            Gizmos.DrawWireSphere(cornersToDraw[i, 3], gizmosSize);
        }
    }
}
