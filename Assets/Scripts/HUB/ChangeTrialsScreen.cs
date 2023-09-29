using UnityEngine;
using UnityEngine.Video;

public class ChangeTrialsScreen : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private VideoClip _screenTrials1;
    [SerializeField] private VideoClip _screenTrials2;
    
    private bool _timeIsRunning;
    private float _timer;
    private bool _checkCollider;
    
     private void ChangeScreen ()
     {
         GetComponent<VideoPlayer>().clip = _screenTrials2;
     }

     private void ReturnScreen()
     {
         GetComponent<VideoPlayer>().clip = _screenTrials1;
     }

    private void Update()
    {
        if (_timeIsRunning)
        {
            ChangeScreen();
            _timer += Time.deltaTime;
            if (!_checkCollider && _timer >= 4f && gameObject.name == "Screen1")
            {
                _gameManager.LoadLevel("2_Lvl_1");
                _checkCollider = true;
            }
            else if (!_checkCollider && _timer >= 4f && gameObject.name == "Screen2")
            {
                _gameManager.LoadLevel("3_Lvl_2");
                _checkCollider = true;
            }
            else if (!_checkCollider && _timer >= 4f && gameObject.name == "Screen3")
            {
                _gameManager.LoadLevel("4_Lvl_3");
                _checkCollider = true;
            }
        }

        if (_timeIsRunning == false)
        {
            ReturnScreen();
            _timer = 0f;
            _checkCollider = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _timeIsRunning = true;

        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _timeIsRunning = false;

        }
    }
}