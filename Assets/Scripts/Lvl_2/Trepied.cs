using System.Collections.Generic;
using UnityEngine;

public class Trepied : MonoBehaviour
{
    [SerializeField] private List<GameObject> _lookTrepied;
    [SerializeField] private List<GameObject> _trepied;
    [SerializeField] private List<GameObject> _source;
    private List<LineRenderer> _laser = new ();
    private bool _isConnectedSource1;
    private bool _isConnectedSource2;
    private bool _isGrabbed;
    
    private void OnEnable() { Source.ConnectTrepied += Connect; }
    private void OnDisable() { Source.ConnectTrepied -= Connect; }

    private void Connect(GameObject posTrepied, int id, bool isConnected)
    {
        if (posTrepied == gameObject)
        {
            if (id == 0) _isConnectedSource1 = isConnected;
            if (id == 1) _isConnectedSource2 = isConnected;
        }
    }

    private void Awake()
    {
        foreach (GameObject lookTrepied in _lookTrepied)
            _laser.Add(lookTrepied.GetComponent<LineRenderer>());
    }
    
    public void HitLaser()
    {
        if (_isConnectedSource1 || _isConnectedSource2)
        {
            for (int i = 0; i < _lookTrepied.Count; i++)
            {
                _lookTrepied[i].transform.LookAt(_trepied[i].transform);
                if (Physics.Raycast(_lookTrepied[i].transform.position, _lookTrepied[i].transform.forward, out RaycastHit hit))
                {
                    if (hit.collider.gameObject.CompareTag("Target"))
                    {
                        _laser[i].enabled = true;
                        _laser[i].SetPosition(0, _lookTrepied[i].transform.position);
                        _laser[i].SetPosition(1, _trepied[i].transform.position);
                    }
                    else _laser[i].enabled = false;
                }
            }
        }
    }

    public void RecalculateLaser()
    {
        foreach (GameObject trepied in _trepied)
            trepied.GetComponent<Trepied>().HitLaser();
    }
    
    public void ClearLaser()
    {
        _isGrabbed = true;
        if (_isConnectedSource1)
            GameObject.Find("Source1/LookAt" + gameObject.name).GetComponent<LineRenderer>().enabled = false;
        if (_isConnectedSource2)
            GameObject.Find("Source2/LookAt" + gameObject.name).GetComponent<LineRenderer>().enabled = false;
        foreach (LineRenderer laser in _laser)
            laser.enabled = false;
        foreach (GameObject trepied in _lookTrepied)
            GameObject.Find("LookAt" + gameObject.name).GetComponent<LineRenderer>().enabled = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && _isGrabbed)
        {
            foreach (GameObject source in _source)
                source.GetComponent<Source>().HitLaser();
            HitLaser();
            RecalculateLaser();
            _isGrabbed = false;
        }
    }
}