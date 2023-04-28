using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    public List<Transform> _trepied1;
    public List<Transform> _trepied2;
    public List<Transform> _trepied3;
    public List<Transform> _posTarget1;
    public List<Transform> _posTarget2;
    public List<Transform> _posTarget3;
    public List<bool> hit1;
    public List<bool> hit2;
    public List<bool> hit3;

    private void Update()
    {
        foreach (Transform transform in _trepied1)
            Debug.DrawRay(transform.position, transform.forward * 20f, Color.green);
        foreach (Transform transform in _trepied2)
            Debug.DrawRay(transform.position, transform.forward * 20f, Color.red);
        foreach (Transform transform in _trepied3)
            Debug.DrawRay(transform.position, transform.forward * 20f, Color.blue);
        RotateToTarget();
        CheckHitTarget();
    }

    private void RotateToTarget()
    {
        for(int i=0; i<_trepied1.Count; i++)
        {
            _trepied1[i].LookAt(_posTarget1[i]);
            _trepied2[i].LookAt(_posTarget2[i]);
            _trepied3[i].LookAt(_posTarget3[i]);
        }
    }

    private void CheckHitTarget()
    {
        for (int i=0; i<_trepied1.Count; i++)
        {
            if(Physics.Raycast(_trepied1[i].position, _trepied1[i].forward, out RaycastHit rayHit1))
                if (rayHit1.collider.gameObject.CompareTag("targetLaser")) 
                    hit1[i] = true;
                else hit1[i] = false;
            if(Physics.Raycast(_trepied2[i].position, _trepied2[i].forward, out RaycastHit rayHit2))
                if (rayHit2.collider.gameObject.CompareTag("targetLaser")) 
                    hit2[i] = true;
                else hit2[i] = false;
            if(Physics.Raycast(_trepied3[i].position, _trepied3[i].forward, out RaycastHit rayHit3))
                if (rayHit3.collider.gameObject.CompareTag("targetLaser")) 
                    hit3[i] = true;
                else hit3[i] = false;
        }
    }
}