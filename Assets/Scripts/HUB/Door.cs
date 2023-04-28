using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;
    private bool _isOpen ;

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
        }
        else
        {
            _isOpen = false;
            _animator.SetBool("OpenDoor", false);
        }
    }
}
