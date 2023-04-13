using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

public class ChangeSens : MonoBehaviour
{
   
   [SerializeField] private Volume _postprocessing;

   public void ChangeVu()
   {
       _postprocessing.GetComponent<ColorAdjustments>().saturation.value = 0;
   }
}
