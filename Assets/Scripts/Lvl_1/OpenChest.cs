using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    [SerializeField] private Animator chestAnimator;
    [SerializeField] private Animator chestAnimatorMirror;
    [SerializeField] private TextMeshProUGUI codeText; 
    public String codeTexteValue ;
   public string SafeCode;
   [SerializeField] private AudioSource chestOpenSound; 

    
    void Update() 
    {
        codeText.text = codeTexteValue;

        if (codeTexteValue == SafeCode)
        { 
            chestAnimator.SetTrigger("Open");
            chestAnimatorMirror.SetTrigger("Open");

            chestOpenSound.Play();
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
