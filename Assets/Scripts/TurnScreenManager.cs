using System.Collections.Generic;
using UnityEngine;

public class TurnScreenManager : MonoBehaviour
{
    [Header("Screen Off Material")]
    [SerializeField] private Material _screenOffMat;
    
    [Header("Screens Manager")]
    [SerializeField] private List<MeshRenderer> _listScreensPanel;
    [SerializeField] private List<MeshRenderer> _listTutosPanel;
    
    [Header("Colliders")]
    [SerializeField] private List<Collider> _listColliders;

    private void OnEnable()
    {
        GameManager.TurnScreen += TurnScreenOf;
    }

    private void OnDisable()
    {
        GameManager.TurnScreen -= TurnScreenOf;
    }

    private void TurnScreenOf(int idLevel)
    {
        _listScreensPanel[idLevel].material = _screenOffMat;
        _listTutosPanel[idLevel].material = _screenOffMat;
        _listColliders[idLevel].enabled = false;
    }
}