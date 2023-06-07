using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : MonoBehaviour
{
    [Header("Sources References")]
    [SerializeField] private List<GameObject> _sources;
    [SerializeField] private List<GameObject> _lookAtSources;
    private int _idSource;
    private bool _sourceSelected;

    [Header("Tripods References")]
    [SerializeField] private List<GameObject> _tripods;
    [SerializeField] private List<GameObject> _lookAtTripods;

    [Header("Receptors References")]
    [SerializeField] private List<GameObject> _receptors;
    [SerializeField] private List<GameObject> _lookAtReceptors;
    
    [Header("Connected References")]
    public List<bool> _isSource;
    public bool _isConnectedSource;
    public bool _isConnectedTripod;
    public List<bool> _receptorTouched;
    public bool _isGrabbed;
    public bool _alreadyDone;
    public bool _checkTargets;
    public bool _checkReceptors;

    private List<LineRenderer> _laserSources = new();
    private List<LineRenderer> _laserTripods = new();
    private List<LineRenderer> _laserReceptors = new();
    
    public delegate void ReceptorEvent(int receptorId);
    public static event ReceptorEvent OpenDoor;
    public static event ReceptorEvent CloseDoor;
    
    private void Awake()
    {
        foreach (GameObject lookAtSources in _lookAtSources)
            _laserSources.Add(lookAtSources.GetComponent<LineRenderer>());
        foreach (GameObject lookAtTargets in _lookAtTripods)
            _laserTripods.Add(lookAtTargets.GetComponent<LineRenderer>());
        foreach (GameObject lookAtReceptors in _lookAtReceptors)
            _laserReceptors.Add(lookAtReceptors.GetComponent<LineRenderer>());
    }

    private void OnEnable()
    {
        UITripods.SelectSource += ChangeSource;
        Receptor.UpdateLaser += RecalculateLaser;
    }

    private void OnDisable()
    {
        UITripods.SelectSource -= ChangeSource;
        Receptor.UpdateLaser -= RecalculateLaser;
    }

    private void ChangeSource(GameObject tripod, int id)
    {
        if (tripod == gameObject)
        {
            _idSource = id;
            gameObject.GetComponent<Renderer>().material.color = id == 0 ? Color.blue : Color.red;
            _sourceSelected = true;
        }
    }
    
    private void RecalculateLaser()
    {
        StartCoroutine(CheckLaser());
    }

    private IEnumerator CheckLaser()
    {
        if (_isConnectedSource)
        {
            for (float t = 0f; t < 2f; t += Time.deltaTime)
            {
                if (Physics.Raycast(_lookAtSources[_idSource].transform.position,
                        _lookAtSources[_idSource].transform.forward,
                        out RaycastHit hit))
                {
                    if (hit.collider.gameObject.CompareTag("Door"))
                    {
                        EraseLaser();
                        break;
                    }
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && !_isGrabbed && !_alreadyDone)
        {
            _alreadyDone = true;
            if (_sourceSelected && !_isConnectedSource)
            {
                LocateSource();
            }
        }
    }

    public void SelectEntered()
    {
        _alreadyDone = false;
        _isGrabbed = true;
        _checkTargets = false;
        _checkReceptors = false;
        foreach (GameObject tripods in _tripods)
        {
            tripods.GetComponent<Transmitter>()._checkTargets = false;
            tripods.GetComponent<Transmitter>()._checkReceptors = false;
        }
        EraseLaser();
    }

    public void SelectExited()
    {
        _isGrabbed = false;
    }

    private void LocateSource()
    {
        _lookAtSources[_idSource].transform.LookAt(_sources[_idSource].transform);
        if (Physics.Raycast(_lookAtSources[_idSource].transform.position, _lookAtSources[_idSource].transform.forward,
                out RaycastHit hit))
        {
            if (hit.collider.gameObject.CompareTag($"Source{_idSource}"))
            {
                _isSource[_idSource] = true;
                StartCoroutine(ShootLaser(_laserSources[_idSource], _sources[_idSource], gameObject, _isSource));
                _isConnectedSource = true;
            }
            else
            {
                _isSource[_idSource] = false;
                foreach (GameObject tripods in _tripods)
                    if (tripods.GetComponent<Transmitter>()._isConnectedSource) 
                        tripods.GetComponent<Transmitter>().LocateTargets();
            }
        }
    }

    private void LocateTargets()
    {
        _checkTargets = true;
        for (int i = 0; i < _tripods.Count; i++)
        {
            if (_idSource == _tripods[i].GetComponent<Transmitter>()._idSource && _tripods[i].GetComponent<Transmitter>()._sourceSelected)
            {
                _lookAtTripods[i].transform.LookAt(_tripods[i].transform);
                if (Physics.Raycast(_lookAtTripods[i].transform.position, _lookAtTripods[i].transform.forward,
                        out RaycastHit hit))
                {
                    if (hit.collider.gameObject.CompareTag("Target") && !_tripods[i].GetComponent<Transmitter>()
                                                                         ._isSource[0]
                                                                     && !_tripods[i].GetComponent<Transmitter>()
                                                                         ._isSource[1])
                    {
                        _tripods[i].GetComponent<Transmitter>()._isSource[0] = _isSource[0];
                        _tripods[i].GetComponent<Transmitter>()._isSource[1] = _isSource[1];
                        _tripods[i].GetComponent<Transmitter>()._isConnectedTripod = true;
                        StartCoroutine(ShootLaser(_laserTripods[i], gameObject, _tripods[i], _isSource));
                    }
                }
            }
        }
    }

    private void LocateReceptors(List<bool> source)
    {
        _checkReceptors = true;
        if (source[0])
        {
            for (int i = 0; i < _receptors.Count - 2; i++)
            {
                _lookAtReceptors[i].transform.LookAt(_receptors[i].transform);
                if (Physics.Raycast(_lookAtReceptors[i].transform.position, _lookAtReceptors[i].transform.forward,
                        out RaycastHit hit))
                {
                    if (hit.collider.gameObject.CompareTag($"Receptor{i}") && !_receptorTouched[i] && !_receptors[i].GetComponent<Receptor>()._isAlreadyTouched)
                    {
                        _receptorTouched[i] = true;
                        StartCoroutine(ShootLaser(_laserReceptors[i], gameObject, _receptors[i], _isSource));
                        OpenDoor?.Invoke(i);
                    }
                }
            }
        }
        else if (source[1])
        {
            for (int i = 2; i < _receptors.Count; i++)
            {
                _lookAtReceptors[i].transform.LookAt(_receptors[i].transform);
                if (Physics.Raycast(_lookAtReceptors[i].transform.position, _lookAtReceptors[i].transform.forward,
                        out RaycastHit hit))
                {
                    if (hit.collider.gameObject.CompareTag($"Receptor{i}")/* && !_receptorTouched[i] && !_receptors[i].GetComponent<Receptor>()._isAlreadyTouched*/)
                    {
                        _receptorTouched[i] = true;
                        StartCoroutine(ShootLaser(_laserReceptors[i], gameObject, _receptors[i], _isSource));
                        OpenDoor?.Invoke(i);
                    }
                }
            }
        }
    }
    
    private IEnumerator ShootLaser(LineRenderer laser, GameObject start, GameObject end, List<bool> source)
    {
        Gradient gradient = new Gradient();
        float time = 0.5f;
        gradient.SetKeys(source[0]
            ? new[] {new GradientColorKey(Color.blue, 0f)}
            : new[] {new GradientColorKey(Color.red, 0f)}, new[] {new GradientAlphaKey(1f, 0f)});
        laser.enabled = true;
        laser.colorGradient = gradient;
        laser.SetPosition(0, start.transform.position);
        for (float t = 0f; t < time; t += Time.deltaTime)
        {
            laser.SetPosition(1, Vector3.Lerp(start.transform.position, end.transform.position, t / time));
            yield return null;
        }
        if (_isConnectedSource)
        {
            if (!_checkTargets) LocateTargets();
            if (!_checkReceptors) LocateReceptors(_isSource);
            yield return new WaitForSeconds(time);
            foreach (GameObject tripods in _tripods)
                if (tripods.GetComponent<Transmitter>()._isConnectedTripod)
                    if (!tripods.GetComponent<Transmitter>()._checkReceptors)
                        tripods.GetComponent<Transmitter>().LocateReceptors(tripods.GetComponent<Transmitter>()._isSource);
        }
    }

    private void EraseLaser()
    {
        foreach (LineRenderer laserSources in _laserSources) laserSources.enabled = false;
        foreach (LineRenderer laserTripods in _laserTripods) laserTripods.enabled = false;
        foreach (LineRenderer laserReceptors in _laserReceptors) laserReceptors.enabled = false;
        foreach (GameObject tripods in _tripods)
        {
            if (_isConnectedSource)
            {
                if (_isSource[0] && !tripods.GetComponent<Transmitter>()._isConnectedSource)
                {
                    tripods.GetComponent<Transmitter>()._isSource[0] = false;
                    tripods.GetComponent<Transmitter>()._isConnectedTripod = false;
                    if (tripods.GetComponent<Transmitter>()._receptorTouched[0])
                    {
                        tripods.GetComponent<Transmitter>()._receptorTouched[0] = false;
                        CloseDoor?.Invoke(0);
                    }
                    if (tripods.GetComponent<Transmitter>()._receptorTouched[1])
                    {
                        tripods.GetComponent<Transmitter>()._receptorTouched[1] = false;
                        CloseDoor?.Invoke(1);
                    }
                    foreach (LineRenderer laserReceptor in tripods.GetComponent<Transmitter>()._laserReceptors)
                        if (laserReceptor.name is "LookAtReceptor1" or "LookAtReceptor2")
                            laserReceptor.enabled = false;
                }
                if (_isSource[1] && !tripods.GetComponent<Transmitter>()._isConnectedSource)
                {
                    tripods.GetComponent<Transmitter>()._isSource[1] = false;
                    tripods.GetComponent<Transmitter>()._isConnectedTripod = false;
                    if (tripods.GetComponent<Transmitter>()._receptorTouched[2])
                    {
                        tripods.GetComponent<Transmitter>()._receptorTouched[2] = false;
                        CloseDoor?.Invoke(2);
                    }
                    if (tripods.GetComponent<Transmitter>()._receptorTouched[3])
                    {
                        tripods.GetComponent<Transmitter>()._receptorTouched[3] = false;
                        CloseDoor?.Invoke(3);
                    }
                    foreach (LineRenderer laserReceptor in tripods.GetComponent<Transmitter>()._laserReceptors)
                        if (laserReceptor.name is "LookAtReceptor3" or "LookAtReceptor4")
                            laserReceptor.enabled = false;
                }
            }
            foreach (LineRenderer laser in tripods.GetComponentsInChildren<LineRenderer>())
                if (laser.name == $"LookAt{gameObject.name}") laser.enabled = false;
        }
        _isConnectedSource = false;
        _isConnectedTripod = false;
        for (int i = 0; i < _isSource.Count; i++) _isSource[i] = false;
        for (int i = 0; i < _receptorTouched.Count; i++)
        {
            if (_receptorTouched[i])
            {
                CloseDoor?.Invoke(i);
                _receptorTouched[i] = false;
            }
        }
    }
}