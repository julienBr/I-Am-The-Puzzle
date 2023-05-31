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
    [SerializeField] private Vector3 direction;

    void Start()
    {

    }

    public void FollowPlayer()
    {

        /* Vector3 objectToFollowLocal = _mirror.InverseTransformPoint(_objectToFollow.position);
         initialPosition = transform.position;
         finalPosition = new Vector3(objectToFollowLocal.x, objectToFollowLocal.y, -objectToFollowLocal.z);
         transform.position = _mirror.TransformPoint(finalPosition);
         direction = finalPosition - initialPosition;
   
         RaycastHit hit;
         if (Physics.Raycast(initialPosition, direction, out hit, Mathf.Infinity))
   
         {
             Debug.DrawRay(initialPosition, direction*100f, Color.yellow,2f);
   
             if (hit.collider.gameObject.name == "Obstacle1")
             {
                 Debug.Log("OBSTACLE TOUCHERRRRR");
             }
         }*/


    }

    private void Update()
    {
        Vector3 objectToFollowLocal = _mirror.InverseTransformPoint(_objectToFollow.position);
        initialPosition = transform.position;
        finalPosition = new Vector3(objectToFollowLocal.x, objectToFollowLocal.y, -objectToFollowLocal.z);
        transform.position = _mirror.TransformPoint(finalPosition);
        direction = finalPosition - initialPosition;

        RaycastHit hit;
        if (Physics.Raycast(initialPosition, direction, out hit, Mathf.Infinity))

        {
            Debug.DrawRay(initialPosition, direction * 100f, Color.yellow);

            if (hit.collider.gameObject.name == "Obstacle1")
            {
                Debug.Log("OBSTACLE TOUCHERRRRR");
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

}







