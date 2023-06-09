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
    
    void Start()
    {
       
    }

    
   public void FlashLight()
   {
       
        
        if (CanOn == true && batteryIstranparent == false)
        {
            if (FlashOn == false)
            {
               
                _light.gameObject.GetComponent<Light>().enabled = true;
                FlashOn = true;
            }
            else if (FlashOn == true)
            {
               
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
   }
}
