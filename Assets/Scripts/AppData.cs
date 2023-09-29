using UnityEngine;

[CreateAssetMenu(fileName = "Appdata")]
public class AppData : ScriptableObject
{
    private Vector3 posPlayer = new Vector3(10.506f, 0.214f, 10.353f);
    
    [Header("WinCondition")]
    public bool _lvl_1_succeeded;
    public bool _lvl_2_succeeded;
    public bool _lvl_3succeeded;
}