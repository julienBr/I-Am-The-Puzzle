using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject fade;

    public void Quit () 
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
		    Application.Quit();
        #endif
    }
}