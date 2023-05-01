using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportPanier : MonoBehaviour
{
   private Animator _animator;
   private bool _hoopIsOpen;
   [SerializeField] private AudioSource _mecanicSound;
   [SerializeField] private Collider _colliderPanier ;

   private void Start()
   {
      _animator = GetComponent<Animator>();
     _colliderPanier.enabled = false;
   }
   public void OpenHoop()
   {
      if (!_hoopIsOpen)
      {
        _hoopIsOpen = true;
         _animator.SetBool("Hoop", true);
         _mecanicSound.Play();
         _colliderPanier.enabled = true;
      }
      else
      {
         _hoopIsOpen = false;
         _animator.SetBool("Hoop", false);
         _mecanicSound.Play();
        _colliderPanier.enabled = false;
      }
   }  
   
}
