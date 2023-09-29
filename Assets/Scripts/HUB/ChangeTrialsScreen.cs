using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ChangeTrialsScreen : MonoBehaviour
{
    [SerializeField] private VideoClip _screenTrials2;
    [SerializeField] private VideoClip _screenTrials1;
    [SerializeField] private float _timer = 0f;
    public GameManager _gameManager;
    [SerializeField] private bool _timeIsRunning = false;
    
     public void ChangeScreen ()
     {
         GetComponent<VideoPlayer>().clip = _screenTrials2;
     }

     public void ReturnScreen()
     {
         GetComponent<VideoPlayer>().clip = _screenTrials1;
     }

    private void Update()
    {
        if (_timeIsRunning == true)
        {
            ChangeScreen();
            _timer += Time.deltaTime;
            if (_timer >= 4f && gameObject.name == "Screen1")
            {
                //_gameManager.Level1();
            }
            else if (_timer >= 4f && gameObject.name == "Screen2")
            {
                //_gameManager.Level2();
            }
            else if (_timer >= 4f && gameObject.name == "Screen3")
            {
                //_gameManager.Level3();
            }
        }

        if (_timeIsRunning == false)
        {
            ReturnScreen();
            _timer = 0f;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag =="Player")
        {
            _timeIsRunning = true;

        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag =="Player")
        {
            _timeIsRunning = false;

        }
    }
}
