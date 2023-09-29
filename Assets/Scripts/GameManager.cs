using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Fader")]
    [SerializeField] private GameObject fade;
    
    [Header("Data")]
    [SerializeField] private AppData _data;

    [Header("Player")]
    [SerializeField] private Transform _player;
    
    private string _currentScene;
    private string _previousScene;

    private void OnEnable()
    {
        WinCondition.TriggerWinEvent += SuccedTrial;
    }

    private void OnDisable()
    {
        WinCondition.TriggerWinEvent -= SuccedTrial;
    }

    private void SuccedTrial(int level)
    {
        if (level == 1) _data._lvl_1_succeeded = true;
        else if (level == 2) _data._lvl_2_succeeded = true;
        else _data._lvl_3succeeded = true;
        LoadLevel("1_Hub");
    }
    
    private void OnDestroy() { _previousScene = gameObject.scene.name; }

    private void Awake()
    {
        _currentScene = gameObject.scene.name;
        if (_currentScene == "0_Menu")
        {
            _data._lvl_1_succeeded = false;
            _data._lvl_2_succeeded = false;
            _data._lvl_3succeeded = false;
        }
    }

    public void LoadLevel(string levelToLoad)
    {
        StartCoroutine(ThrowLoadScene(levelToLoad));
    }
    
    private IEnumerator ThrowLoadScene(string levelToLoad)
    {
        yield return new WaitForSeconds(1f);
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        while (!loadOperation.isDone) yield return null;
    }
    
    public void Quit () 
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
		    Application.Quit();
        #endif
    }
}