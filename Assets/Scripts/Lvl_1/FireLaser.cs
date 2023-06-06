using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaser : MonoBehaviour
{
    private bool targetHit;
    public bool Enigmfinished;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void FiringBullet()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.yellow,2f);
            Debug.Log("Did Hit" + hit.collider.gameObject.name);
            targetHit = true;
            
            if (targetHit = true && hit.collider.gameObject.tag == "Player")
            {
                Enigmfinished = true;
            }
        }

        
    }
}
