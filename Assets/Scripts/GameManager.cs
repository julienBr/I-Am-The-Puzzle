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
    
    
    public void Level1()
    {
        StartCoroutine(WaitLevel1());
    }
    
    
    public void Level2()
    {
        StartCoroutine(WaitLevel2());
    }
    
    public void Level3()
    {
        StartCoroutine(WaitLevel3());
    }
    
    
    
    
    

    private IEnumerator ThrowGame()
    {
        yield return new WaitForSeconds(1f);
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("1_Hub");
    }
    public IEnumerator WaitLevel1()
    {
        yield return new WaitForSeconds(1f);
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("2_Lvl_1");
    }
    
    public IEnumerator WaitLevel2()
    {
        yield return new WaitForSeconds(1f);
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("3_Lvl_2");
    }
    
    public IEnumerator WaitLevel3()
    {
        yield return new WaitForSeconds(1f);
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("4_Lvl_3");
    }
}