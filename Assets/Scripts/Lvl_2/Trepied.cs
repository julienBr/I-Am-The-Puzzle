using System.Collections.Generic;
using UnityEngine;

public class Trepied : MonoBehaviour
{
    [SerializeField] private List<GameObject> _lookTrepied;
    [SerializeField] private List<GameObject> _lookRecepteur;
    [SerializeField] private List<GameObject> _trepied;
    [SerializeField] private List<GameObject> _recepteur;
    [SerializeField] private List<GameObject> _source;
    private List<LineRenderer> _laser = new();
    private List<LineRenderer> _laserToRecepteur = new();
    public bool _isConnectedSource1;
    public bool _isConnectedSource2;
    public bool _isConnectedSource1Trepied;
    public bool _isConnectedSource2Trepied;
    private bool _isGrabbed;
    private bool _posDepart = true;

    private void Awake()
    {
        foreach (GameObject lookTrepied in _lookTrepied)
            _laser.Add(lookTrepied.GetComponent<LineRenderer>());
        foreach (GameObject lookRecepteur in _lookRecepteur)
            _laserToRecepteur.Add(lookRecepteur.GetComponent<LineRenderer>());
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
                        if (_isConnectedSource1) _trepied[i].GetComponent<Trepied>()._isConnectedSource1Trepied = true;
                        else _trepied[i].GetComponent<Trepied>()._isConnectedSource2Trepied = true;
                        _laser[i].enabled = true;
                        _laser[i].SetPosition(0, _lookTrepied[i].transform.position);
                        _laser[i].SetPosition(1, _trepied[i].transform.position);
                    }
                    else
                    {
                        _laser[i].enabled = false;
                        _trepied[i].GetComponent<Trepied>()._isConnectedSource1Trepied = false;
                        _trepied[i].GetComponent<Trepied>()._isConnectedSource2Trepied = false;
                    }
                }
            }
        }
    }

    private void HitRecepteur()
    {
        if (_isConnectedSource1 || _isConnectedSource1Trepied)
        {
            for (int i = 0; i < _lookRecepteur.Count; i++)
            {
                _lookRecepteur[i].transform.LookAt(_recepteur[i].transform);
                if (Physics.Raycast(_lookRecepteur[i].transform.position, _lookRecepteur[i].transform.forward, out RaycastHit hit))
                {
                    if (hit.collider.gameObject.CompareTag("Recepteur1"))
                    {
                        _laserToRecepteur[i].enabled = true;
                        Gradient gradient = new Gradient();
                        gradient.SetKeys(new[] { new GradientColorKey(Color.blue, 0f) },
                            new[] { new GradientAlphaKey(1f, 0f) });
                        _laserToRecepteur[i].colorGradient = gradient;
                        _laserToRecepteur[i].SetPosition(0, _lookRecepteur[i].transform.position);
                        _laserToRecepteur[i].SetPosition(1, _recepteur[i].transform.position);
                    }
                }
            }
        }
        if (_isConnectedSource2 || _isConnectedSource2Trepied)
        {
            for (int i = 0; i < _lookRecepteur.Count; i++)
            {
                _lookRecepteur[i].transform.LookAt(_recepteur[i].transform);
                if (Physics.Raycast(_lookRecepteur[i].transform.position, _lookRecepteur[i].transform.forward, out RaycastHit hit))
                {
                    if (hit.collider.gameObject.CompareTag("Recepteur2"))
                    {
                        _laserToRecepteur[i].enabled = true;
                        Gradient gradient = new Gradient();
                        gradient.SetKeys(new[] {new GradientColorKey(Color.red, 0f)}, new[] {new GradientAlphaKey(1f, 0f)});
                        _laserToRecepteur[i].colorGradient = gradient;
                        _laserToRecepteur[i].SetPosition(0, _lookRecepteur[i].transform.position);
                        _laserToRecepteur[i].SetPosition(1, _recepteur[i].transform.position);
                    }
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
            if (trepied.GetComponent<Trepied>()._isConnectedSource1Trepied || trepied.GetComponent<Trepied>()._isConnectedSource2Trepied)
                trepied.GetComponent<Trepied>().HitRecepteur();
        }
    }
    
    public void ClearLaser()
    {
        if (!_posDepart)
        {
            foreach (GameObject source in _source)
                foreach (LineRenderer laser in source.GetComponentsInChildren<LineRenderer>())
                    if (laser.name == "LookAt" + gameObject.name) laser.enabled = false;
            foreach (LineRenderer laser in _laser)
            {
                laser.enabled = false;
                _isConnectedSource1Trepied = false;
                _isConnectedSource2Trepied = false;
            }
            foreach (LineRenderer laserRecepteur in _laserToRecepteur) laserRecepteur.enabled = false;
            foreach (GameObject trepied in _trepied)
            {
                foreach (LineRenderer laser in trepied.GetComponentsInChildren<LineRenderer>())
                    if (laser.name == "LookAt" + gameObject.name) laser.enabled = false;
                if (trepied.GetComponent<Trepied>()._isConnectedSource1)
                    foreach (LineRenderer laserRecepteur in trepied.GetComponent<Trepied>()._laserToRecepteur)
                        if (laserRecepteur.CompareTag("Recepteur1")) laserRecepteur.enabled = false;
                if (trepied.GetComponent<Trepied>()._isConnectedSource2)
                    foreach (LineRenderer laserRecepteur in trepied.GetComponent<Trepied>()._laserToRecepteur)
                        if (laserRecepteur.CompareTag("Recepteur2")) laserRecepteur.enabled = false;
            }
        }
    }

    public void SelectEntered()
    {
        _isGrabbed = true;
        _isConnectedSource1 = false;
        _isConnectedSource2 = false;
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
            foreach (GameObject source in _source) source.GetComponent<Source>().HitLaser();
            HitLaser();
            HitRecepteur();
            RecalculateLaser();
        }
    }
}