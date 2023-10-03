using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class OnFlashLight : MonoBehaviour
{
    [SerializeField] private bool FlashOn = false;
    [SerializeField] private bool CanOn = false;
    [SerializeField] private GameObject _light;
    [SerializeField] private bool batteryIstranparent = false;
    [SerializeField] private XRSocketInteractor socket;
    [SerializeField] private GameObject emissiveLamp;
    [SerializeField] private GameObject flashlightmiddle;
    [SerializeField] private Material emissiveRed;
    [SerializeField] private Material emissiveblue;
     private GameObject codeCoffre;
    //[SerializeField] private GameObject codeCoffremirror;  
    public ObjectFollowMiror objectFollowMirorscript;
    [SerializeField] private Material emissivepurple;
     [SerializeField] private Material glassLight;
     
     [SerializeField] private AudioSource buttonSound;
     
     
     private Ray _ray;
     [SerializeField] Transform raycastOrigin;
     [SerializeField] private bool showCode;

     private void Update()
     {

         if (FlashOn == true && batteryIstranparent == false)
         {
             _ray.origin = raycastOrigin.position;
             _ray.direction = raycastOrigin.forward;

             RaycastHit hit;

             if  (Physics.Raycast(_ray, out hit, 1f))
             {
                // Debug.DrawRay(_ray.origin,_ray.direction,Color.red);
                // Debug.Log(hit.collider.name);
                    
                 if ( hit.collider.gameObject.tag == "Code" )
                 {
                    //codeCoffre.GetComponent<SpriteRenderer>().enabled = true;
                   // codeCoffremirror.GetComponent<SpriteRenderer>().enabled = true;
                   // Debug.Log("CODE AFFICHE");
                   codeCoffre = hit.collider.gameObject;
                   codeCoffre.GetComponent<SpriteRenderer>().enabled = true; 
                 }

                 else
                 {
                     if (codeCoffre != null)
                     {
                         codeCoffre.GetComponent<SpriteRenderer>().enabled = false; 
                     }
                    
                 }
                 
                
             }

         }
         else
         {
             {
                 _ray.origin = raycastOrigin.position;
                 _ray.direction = raycastOrigin.forward;

                 RaycastHit hit;

                 if  (Physics.Raycast(_ray, out hit, 1f))
                 {
                    // Debug.DrawRay(_ray.origin,_ray.direction,Color.red);
                   //  Debug.Log(hit.collider.name);
                    
                     if ( hit.collider.gameObject.tag == "Code" )
                     {
                        // codeCoffre.GetComponent<SpriteRenderer>().enabled = true;
                         // codeCoffremirror.GetComponent<SpriteRenderer>().enabled = true;
                        // Debug.Log("CODE AFFICHE");
                         hit.collider.gameObject.GetComponent<SpriteRenderer>().enabled = false; 
                         
                     }
                
                 }
             }

            
             
         }
        
        
         
     }


     public void FlashLight()
   {
       
        
        if (CanOn == true && batteryIstranparent == false)
        {
            if (FlashOn == false)
            {
                buttonSound.Play();
                flashlightmiddle.GetComponent<MeshRenderer>().material = emissivepurple;
                _light.gameObject.GetComponent<Light>().enabled = true;
                FlashOn = true;
                
                
                
                
                
            }
            else if (FlashOn == true)
            {
                buttonSound.Play();
                flashlightmiddle.GetComponent<MeshRenderer>().material = glassLight;
               _light.gameObject.GetComponent<Light>().enabled = false;
                FlashOn = false;
            }
        }
        

    }
   

   public void SocketActivated()
   {
       GameObject battery = socket.selectTarget.gameObject;
       Debug.Log("le pile est la " + battery.name);
       //battery.GetComponent<Rigidbody>().mass = 0.01f;
       
       
      
      

       if (battery.name == "BatteryMirror")
       {
           CanOn = true;
           batteryIstranparent = false;
           emissiveLamp.GetComponent<MeshRenderer>().material = emissiveblue;

       }
       else if (battery.name == "BatteryTransparent")
       {
           objectFollowMirorscript.GetComponent<OnFlashLight>().CanOn = true;
           objectFollowMirorscript.GetComponent<OnFlashLight>().batteryIstranparent = false;
           objectFollowMirorscript.GetComponent<OnFlashLight>().emissiveLamp.GetComponent<MeshRenderer>().material = emissiveblue;
           
           CanOn = false;
           batteryIstranparent = true;
           
          
       }
       
   }
   
   public void SocketDesacctivated()
   {
      // GameObject battery = socket.selectTarget.gameObject;
       //Rigidbody rigidbody = battery.GetComponent<Rigidbody>();
     // battery.GetComponent<Rigidbody>().mass = 2f;
      
     
       CanOn = false;
       _light.gameObject.GetComponent<Light>().enabled = false;
       emissiveLamp.GetComponent<MeshRenderer>().material = emissiveRed;
       flashlightmiddle.GetComponent<MeshRenderer>().material = glassLight;
       
       
       if (objectFollowMirorscript != null)
       {
           objectFollowMirorscript.GetComponent<OnFlashLight>().emissiveLamp.GetComponent<MeshRenderer>().material = emissiveRed;
           objectFollowMirorscript.GetComponent<OnFlashLight>().flashlightmiddle.GetComponent<MeshRenderer>().material = glassLight;
           objectFollowMirorscript.GetComponent<OnFlashLight>().CanOn = false;
           objectFollowMirorscript.GetComponent<OnFlashLight>().batteryIstranparent = true;
       }
       
       
       
   }
}
