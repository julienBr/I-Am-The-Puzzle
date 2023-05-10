using System.Collections.Generic;
using UnityEngine;

public class Trepied : MonoBehaviour
{
    [SerializeField] private List<GameObject> _lookTrepied;
    [SerializeField] private List<Transform> _posTrepied;
    private List<LineRenderer> _laser = new ();
    private bool _isConnectedSource1;
    private bool _isConnectedSource2;
    
    private void OnEnable()
    {
        Source.ConnectTrepied += Connect;
    }

    private void OnDisable()
    {
        Source.ConnectTrepied -= Connect;
    }

    private void Connect(GameObject posTrepied, int id, bool isConnected)
    {
        if (posTrepied == gameObject)
        {
            if (id == 1) _isConnectedSource1 = isConnected;
            if (id == 2) _isConnectedSource2 = isConnected;
        }
    }

    private void Awake()
    {
        foreach (GameObject lookTrepied in _lookTrepied)
        {
            _laser.Add(lookTrepied.GetComponent<LineRenderer>());
        }
    }
    
    public void HitLaser()
    {
        for(int i = 0; i < _lookTrepied.Count; i++)
        {
            _lookTrepied[i].transform.LookAt(_posTrepied[i]);
            if (Physics.Raycast(_lookTrepied[i].transform.position, _lookTrepied[i].transform.forward, out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag("Target") && (_isConnectedSource1 || _isConnectedSource2))
                {
                    _laser[i].enabled = true;
                    _laser[i].SetPosition(0, _lookTrepied[i].transform.position);
                    _laser[i].SetPosition(1, _posTrepied[i].position);
                }
                else _laser[i].enabled = false;
            }
        }
    }
    
    public void ClearTrepied()
    {
        foreach (LineRenderer laser in _laser)
            laser.enabled = false;
    }
    
    /*[SerializeField] private List<GameObject> _trepied;
    [SerializeField] private List<Transform> _lookAtTrepied;
    [SerializeField] private List<Transform> _posTarget;
    [SerializeField] private List<LineRenderer> _laserRenderer;
    private bool _touchSource;

    public void LookAtTarget()
    {
        for (int i = 0; i < _lookAtTrepied.Count; i++)
            _lookAtTrepied[i].LookAt(_posTarget[i]);
        foreach (GameObject _trepied in _trepied)
        {
            for (int i = 0; i < _trepied.GetComponent<Trepied>()._lookAtTrepied.Count; i++)
                _trepied.GetComponent<Trepied>()._lookAtTrepied[i].LookAt(_trepied.GetComponent<Trepied>()._posTarget[i]);
            _trepied.GetComponent<Trepied>().HitLaser();
        }
    }*/

    /*public void ClearTrepied()
    {
        foreach (GameObject _trepied in _trepied)
            if (_trepied.GetComponent<Trepied>()._touchSource)
                GameObject.Find("LookAt" + gameObject.name).GetComponent<LineRenderer>().enabled = false;
        foreach (LineRenderer laser in _laserRenderer)
            laser.enabled = false;
    }*/
}