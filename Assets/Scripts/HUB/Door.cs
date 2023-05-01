using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;
    private bool _isOpen ;
    [SerializeField] private AudioSource _closeDoor;
    [SerializeField] private AudioSource _openDoor;
 
     private void Start()
     {
         _animator = GetComponent<Animator>();
     }
 
     public void Interact()
     {
         if (!_isOpen)
         {
             _isOpen = true;
             _animator.SetBool("OpenDoor", true);
             _openDoor.Play();
         }
         else
         {
             _isOpen = false;
             _animator.SetBool("OpenDoor", false);
             _closeDoor.Play();
         }
     }
 }
