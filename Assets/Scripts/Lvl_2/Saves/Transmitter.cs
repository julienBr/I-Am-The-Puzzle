using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
    
    private List<LineRenderer> _laserSources = new();
    private List<LineRenderer> _laserTripods = new();
    private List<LineRenderer> _laserReceptors = new();

    [Header("Connected References")]
    public List<bool> _isSource;
    public bool _isAlreadyConnected;
    
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
        if (other.gameObject.CompareTag("Ground"))
        {
            LocateSources();
            if (_isSource[0] || _isSource[1]) LocateTargets();
        }
    }

    private void LocateSources()
    {
        for (int i = 0; i < _sources.Count; i++)
        {
            _lookAtSources[i].transform.LookAt(_sources[i].transform);
            if (Physics.Raycast(_lookAtSources[i].transform.position, _lookAtSources[i].transform.forward, out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag($"Source{i}"))
                {
                    _isSource[i] = true;
                    _isAlreadyConnected = true;
                    ShootLaser(_laserSources[i], _sources[i], gameObject, _isSource);
                }
                else
                {
                    _isSource[i] = false;
                    _laserSources[i].enabled = false;
                }
            }
        }
    }

    private void LocateTargets()
    {
        for (int i = 0; i < _tripods.Count; i++)
        {
            _lookAtTripods[i].transform.LookAt(_tripods[i].transform);
            if (Physics.Raycast(_lookAtTripods[i].transform.position, _lookAtTripods[i].transform.forward, out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag("Target") && _tripods[i].GetComponent<Transmitter>()._isAlreadyConnected == false)
                {
                    _isSource[i] = _tripods[i].GetComponent<Transmitter>()._isSource[i];
                    ShootLaser(_laserTripods[i], _tripods[i], gameObject, _isSource);
                }
                else
                {
                    _laserTripods[i].enabled = false;
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
            new[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 0.5f), new GradientAlphaKey(1f, 1f) });
        laser.enabled = true;
        laser.colorGradient = gradient;
        laser.SetPosition(0, start.transform.position);
        laser.SetPosition(1, end.transform.position);
    }
}