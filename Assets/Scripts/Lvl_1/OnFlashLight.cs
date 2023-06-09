using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFlashLight : MonoBehaviour
{
    private bool FlashOn = false;
    [SerializeField] private GameObject light;
    void Start()
    {
        
    }

    
   public void FlashLight()
    {
        if (FlashOn == false)
        {
           light.gameObject.GetComponent<Light>().enabled = true;
            FlashOn = true;
        }
        else if (FlashOn == true)
        {
            light.gameObject.GetComponent<Light>().enabled = false;
            FlashOn = false;
        }

    }
}
