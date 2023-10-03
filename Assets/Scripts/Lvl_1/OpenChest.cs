using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    [SerializeField] private Animator chestAnimator;
    [SerializeField] private TextMeshProUGUI codeText; 
    public string codeTexteValue ;
   public string SafeCode;
   [SerializeField] private AudioSource chestOpenSound; 

    
    void Update() 
    {
        codeText.text = codeTexteValue;

        if (codeTexteValue == SafeCode)
        {
            chestAnimator.SetTrigger("Open");
           
        }

        if (codeTexteValue.Length >= 7)
        {
            codeTexteValue = "";
        }
    }

   public void AddDigit(string digit)
    {
        codeTexteValue += digit;
    }

   public void Return()
    {
        codeTexteValue = "";
    }
}
