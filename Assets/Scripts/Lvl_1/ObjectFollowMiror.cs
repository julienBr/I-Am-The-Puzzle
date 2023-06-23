using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowMiror : MonoBehaviour
{
    public Transform _objectToFollow; 
    public Transform _mirror;
   public bool OnMirrorSide;
   public bool objectIsGrab = false;
    

    private void Update()
    {
        if (OnMirrorSide == true)
        {
            Vector3 objectToFollowLocal = _mirror.InverseTransformPoint(_objectToFollow.transform.position);
            transform.position = _mirror.TransformPoint(new Vector3(objectToFollowLocal.x, objectToFollowLocal.y, -objectToFollowLocal.z));

            if (objectIsGrab == true )
            {

              transform.rotation = Quaternion.Euler(-_objectToFollow.transform.localEulerAngles.x,
                    -_objectToFollow.transform.localEulerAngles.y, _objectToFollow.transform.localEulerAngles.z);
             
            }
            else if(objectIsGrab == false)
            {
               
               //transform.rotation = _objectToFollow.rotation;
             //transform.rotation = Quaternion.Euler(_objectToFollow.rotation.x,-_objectToFollow.rotation.y,_objectToFollow.rotation.z);
             transform.localRotation = _objectToFollow.transform.localRotation;
            }
          
        }
    }

   public void Objectgrab()
    {
        if (_objectToFollow != null)
        {
            Rigidbody rigidbodymirror = _objectToFollow.GetComponent<Rigidbody>();
        
            rigidbodymirror.isKinematic = true;
            rigidbodymirror.useGravity = false;

            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
       
        
            _objectToFollow.GetComponent<ObjectFollowMiror>().objectIsGrab = true;

        }
      
    

    }

    public void ObjectNotGrab()
    {
        if (_objectToFollow != null)
        {
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        
              Rigidbody rigidbodymirror = _objectToFollow.GetComponent<Rigidbody>();
        
              rigidbodymirror.isKinematic = true;
              rigidbodymirror.useGravity = false;
        
        
            _objectToFollow.GetComponent<ObjectFollowMiror>().objectIsGrab = false;
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

