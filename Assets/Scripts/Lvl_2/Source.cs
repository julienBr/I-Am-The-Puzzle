using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private List<GameObject> _lookTrepied;
    [SerializeField] private List<GameObject> _posTrepied;
    private List<LineRenderer> _laser = new ();

    public delegate void LinkToSource(GameObject posTrepied, int id, bool isConnected);
    public static event LinkToSource ConnectTrepied;

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
            _lookTrepied[i].transform.LookAt(_posTrepied[i].transform);
            if (Physics.Raycast(_lookTrepied[i].transform.position, _lookTrepied[i].transform.forward, out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag("Target"))
                {
                    _laser[i].enabled = true;
                    _laser[i].SetPosition(0, _laser[i].transform.position);
                    _laser[i].SetPosition(1, _posTrepied[i].transform.position);
                    ConnectTrepied?.Invoke(_posTrepied[i], _id, true);
                }
                else
                {
                    _laser[i].enabled = false;
                    ConnectTrepied?.Invoke(_posTrepied[i], _id, false);
                }
            }
        }
    }
}