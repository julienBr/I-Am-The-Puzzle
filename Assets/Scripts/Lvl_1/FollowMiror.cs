using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMiror : MonoBehaviour
{
    public Transform _objectToFollow;
    public Transform _mirror;
    /* private bool _raycastHitObstacle = false;
     private bool _raycastHitObstacle2 = false;
     [SerializeField] private GameObject _obstacle1Zone;
     [SerializeField] private GameObject _obstacle2Zone;*/
    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private Vector3 finalPosition;
    
    void Start()
    {
        
    }
    
    public void FollowPlayer()
    {
      /*  if (_raycastHitObstacle == false && _raycastHitObstacle2 == false ) // Vérification raycast object
        {
           
            
        }
        else  if (_raycastHitObstacle == true)
        {
            transform.position = _obstacle1Zone.transform.position;
        }
        else if (_raycastHitObstacle2 == true)
        {
            transform.position = _obstacle2Zone.transform.position;
        }*/
        
      Vector3 objectToFollowLocal = _mirror.InverseTransformPoint(_objectToFollow.position);
      initialPosition = transform.position;
      transform.position = _mirror.TransformPoint(new Vector3(objectToFollowLocal.x, objectToFollowLocal.y, -objectToFollowLocal.z)) ;


        
    }

    private void Update()
    {
       
        transform.localRotation = _objectToFollow.localRotation;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))

        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.yellow);
        }
        


        /*RaycastHit hit;
         if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
         {
             Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.yellow);
            
 
 
             if (hit.collider.gameObject.name == "Obstacle1")
             {
                 _raycastHitObstacle = true;
                 Debug.Log("Did Hit" + hit.collider.gameObject.name);
             }
             else if (hit.collider.gameObject.name == "Obstacle2")
             {
                 _raycastHitObstacle2 = true;
                 Debug.Log("Did Hit" + hit.collider.gameObject.name);
             }
             else
             {
                 _raycastHitObstacle = false;
                 _raycastHitObstacle2 = false;
             }
         }*/


    }

    }







