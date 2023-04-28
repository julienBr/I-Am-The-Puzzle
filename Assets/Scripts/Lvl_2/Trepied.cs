using System.Collections.Generic;
using UnityEngine;

public class Trepied : MonoBehaviour
{
    public Transform _posTrepied;
    public List<Transform> _posTarget;
    public bool hit;
    private void Update()
    {
        hit = CheckTarget();
        Debug.DrawRay(_posTrepied.position, _posTrepied.forward*10f, Color.green);
    }

    private bool CheckTarget()
    {
        foreach (Transform target in _posTarget)
        {
            if (Physics.Raycast(_posTrepied.position, _posTrepied.forward, out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag("targetLaser")) return true;
            }
        }
        return false;
    }
}