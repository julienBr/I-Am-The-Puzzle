using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Fader")]
    [SerializeField] private GameObject fade;
    
    [Header("Data")]
    [SerializeField] private AppData _data;
    
    private string _currentScene;
    
    public delegate void FinishGameEvent();
    public static event FinishGameEvent FinishGame;
    
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
        if (level == 2) _data._lvl_2_succeeded = true;
        if (level == 3) _data._lvl_3_succeeded = true;
        if (_data._lvl_1_succeeded && _data._lvl_2_succeeded && _data._lvl_3_succeeded) _data._finishGame = true;
        LoadLevel("1_Hub");
    }

    private void Awake()
    {
        _currentScene = gameObject.scene.name;
        if (_currentScene == "0_Menu")
        {
            _data._lvl_1_succeeded = false;
            _data._lvl_2_succeeded = false;
            _data._lvl_3_succeeded = false;
            _data._finishGame = false;
        }
        if (_currentScene == "5_Credits") StartCoroutine(ReturnToHUB());
    }

    private IEnumerator ReturnToHUB()
    {
        yield return new WaitForSeconds(75f);
        LoadLevel("0_Menu");
    }
    
    private void Update() { if(_data._finishGame) FinishGame?.Invoke(); }

    public void LoadLevel(string levelToLoad)
    {
        StartCoroutine(ThrowLoadScene(levelToLoad));
    }
    
    private IEnumerator ThrowLoadScene(string levelToLoad)
    {
        if ((_data._lvl_1_succeeded || _data._lvl_2_succeeded || _data._lvl_3_succeeded) && _currentScene != "1_Hub")
            yield return new WaitForSeconds(10f);
        else yield return new WaitForSeconds(1f);
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