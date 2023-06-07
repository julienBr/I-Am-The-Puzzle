using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowMiror : MonoBehaviour
{
    public Transform _objectToFollow;
    public Transform _mirror;
    [SerializeField] private bool OnMirrorSide;
    public bool objectIsGrab = false;
    

    private void FixedUpdate()
    {
        if (OnMirrorSide == true)
        {
            Vector3 objectToFollowLocal = _mirror.InverseTransformPoint(_objectToFollow.position);
            transform.position =
                _mirror.TransformPoint(new Vector3(objectToFollowLocal.x, objectToFollowLocal.y, -objectToFollowLocal.z));

            if (objectIsGrab == true)
            {
               
                transform.rotation = Quaternion.Euler(-_objectToFollow.localEulerAngles.x,-_objectToFollow.localEulerAngles.y,-_objectToFollow.localEulerAngles.z);
            }
            else
            {
                transform.localRotation = _objectToFollow.localRotation;
            }
           
        }
    }

   public void Objectgrab()
    {
        
        Rigidbody rigidbody = _objectToFollow.GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;

        _objectToFollow.GetComponent<ObjectFollowMiror>().objectIsGrab = true;

    }

    public void ObjectNotGrab()
    {
       
        Rigidbody rigidbody = _objectToFollow.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        
        _objectToFollow.GetComponent<ObjectFollowMiror>().objectIsGrab = false;
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

