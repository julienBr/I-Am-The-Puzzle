using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSound : MonoBehaviour
{
    [SerializeField] private AudioSource _ballsound1;
    [SerializeField] private AudioSource _ballsound2;
    [SerializeField] private AudioSource _ballsound3;
    [SerializeField] private AudioSource _ballsound4;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude <= 1 )
        {
            _ballsound4.Play();
            
        }

        else if (collision.relativeVelocity.magnitude <= 2 )
        {
            _ballsound3.Play();
        }
        else if (collision.relativeVelocity.magnitude <= 3 )
        {
            _ballsound2.Play();
        }
        else if (collision.relativeVelocity.magnitude <= 15 )
        {
            _ballsound1.Play();
        }
        

    }
}
