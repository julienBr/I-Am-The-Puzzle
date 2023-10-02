using System.Collections.Generic;
using UnityEngine;

public class TurnScreenManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private AppData _data;
    
    [Header("Screen Off Material")]
    [SerializeField] private Material _screenOffMat;
    
    [Header("Screens Manager")]
    [SerializeField] private List<MeshRenderer> _listScreensPanel;
    [SerializeField] private List<MeshRenderer> _listTutosPanel;
    
    [Header("Colliders")]
    [SerializeField] private List<Collider> _listColliders;

    private void Awake()
    {
        if(_data._lvl_1_succeeded) TurnScreenOf(0);
        if(_data._lvl_2_succeeded) TurnScreenOf(1);
        if(_data._lvl_3_succeeded) TurnScreenOf(2);
    }

    private void TurnScreenOf(int idLevel)
    {
        _listScreensPanel[idLevel].material = _screenOffMat;
        _listTutosPanel[idLevel].material = _screenOffMat;
        _listColliders[idLevel].enabled = false;
    }
}