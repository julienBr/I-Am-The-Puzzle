using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowMiror : MonoBehaviour
{
    public Transform _objectToFollow;
    public Transform _mirror;
    [SerializeField] private bool OnMirrorSide;



    private void Update()
    {
        if (OnMirrorSide == true)
        {
            Vector3 objectToFollowLocal = _mirror.InverseTransformPoint(_objectToFollow.position);
            transform.position =
                _mirror.TransformPoint(new Vector3(objectToFollowLocal.x, objectToFollowLocal.y, -objectToFollowLocal.z));
            transform.rotation = _objectToFollow.rotation;

        }
       
        

    }



    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "MirrorZone")
        {
            OnMirrorSide = true;

        }
    }
    
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "MirrorZone")
        {
            OnMirrorSide = false;

        }
    }

}

