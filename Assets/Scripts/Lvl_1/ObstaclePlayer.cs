using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePlayer : MonoBehaviour
{
   
    private bool _obstacleOk;
    private bool _clonePlayerOk;
  //  [SerializeField] private GameObject playerclone;
    

    void Update()
    {
        
    }
    
  
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag =="Player")
        {
            other.gameObject.GetComponent<FollowMiror>().enabled = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag =="Player")
        {
            other.gameObject.GetComponent<FollowMiror>().enabled = true;

        }
    }
}
