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
    
     [SerializeField] private Material emissivepurple;
     [SerializeField] private Material glassLight;
    
    void Start()
    {
       
    }

    
   public void FlashLight()
   {
       
        
        if (CanOn == true && batteryIstranparent == false)
        {
            if (FlashOn == false)
            {
                flashlightmiddle.GetComponent<MeshRenderer>().material = emissivepurple;
                _light.gameObject.GetComponent<Light>().enabled = true;
                FlashOn = true;
            }
            else if (FlashOn == true)
            {
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

       if (battery.name == "BatteryMirror")
       {
           CanOn = true;
           batteryIstranparent = false;
           emissiveLamp.GetComponent<MeshRenderer>().material = emissiveblue;

       }
       else if (battery.name == "BatteryTransparent")
       {
           CanOn = false;
           batteryIstranparent = true;
       }
       
   }
   
   public void SocketDesacctivated()
   {
       CanOn = false;
       _light.gameObject.GetComponent<Light>().enabled = false;
       emissiveLamp.GetComponent<MeshRenderer>().material = emissiveRed;
       flashlightmiddle.GetComponent<MeshRenderer>().material = glassLight;
   }
}
