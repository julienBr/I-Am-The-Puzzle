using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldOn : MonoBehaviour
{
    [SerializeField] private bool shieldOn = false;
    [SerializeField] private bool CanOn = false;
    
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private GameObject shieldPlasma;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OpenShield()
    {
       
        
        if (CanOn == true)
        {
            if (shieldOn == false)
            {
                buttonSound.Play();
               shieldPlasma.SetActive(true);
              
                shieldOn = true;
            }
            else if (shieldOn == true)
            {
               buttonSound.Play();
               shieldPlasma.SetActive(false);
               
               shieldOn= false;
            }
        }
        

    }
    
    
    
    
}
