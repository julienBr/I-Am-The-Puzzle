using System.Collections.Generic;
using UnityEngine;

public class Transmitters : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private List<GameObject> _targets;
    [SerializeField] private List<GameObject> _lookAtTargets;
    [SerializeField] private bool _isSource;
    [SerializeField] private bool _posDepart = true;
    private Rays _sourceRay;
    private bool _onGround;

    public void Update()
    {
        if (_isSource || (_onGround && _sourceRay != null))
            LocateTargets();
    }
    
    private void LocateTargets()
    {
        for (int i = 0; i < _targets.Count; i++)
        {
            _lookAtTargets[i].transform.LookAt(_targets[i].transform);
            if (Physics.Raycast(_lookAtTargets[i].transform.position, _lookAtTargets[i].transform.forward, out RaycastHit hit))// && _onGround && _sourceRay == null)
            {
                if (hit.collider.gameObject.CompareTag("Target"))
                {
                    DrawRay(gameObject, _targets[i], _lookAtTargets[i].GetComponent<RayRenderer>());
                    _targets[i].GetComponent<Transmitters>()._sourceRay = _sourceRay;
                }
                else _targets[i].GetComponent<Transmitters>()._sourceRay = null;
            }
        }
    }

    private void DrawRay(GameObject transmitter, GameObject receptor, RayRenderer ray)
    {
        ray.GetComponent<LineRenderer>().SetPosition(0, transmitter.transform.position);
        ray.GetComponent<LineRenderer>().SetPosition(1, receptor.transform.position);
    }

    public void SelectEntered()
    {
        _onGround = false;
        _isSource = false;
    }

    public void SelectExited()
    {
        _onGround = true;
        _posDepart = false;
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && _onGround && !_posDepart)
        {
            Debug.Log("onGround");
            LocateTargets();
        }
    }
}