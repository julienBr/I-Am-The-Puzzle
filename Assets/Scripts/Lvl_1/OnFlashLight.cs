using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFlashLight : MonoBehaviour
{
    private bool FlashOn = false;
    void Start()
    {
        
    }

    
   public void FlashLight()
    {
        if (FlashOn = false)
        {
            gameObject.gameObject.GetComponent<Light>().enabled = true;
            FlashOn = true;
        }
        else if (FlashOn = true)
        {
            gameObject.gameObject.GetComponent<Light>().enabled = false;
            FlashOn = false;
        }

    }
}
