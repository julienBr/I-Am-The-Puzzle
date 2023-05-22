using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMiror : MonoBehaviour
{
    public Transform _objectToFollow;
    public Transform _mirror;
    private bool _raycastHitObstacle = false;
   
    
    void Start()
    {
        
    }
    
    public void FollowPlayer()
    {
        if (_raycastHitObstacle == false) // Vérification raycast object
        {
           
            Vector3 objectToFollowLocal = _mirror.InverseTransformPoint(_objectToFollow.position);
            transform.position = _mirror.TransformPoint(new Vector3(objectToFollowLocal.x, objectToFollowLocal.y, -objectToFollowLocal.z));
           
        }
        else 
        {
            
        }
        


        
    }

    private void Update()
    {
        
        transform.rotation = _objectToFollow.rotation;
        //Vector3 objectToFollowLocal = _mirror.InverseTransformPoint(_objectToFollow.position);
        //transform.position = _mirror.TransformPoint(new Vector3(objectToFollowLocal.x, objectToFollowLocal.y, -objectToFollowLocal.z));

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.yellow);
            Debug.Log("Did Hit" + hit.collider.gameObject.name);


            if (hit.collider.gameObject.tag == "obstacle")
            {
                Debug.Log("LETSGOOOOO");
            }
        }

    }





}

