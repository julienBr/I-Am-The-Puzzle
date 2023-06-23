using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExitLight : MonoBehaviour
{
    [SerializeField] private Material _redLight;
    [SerializeField] private Material _greenLight;
    [SerializeField] private int _lampNumber = 0;
   
    
    private void OnEnable()
    {
       Lvl1Manager.OnLampColorChange += ChangeColor;
    }
    private void OnDisable()
    {
       Lvl1Manager.OnLampColorChange -= ChangeColor;
    }

    private void Start()
    {
        ChangeColor(_lampNumber);


    }

    private void ChangeColor(int lampNumber, bool unlock = false)
    {
        if (lampNumber >= _lampNumber)
        {
            if (unlock)
            {
                GetComponent<MeshRenderer>().material = _greenLight;
               
            }
            else
            {
                GetComponent<MeshRenderer>().material = _redLight;
            }
        }
    }
}