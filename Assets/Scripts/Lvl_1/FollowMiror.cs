using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMiror : MonoBehaviour
{
    public Transform _objectToFollow;

    public Transform _mirror;
    
    

    private void Update()
    {
        Vector3 objectToFollowLocal = _mirror.InverseTransformPoint(_objectToFollow.position);

        transform.position =
            _mirror.TransformPoint(new Vector3(objectToFollowLocal.x, objectToFollowLocal.y, -objectToFollowLocal.z));

        transform.localRotation = _objectToFollow.localRotation;



    }

}







