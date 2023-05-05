using System.Collections.Generic;
using UnityEngine;

public class Trepied : MonoBehaviour
{
    [SerializeField] private List<GameObject> _trepied;
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
    }
    
    public void HitLaser()
    {
        for(int i = 0; i < _lookAtTrepied.Count; i++)
        {
            if (Physics.Raycast(_lookAtTrepied[i].position, _lookAtTrepied[i].forward, out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag("Source1") || hit.collider.gameObject.CompareTag("Source2"))
                {
                    _laserRenderer[i].enabled = true;
                    _laserRenderer[i].SetPosition(0, _lookAtTrepied[i].position);
                    _laserRenderer[i].SetPosition(1, _posTarget[i].position);
                    _touchSource = true;
                }
                if (hit.collider.gameObject.CompareTag("Target") && _touchSource)
                {
                    _laserRenderer[i].enabled = true;
                    _laserRenderer[i].SetPosition(0, _lookAtTrepied[i].position);
                    _laserRenderer[i].SetPosition(1, _posTarget[i].position);
                }
            }
        }
    }

    public void ClearTrepied()
    {
        _touchSource = false;
        foreach (GameObject _trepied in _trepied)
            if (_trepied.GetComponent<Trepied>()._touchSource)
                GameObject.Find("LookAt" + gameObject.name).GetComponent<LineRenderer>().enabled = false;
        foreach (LineRenderer laser in _laserRenderer)
            laser.enabled = false;
    }
}