using System.Collections;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{ 
    private Animator _animator;
    private bool _isOpen;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Open()
    {
        if (_isOpen)
        {
            _isOpen = true;
            StartCoroutine(DoorOpen());
        }
    }

    private IEnumerator DoorOpen()
    {
        _animator.SetBool("OpenDoor", true);
        yield return new WaitForSeconds(4f);
        _animator.SetBool("OpenDoor", false);
        _isOpen = false;
    }
}