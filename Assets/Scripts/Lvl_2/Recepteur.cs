using System.Collections.Generic;
using UnityEngine;

public class Recepteur : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private List<GameObject> _lookTrepied;
    [SerializeField] private List<GameObject> _trepied;
    private List<LineRenderer> _laser = new ();

    public delegate void LinkToRecepteur(int id, bool isConnected);
    public static event LinkToRecepteur ConnectDoor;
    
    private void Awake()
    {
        foreach (GameObject lookTrepied in _lookTrepied)
            _laser.Add(lookTrepied.GetComponent<LineRenderer>());
    }

    public void HitLaser()
    {
        for(int i = 0; i < _trepied.Count; i++)
        {
            if (_trepied[i].GetComponent<Trepied>()._isConnectedSource1 || _trepied[i].GetComponent<Trepied>()._isConnectedTrepiedSource1)
            {
                _lookTrepied[i].transform.LookAt(_trepied[i].transform);
                if (Physics.Raycast(_lookTrepied[i].transform.position, _lookTrepied[i].transform.forward,
                        out RaycastHit hit))
                {
                    if (hit.collider.gameObject.CompareTag("Target"))
                    {
                        _laser[i].enabled = true;
                        _laser[i].SetPosition(0, _trepied[i].transform.position);
                        _laser[i].SetPosition(1, _laser[i].transform.position);
                        Debug.Log(i);
                        ConnectDoor?.Invoke(i, true);
                    }
                    else
                    {
                        _laser[i].enabled = false;
                        ConnectDoor?.Invoke(i, false);
                    }
                }
            }
            else if (_trepied[i].GetComponent<Trepied>()._isConnectedSource2 || _trepied[i].GetComponent<Trepied>()._isConnectedTrepiedSource2)
            {
                _lookTrepied[i].transform.LookAt(_trepied[i].transform);
                if (Physics.Raycast(_lookTrepied[i].transform.position, _lookTrepied[i].transform.forward,
                        out RaycastHit hit))
                {
                    if (hit.collider.gameObject.CompareTag("Target"))
                    {
                        _laser[i].enabled = true;
                        _laser[i].SetPosition(0, _trepied[i].transform.position);
                        _laser[i].SetPosition(1, _laser[i].transform.position);
                        ConnectDoor?.Invoke(i, true);
                    }
                    else
                    {
                        _laser[i].enabled = false;
                        ConnectDoor?.Invoke(i, false);
                    }
                }
            }
        }
    }
}