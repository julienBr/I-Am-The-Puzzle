using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportPanier : MonoBehaviour
{
   private Animator _animator;
   private bool _hoopIsOpen;
   [SerializeField] private AudioSource _mecanicSound;

   private void Start()
   {
      _animator = GetComponent<Animator>();
   }
   public void OpenHoop()
   {
      if (!_hoopIsOpen)
      {
        _hoopIsOpen = true;
         _animator.SetBool("Hoop", true);
         _mecanicSound.Play();
      }
      else
      {
         _hoopIsOpen = false;
         _animator.SetBool("Hoop", false);
         _mecanicSound.Play();
      }
   }  
   
}
