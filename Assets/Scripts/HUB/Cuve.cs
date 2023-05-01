using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuve : MonoBehaviour
{
    private Animator _animator;
    private bool _cuveisOpen ;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (!_cuveisOpen)
        {
            _cuveisOpen = true;
            _animator.SetBool("OpenCuve", true);
        }
        else
        {
            _cuveisOpen = false;
            _animator.SetBool("OpenCuve", false);
        }
    }
}