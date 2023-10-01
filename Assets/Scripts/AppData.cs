using UnityEngine;

[CreateAssetMenu(fileName = "Appdata")]
public class AppData : ScriptableObject
{
    public Vector3 posPlayer;
    
    [Header("WinCondition")]
    public bool _lvl_1_succeeded;
    public bool _lvl_2_succeeded;
    public bool _lvl_3_succeeded;
    public bool _finishGame;
}