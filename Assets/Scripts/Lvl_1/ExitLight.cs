using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLight : MonoBehaviour
{
    [SerializeField] private Color _redLight;
    [SerializeField] private Color _greenLight;
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
        if (lampNumber == _lampNumber)
        {
            if (unlock)
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
