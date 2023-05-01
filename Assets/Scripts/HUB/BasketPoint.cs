using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketPoint : MonoBehaviour
{
   [SerializeField] private GameObject _panier;
   [SerializeField] private AudioSource _panierPoint;
   public bool _raycastHit; 
   public bool paniercollider =false;

   private void Awake()
   {
       _raycastHit = false;
    }

   private void FixedUpdate()
   {
       RaycastHit hit;
       if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
       {
           Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.yellow);
           Debug.Log("Did Hit" + hit.collider.gameObject.name);
           _raycastHit = true;
       }
       else
       {
           Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.red);
           Debug.Log("Did not Hit");
           _raycastHit = false;
       }
   }

   private void Update()
   {
       if (paniercollider == true & _raycastHit == true)
       {
           _panier.GetComponent<ParticleSystem>().Play();
           _panierPoint.Play();
           paniercollider = false;
       }
      
   }


   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") )
        {
            paniercollider = true;
          

        }

        
    }
   
}
