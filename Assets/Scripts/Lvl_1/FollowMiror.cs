using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMiror : MonoBehaviour
{
    public Transform _objectToFollow;
    public Transform _mirror;
    private bool _obstacleOk;
    private bool _clonePlayerOk;
    public bool _raycastHit; 
    
    void Start()
    {
        
    }
    
    void Update()
    {
        Vector3 objectToFollowLocal = _mirror.InverseTransformPoint(_objectToFollow.position);
        transform.position = _mirror.TransformPoint(new Vector3(objectToFollowLocal.x, objectToFollowLocal.y, -objectToFollowLocal.z));

        transform.localRotation = _objectToFollow.localRotation;


        
    }
    
    private void Awake()
    {
        _raycastHit = false;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
               Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.yellow);
               Debug.Log("Did Hit" + hit.collider.gameObject.name);
            _raycastHit = true;
        }
        else
        {
              Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.red);
                Debug.Log("Did not Hit");
            _raycastHit = false;
        }
    }
    
    
    
}

