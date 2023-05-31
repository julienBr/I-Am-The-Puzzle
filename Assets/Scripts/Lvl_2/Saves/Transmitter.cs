using System.Collections.Generic;
using UnityEngine;

public class Transmitter : MonoBehaviour
{
    [Header("Sources References")]
    [SerializeField] private List<GameObject> _sources;
    [SerializeField] private List<GameObject> _lookAtSources;

    [Header("Tripods References")]
    [SerializeField] private List<GameObject> _tripods;
    [SerializeField] private List<GameObject> _lookAtTripods;

    [Header("Receptors References")]
    [SerializeField] private List<GameObject> _receptors;
    [SerializeField] private List<GameObject> _lookAtReceptors;

    [Header("Connected References")] public List<bool> _isSource;
    public bool _isConnected;
    public bool _posDépart = true;
    public bool _isGrabbed;
    public bool _alreadyDone;
    
    private List<LineRenderer> _laserSources = new();
    private List<LineRenderer> _laserTripods = new();
    private List<LineRenderer> _laserReceptors = new();
    
    private void Awake()
    {
        foreach (GameObject lookAtSources in _lookAtSources)
            _laserSources.Add(lookAtSources.GetComponent<LineRenderer>());
        foreach (GameObject lookAtTargets in _lookAtTripods)
            _laserTripods.Add(lookAtTargets.GetComponent<LineRenderer>());
        foreach (GameObject lookAtReceptors in _lookAtReceptors)
            _laserReceptors.Add(lookAtReceptors.GetComponent<LineRenderer>());
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && !_isGrabbed && !_posDépart && !_alreadyDone)
        {
            _alreadyDone = true;
            LocateSources();
            if (_isConnected) LocateTargets();
            foreach (GameObject tripods in _tripods)
            {
                if (/*!tripods.GetComponent<Transmitter>()._posDépart && */tripods.GetComponent<Transmitter>()._isConnected)
                {
                    tripods.GetComponent<Transmitter>().LocateTargets();
                }
            }
        }
    }

    public void SelectEntered()
    {
        _alreadyDone = false;
        _isGrabbed = true;
        EraseLaser();
    }

    public void SelectExited()
    {
        _posDépart = false;
        _isGrabbed = false;
    }

    private void LocateSources()
    {
        for (int i = 0; i < _sources.Count; i++)
        {
            _lookAtSources[i].transform.LookAt(_sources[i].transform);
            if (Physics.Raycast(_lookAtSources[i].transform.position, _lookAtSources[i].transform.forward,
                    out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag($"Source{i}"))
                {
                    _isSource[i] = true;
                    _isConnected = true;
                    ShootLaser(_laserSources[i], _sources[i], gameObject, _isSource);
                }
                else _isSource[i] = false;
            }
        }
    }

    private void LocateTargets()
    {
        for (int i = 0; i < _tripods.Count; i++)
        {
            _lookAtTripods[i].transform.LookAt(_tripods[i].transform);
            if (Physics.Raycast(_lookAtTripods[i].transform.position, _lookAtTripods[i].transform.forward,
                    out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag("Target") && !_tripods[i].GetComponent<Transmitter>()._isSource[0]
                                                                 && !_tripods[i].GetComponent<Transmitter>()._isSource[1])
                {
                    _tripods[i].GetComponent<Transmitter>()._isSource[0] = _isSource[0];
                    _tripods[i].GetComponent<Transmitter>()._isSource[1] = _isSource[1];
                    ShootLaser(_laserTripods[i], gameObject, _tripods[i], _isSource);
                }
            }
        }
    }

    private void ShootLaser(LineRenderer laser, GameObject start, GameObject end, List<bool> source)
    {
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            source[0]
                ? new[]
                {
                    new GradientColorKey(Color.blue, 0f), new GradientColorKey(Color.cyan, 0.5f),
                    new GradientColorKey(Color.blue, 1f)
                }
                : new[]
                {
                    new GradientColorKey(Color.red, 0f), new GradientColorKey(Color.yellow, 0.5f),
                    new GradientColorKey(Color.red, 1f)
                },
            new[]
                {
                    new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 0.5f), new GradientAlphaKey(1f, 1f)
                    
                });
        laser.enabled = true;
        laser.colorGradient = gradient;
        laser.SetPosition(0, start.transform.position);
        laser.SetPosition(1, end.transform.position);
    }

    private void EraseLaser()
    {
        foreach (LineRenderer laserSources in _laserSources) laserSources.enabled = false;
        foreach (LineRenderer laserTripods in _laserTripods) laserTripods.enabled = false;
        foreach (GameObject tripods in _tripods)
        {
            if (_isConnected)
            {
                if (_isSource[0] && !tripods.GetComponent<Transmitter>()._isConnected)
                    tripods.GetComponent<Transmitter>()._isSource[0] = false;
                if (_isSource[1] && !tripods.GetComponent<Transmitter>()._isConnected)
                    tripods.GetComponent<Transmitter>()._isSource[1] = false;
            }
            foreach (LineRenderer laser in tripods.GetComponentsInChildren<LineRenderer>())
            {
                if (laser.name == $"LookAt{gameObject.name}")
                {
                    laser.enabled = false;
                }
            }
        }
        _isConnected = false;
        for (int i = 0; i < _isSource.Count; i++) _isSource[i] = false;
    }
}