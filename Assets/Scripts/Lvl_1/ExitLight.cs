using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLight : MonoBehaviour
{
    [SerializeField] private Color _redLight;
    [SerializeField] private Color _greenLight;
    [SerializeField] private int _lampNumber = 0;
   
   
    private void Start()
    {
        
    }


    private void ChangeColor()
    {
        
        {
           if
            {
                GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", _greenLight);
            }
            else
            {
                GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", _redLight);
            }
        }
    }
}
