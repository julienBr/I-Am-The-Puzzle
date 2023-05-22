using System.Collections.Generic;
using UnityEngine;

public class Trepied : MonoBehaviour
{
    [SerializeField] private List<GameObject> _lookTrepied;
    [SerializeField] private List<GameObject> _trepied;
    [SerializeField] private List<GameObject> _recepteur1;
    [SerializeField] private List<GameObject> _recepteur2;
    [SerializeField] private List<GameObject> _source;
    private List<LineRenderer> _laser = new();
    public bool _isConnectedSource1;
    public bool _isConnectedSource2;
    public bool _isConnectedTrepiedSource1;
    public bool _isConnectedTrepiedSource2;
    private bool _isGrabbed;
    private bool _posDepart = true;

    private void Awake()
    {
        foreach (GameObject lookTrepied in _lookTrepied)
            _laser.Add(lookTrepied.GetComponent<LineRenderer>());
    }
    
    private void OnEnable() { Source.ConnectTrepied += Connect; }
    private void OnDisable() { Source.ConnectTrepied -= Connect; }

    private void Connect(GameObject trepied, int id, bool isConnected)
    {
        if (isConnected)
        {
            if (trepied == gameObject)
            {
                if (id == 0)
                {
                    _isConnectedSource1 = isConnected;
                    foreach (LineRenderer laser in _laser)
                    {
                        Gradient gradient = new Gradient();
                        gradient.SetKeys(new[] {new GradientColorKey(Color.blue, 0f)}, new[] {new GradientAlphaKey(1f, 0f)});
                        laser.colorGradient = gradient;
                    }
                }
                else
                {
                    _isConnectedSource2 = isConnected;
                    foreach (LineRenderer laser in _laser)
                    {
                        Gradient gradient = new Gradient();
                        gradient.SetKeys(new[] {new GradientColorKey(Color.red, 0f)}, new[] {new GradientAlphaKey(1f, 0f)});
                        laser.colorGradient = gradient;
                    }
                }
            }
        }
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
                        if (_isConnectedSource1) _trepied[i].GetComponent<Trepied>()._isConnectedTrepiedSource1 = true;
                        else _trepied[i].GetComponent<Trepied>()._isConnectedTrepiedSource2 = true;
                        _laser[i].enabled = true;
                        _laser[i].SetPosition(0, _lookTrepied[i].transform.position);
                        _laser[i].SetPosition(1, _trepied[i].transform.position);
                    }
                    else
                    {
                        _laser[i].enabled = false;
                        _trepied[i].GetComponent<Trepied>()._isConnectedTrepiedSource1 = false;
                        _trepied[i].GetComponent<Trepied>()._isConnectedTrepiedSource2 = false;
                    }
                }
            }
        }
    }

    private void RecalculateLaser()
    {
        foreach (GameObject trepied in _trepied)
        {
            //if (trepied.GetComponent<Trepied>()._isConnectedSource1 || trepied.GetComponent<Trepied>()._isConnectedSource2)
                trepied.GetComponent<Trepied>().HitLaser();
        }
        foreach (GameObject recepteur in _recepteur1) recepteur.GetComponent<Recepteur>().HitLaser();
        foreach (GameObject recepteur in _recepteur2) recepteur.GetComponent<Recepteur>().HitLaser();
    }
    
    public void ClearLaser()
    {
        if (!_posDepart)
        {
            foreach (LineRenderer laser in _laser) laser.enabled = false;
            
            foreach (GameObject source in _source)
                foreach (LineRenderer laser in source.GetComponentsInChildren<LineRenderer>())
                    if (laser.name == "LookAt" + gameObject.name) laser.enabled = false;
            
            foreach (GameObject recepteur in _recepteur1)
                foreach (LineRenderer laser in recepteur.GetComponentsInChildren<LineRenderer>())
                    if (laser.name == "LookAt" + gameObject.name) laser.enabled = false;
            
            foreach (GameObject recepteur in _recepteur2)
                foreach (LineRenderer laser in recepteur.GetComponentsInChildren<LineRenderer>())
                    if (laser.name == "LookAt" + gameObject.name) laser.enabled = false;
            
            foreach (GameObject trepied in _trepied)
            {
                foreach (LineRenderer laser in trepied.GetComponentsInChildren<LineRenderer>())
                    if (laser.name == "LookAt" + gameObject.name) laser.enabled = false;
                if (_isConnectedSource1)
                {
                    foreach (GameObject recepteur in _recepteur1)
                    {
                        foreach (LineRenderer laser in recepteur.GetComponentsInChildren<LineRenderer>())
                            if (laser.name != "LookAt" + gameObject.name)
                                laser.enabled = false;
                    }
                    _isConnectedSource1 = false;
                }
                if (_isConnectedSource2)
                {
                    foreach (GameObject recepteur in _recepteur2)
                    {
                        foreach (LineRenderer laser in recepteur.GetComponentsInChildren<LineRenderer>())
                            if (laser.name != "LookAt" + gameObject.name)
                                laser.enabled = false;
                    }
                    _isConnectedSource2 = false;
                }
            }
        }
    }

    public void SelectEntered()
    {
        _isGrabbed = true;
        _isConnectedTrepiedSource1 = false;
        _isConnectedTrepiedSource2 = false;
    }
    public void SelectExited()
    {
        _isGrabbed = false;
        _posDepart = false;
        foreach (GameObject trepied in _trepied) trepied.GetComponent<Trepied>()._posDepart = false;
    }
    
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && !_isGrabbed && !_posDepart)
        {
            foreach (GameObject source in _source)
                source.GetComponent<Source>().HitLaser();
            HitLaser();
            if (_isConnectedSource1 || _isConnectedTrepiedSource1)
                foreach (GameObject recepteur in _recepteur1)
                    recepteur.GetComponent<Recepteur>().HitLaser();
            if (_isConnectedSource2 || _isConnectedTrepiedSource2)
                foreach (GameObject recepteur in _recepteur2)
                    recepteur.GetComponent<Recepteur>().HitLaser();
            RecalculateLaser();
        }
    }
}