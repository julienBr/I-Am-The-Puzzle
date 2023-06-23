using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrismeAnimation : MonoBehaviour
{
    public Color redColor;
    public Color blueColor;
    public Color originalColor;
    public float RotationSpeed = 2f;
    public bool _laserActivated = false;
    
    void Start()
    {
        //GetComponent<MeshRenderer>().material.SetColor("Color_0a8bc23ce2644fbf9e887b1975e13b04", customColor);
        
    }

    private void FixedUpdate()
    {
        
        if (_laserActivated == true)
        {
            transform.Rotate(0f,RotationSpeed,0f);
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", redColor);
            
        }

      

    }

    public void PrismeActivated()
    {
        _laserActivated = true;
    }
}
