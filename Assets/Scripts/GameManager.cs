using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void PressAnyKey()
    {
        StartCoroutine(ThrowGame());
    }

    private IEnumerator ThrowGame()
    {
        yield return new WaitForSeconds(1f);
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("1_Hub");
    }
}