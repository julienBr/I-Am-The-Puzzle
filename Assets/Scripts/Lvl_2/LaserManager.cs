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
    public List<LineRenderer> _laser1;
    public List<LineRenderer> _laser2;
    public List<LineRenderer> _laser3;

    public void Trepied1ToTarget()
    {
        for(int i = 0; i < _trepied1.Count; i++)
        {
            _trepied1[i].LookAt(_posTarget1[i]);
            if (Physics.Raycast(_trepied1[i].position, _trepied1[i].forward, out RaycastHit rayHit1))
            {
                if (rayHit1.collider.gameObject.CompareTag("Source1") || rayHit1.collider.gameObject.CompareTag("Source2"))
                {
                    if (rayHit1.collider.gameObject.CompareTag("Target"))
                    {
                        //Debug.DrawRay(_trepied1[i].position, _trepied1[i].forward*20f, Color.green);
                        _laser1[i].enabled = true;
                        _laser1[i].SetPosition(0, _trepied1[i].position);
                        _laser1[i].SetPosition(1, _posTarget1[i].position);
                    }
                }
            }
        }
    }

    public void ClearTrepied1()
    {
        foreach (LineRenderer laser in _laser1)
            laser.enabled = false;
    }
    
    public void Trepied2ToTarget()
    {
        for (int i = 0; i < _trepied2.Count; i++)
        {
            _trepied2[i].LookAt(_posTarget2[i]);
            if(Physics.Raycast(_trepied2[i].position, _trepied2[i].forward, out RaycastHit rayHit2))
            {
                if (rayHit2.collider.gameObject.CompareTag("Target"))
                {
                    //Debug.DrawRay(_trepied2[i].position, _trepied2[i].forward*20f, Color.red);
                    _laser2[i].enabled = true;
                    _laser2[i].SetPosition(0, _trepied2[i].position);
                    _laser2[i].SetPosition(1, _posTarget2[i].position);
                }
                else
                {
                    _laser2[i].enabled = false;
                }
            }
        }
    }
    
    public void ClearTrepied2()
    {
        foreach (LineRenderer laser in _laser2)
            laser.enabled = false;
    }
    
    public void Trepied3ToTarget()
    {
        for (int i = 0; i < _trepied3.Count; i++)
        {
            _trepied3[i].LookAt(_posTarget3[i]);
            if(Physics.Raycast(_trepied3[i].position, _trepied3[i].forward, out RaycastHit rayHit3))
            {
                if (rayHit3.collider.gameObject.CompareTag("Target"))
                {
                    //Debug.DrawRay(_trepied3[i].position, _trepied3[i].forward * 20f, Color.blue);
                    _laser3[i].enabled = true;
                    _laser3[i].SetPosition(0, _trepied3[i].position);
                    _laser3[i].SetPosition(1, _posTarget3[i].position);
                }
                else
                {
                    _laser2[i].enabled = false;
                }
            }
        }
    }
    
    public void ClearTrepied3()
    {
        foreach (LineRenderer laser in _laser3)
            laser.enabled = false;
    }
}