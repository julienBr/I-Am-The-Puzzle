using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ChangeTrialsScreen : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private List<VideoClip> _listTrials;

    private VideoPlayer _screen;
    private bool _timeIsRunning;
    private float _timer;
    private bool _checkCollider;

    private void Awake() { _screen = GetComponent<VideoPlayer>(); }

    private void ChangeScreen(bool toggle)
    {
        _screen.clip = toggle ? _listTrials[1] : _listTrials[0];
    }

    private void Update()
    {
        if (_timeIsRunning)
        {
            ChangeScreen(true);
            _timer += Time.deltaTime;
            switch (_checkCollider)
            {
                case false when _timer >= 4f && gameObject.name == "Screen1":
                    _gameManager.LoadLevel("2_Lvl_1");
                    _checkCollider = true;
                    break;
                case false when _timer >= 4f && gameObject.name == "Screen2":
                    _gameManager.LoadLevel("3_Lvl_2");
                    _checkCollider = true;
                    break;
                case false when _timer >= 4f && gameObject.name == "Screen3":
                    _gameManager.LoadLevel("4_Lvl_3");
                    _checkCollider = true;
                    break;
            }
        }

        if (!_timeIsRunning)
        {
            ChangeScreen(false);
            _timer = 0f;
            _checkCollider = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) 
            _timeIsRunning = true;
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            _timeIsRunning = false;
    }
}