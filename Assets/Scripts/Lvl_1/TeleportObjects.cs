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
        }
        
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag =="ObjectToTeleport" )
        {
            _objectOn = true;
            _objectToTeleport = collider.gameObject.gameObject;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag =="ObjectToTeleport" )
        {
            _objectOn = false;
            _objectToTeleport = null;
        }
    }
}
