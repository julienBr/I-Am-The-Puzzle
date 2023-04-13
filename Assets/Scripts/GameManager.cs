using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private AppDatas choice;
    [SerializeField] private GameObject fade;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _difficultyMenu;

    public void NewGame()
    {
        _mainMenu.SetActive(false);
        _difficultyMenu.SetActive(true);
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