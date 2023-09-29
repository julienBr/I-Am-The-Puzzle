using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportObjects : MonoBehaviour
{
    [SerializeField] private GameObject _zonetoTeleport;
    [SerializeField] private bool _objectOn = false;
    private GameObject _objectToTeleport;
   
    
    
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }


    public void TeleportObject()
    {
        if (_objectOn == true )
        {
            _objectToTeleport.transform.position = _zonetoTeleport.transform.position;
          //  _objectToTeleport.GetComponent<ObjectFollowMiror>().enabled = true;
        }
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("ObjectToTeleport"))
        {
            _objectOn = true;
            _objectToTeleport = collision.gameObject.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ObjectToTeleport"))
        {
            _objectOn = false;                      
            _objectToTeleport = null;
        }
    }
    
    
}