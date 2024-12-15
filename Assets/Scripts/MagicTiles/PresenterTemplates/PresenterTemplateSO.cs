using UnityEngine;

[CreateAssetMenu(fileName = "PresenterTemplate", menuName = "Presenter/Template Settings")]
public class PresenterTemplateSO : ScriptableObject
{
    [Header("Core GameObject Template")]
    public GameObject basePrefab;
}
