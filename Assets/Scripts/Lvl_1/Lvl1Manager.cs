using System.Collections;
using UnityEngine;

public class Lvl1Manager : MonoBehaviour
{
    public delegate void EnigmeResult(int number, bool unlock);
    public static event EnigmeResult OnLampColorChange;
    
    private int _enigmeTotal = 3;
    [SerializeField] private DataLvl1 levelLoad;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject mirror;
    [SerializeField] private GameObject WinWarningUI;
    [SerializeField] private GameObject LooseWarningUI;
    [SerializeField] private GameObject finalwarningUI;
    [SerializeField] private Material _winMirrorShader;
    [SerializeField] private Material _looseMirrorShader;
    [SerializeField] private GameObject _cloneplayer;
    [SerializeField] private GameObject _cloneplayerFace;
    [SerializeField] private Animator dissolveAnimator; 
    
    
    public bool cloneIsDead;
    public bool playerIsDead;
    [SerializeField] private bool _win;
    [SerializeField] private bool _lost;

    [SerializeField] private Animator leftDoor;
    [SerializeField] private Animator rightDoor;
    [SerializeField] private GameObject switchDoor;
    [SerializeField] private Material switchDoorGreenLight;

    [SerializeField] private AudioSource doorSound;
    
    [Header("puzzle 1 Objects")]
    [SerializeField] private GameObject pistolwithAmmoMirrorLvl1;
    [SerializeField] private GameObject pistolTransparent;
    
    [Header("puzzle 2 Objects")]
    [SerializeField] private GameObject pistolwithAmmo;
    [SerializeField] private GameObject pistolwithAmmoMirrorLvl2;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject obstaclemirror;
  
    [Header("puzzle 3 Objects")]
    [SerializeField] private GameObject ammo;
    [SerializeField] private GameObject ammoTransparent;
    [SerializeField] private GameObject chest;
    [SerializeField] private GameObject chestMirror;
    [SerializeField] private GameObject batteryMirror;
    [SerializeField] private GameObject batterytransparent;
    [SerializeField] private GameObject pistolwithoutAmmo;
    [SerializeField] private GameObject pistolwithoutAmmoMirror;
    [SerializeField] private GameObject flashLight;
    [SerializeField] private GameObject flashLightMirror;

    void Start()
    {
        levelLoad.levelactuelle = levelLoad.tableauLevel[levelLoad.puzzle];
        OnLampColorChange?.Invoke(levelLoad.puzzle, true);
      
        pistolwithAmmo.SetActive(false);
        pistolwithAmmoMirrorLvl1.SetActive(false);
        pistolwithAmmoMirrorLvl2.SetActive(false);
        pistolTransparent.SetActive(false);
        pistolwithoutAmmo.SetActive(false);
        pistolwithoutAmmoMirror.SetActive(false);
        flashLight.SetActive(false);
        flashLightMirror.SetActive(false);
        obstacle.SetActive(false);
        obstaclemirror.SetActive(false); 
        ammo.SetActive(false);
        ammoTransparent.SetActive(false);
        chest.SetActive(false);
        chestMirror.SetActive(false);
        batteryMirror.SetActive(false);
        batterytransparent.SetActive(false);
        
        if (levelLoad.levelactuelle == levelLoad.tableauLevel[0]) 
        {
            pistolTransparent.SetActive(true);
            pistolwithAmmoMirrorLvl1.SetActive(true);
        }
        else if (levelLoad.levelactuelle == levelLoad.tableauLevel[1]) 
        {
            pistolwithAmmo.SetActive(true); 
            pistolwithAmmoMirrorLvl2.SetActive(true);
            obstacle.SetActive(true);
            obstaclemirror.SetActive(true); 
        }
        else if (levelLoad.levelactuelle == levelLoad.tableauLevel[2])
        {
            chest.SetActive(true); 
            chestMirror.SetActive(true);
            pistolwithoutAmmo.SetActive(true);
            pistolwithoutAmmoMirror.SetActive(true);
            flashLight.SetActive(true);
            flashLightMirror.SetActive(true);
            ammo.SetActive(true);
            ammoTransparent.SetActive(true);
            batterytransparent.SetActive(true);
            batteryMirror.SetActive(true);
        }
        
    }
    
    public void ChangeLvl ()
    {
        // levelLoad.puzzle++;
        if (levelLoad.puzzle < _enigmeTotal)
        {
            _gameManager.LoadLevel("2_Lvl_1");
        }
        else if (levelLoad.puzzle >= _enigmeTotal)
        {
            levelLoad.puzzle = 0;
            //_gameManager.PressAnyKey();
        }
    }

    private void EnigmeFinished()
    {
        levelLoad.puzzle++;
        OnLampColorChange?.Invoke(levelLoad.puzzle, true);
       
        
        if (levelLoad.puzzle < _enigmeTotal)
        {
            mirror.GetComponent<MeshRenderer>().material = _winMirrorShader;
           WinWarningUI.SetActive(true);
            
        }
        else if (levelLoad.puzzle >= _enigmeTotal)
        {
            mirror.GetComponent<MeshRenderer>().material = _winMirrorShader;
            finalwarningUI.SetActive(true);
            OpenDoor();
            switchDoor.GetComponent<MeshRenderer>().material = switchDoorGreenLight;
        }
    }

    public IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(0.1f);
        if (!_win && playerIsDead) //Lost
        {
            //gameover
            ////reload la scene
            
            mirror.GetComponent<MeshRenderer>().material = _looseMirrorShader;
            LooseWarningUI.SetActive(true);
            _lost = true;
            _gameManager.LoadLevel("2_Lvl_1");
            Debug.Log("lost");
            
        }
        if (!_lost && cloneIsDead && !playerIsDead) //Win
        {
            //WIN
            destroyClone();
            _win = true;
            EnigmeFinished();
            Debug.Log("WIN");
        }
    }

    void OpenDoor()
    {
        leftDoor.SetTrigger("OpenDoor");
        rightDoor.SetTrigger("OpenDoor");
        doorSound.Play();
    }

    void destroyClone()
    {
        StartCoroutine(delayDissolve());
    }
    
    IEnumerator delayDissolve()
    {
        _cloneplayerFace.SetActive(false);
        dissolveAnimator.SetTrigger("Dissolve");
        yield return new WaitForSeconds(2f);
        Destroy(_cloneplayer); 
    }

}