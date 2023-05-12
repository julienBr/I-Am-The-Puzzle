using System.Collections.Generic;
using UnityEngine;

public class Trepied : MonoBehaviour
{
    public List<GameObject> _lookTrepied;
    public List<GameObject> _trepied;
    public List<GameObject> _source;
    public List<LineRenderer> _laser = new ();
    public bool _isConnectedSource1;
    public bool _isConnectedSource2;
    private bool _isGrabbed;
    private bool _posDepart = true;
    
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
    
    private void HitLaser()
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

    private void RecalculateLaser()
    {
        foreach (GameObject trepied in _trepied)
        {
            if (trepied.GetComponent<Trepied>()._isConnectedSource1 || trepied.GetComponent<Trepied>()._isConnectedSource2)
                trepied.GetComponent<Trepied>().HitLaser();
        }
    }
    
    public void ClearLaser()
    {
        if (!_posDepart)
        {
            GameObject.Find("Source1/LookAt" + gameObject.name).GetComponent<LineRenderer>().enabled = false;
            GameObject.Find("Source2/LookAt" + gameObject.name).GetComponent<LineRenderer>().enabled = false;
            foreach (LineRenderer laser in _laser)
                laser.enabled = false;
            foreach (GameObject trepied in _trepied)
            {
                //Debug.Log(trepied.name + " : " + GameObject.Find("LookAt" + gameObject.name));
                GameObject.Find("LookAt" + gameObject.name).GetComponent<LineRenderer>().enabled = false;
            }
        }
    }

    public void Entered()
    {
        _isGrabbed = true;
        _isConnectedSource1 = false;
        _isConnectedSource2 = false;
    }
    public void Exited()
    {
        _isGrabbed = false;
        _posDepart = false;
        foreach (GameObject trepied in _trepied)
            trepied.GetComponent<Trepied>()._posDepart = false;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && !_isGrabbed && !_posDepart)
        {
            foreach (GameObject source in _source)
                source.GetComponent<Source>().HitLaser();
            HitLaser();
            RecalculateLaser();
        }
    }
}