using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolowPlayer : MonoBehaviour
{
    
    [SerializeField] private GameObject _cameraTarget;
    [SerializeField] private GameObject _cameraHead;
    [SerializeField] private Transform _temp;
  
    private void Update()
    {
       RotateToTarget(_cameraTarget.transform.position);
       
       
    }
    
    
    
    protected void RotateToTarget(Vector3 TargetPos)
        {
            _temp.position = new Vector3(TargetPos.x, TargetPos.y, TargetPos.z);
            _cameraHead.transform.LookAt(_temp);
        }
    
    
}
