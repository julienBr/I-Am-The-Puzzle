using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Lvl1Manager : MonoBehaviour
{
    public delegate void enigmeResult(int number, bool unlock);

    public static event enigmeResult OnLampColorChange;
    
    
    
    
    
    
    private int _enigmeTotal = 3;
    [SerializeField] private AppData levelLoad;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject mirror;
    [SerializeField] private GameObject warningUI;
    [SerializeField] private Material _warningMirrorShader;
    [SerializeField] private GameObject _cloneplayer;
    public bool cloneIsDead = false;
    public bool playerIsDead = false;
   
    
    [Header("puzzle 1 Objects")]
    [SerializeField] private GameObject pistolwithAmmoMirrorLvl1;
    [SerializeField] private GameObject pistolTransparent;
    
    [Header("puzzle 2 Objects")]
    [SerializeField] private GameObject pistolwithAmmo;
    [SerializeField] private GameObject pistolwithAmmoMirrorLvl2;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject obstaclemirror;
   // [SerializeField] private GameObject obstacleButton;
   // [SerializeField] private GameObject obstacleButtonMirrorTransparent;
    
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
      //obstacleButton.SetActive(false);
     //obstacleButtonMirrorTransparent.SetActive(false);
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
        //  obstacleButton.SetActive(true);
        // obstacleButtonMirrorTransparent.SetActive(true);
       
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
            
            SceneManager.LoadScene("Lvl_1_test");
        }
        else if (levelLoad.puzzle >= _enigmeTotal)
        {
            levelLoad.puzzle = 0;
            _gameManager.PressAnyKey();
            
        }
    }

    public void EnigmeFinished()
    {

     levelLoad.puzzle++;
     OnLampColorChange?.Invoke(levelLoad.puzzle, true);
     Destroy(_cloneplayer);
     
     if (levelLoad.puzzle < _enigmeTotal)
     {
      mirror.GetComponent<MeshRenderer>().material = _warningMirrorShader;
      warningUI.SetActive(true);
     }
     else if (levelLoad.puzzle >= _enigmeTotal)
     {
      mirror.GetComponent<MeshRenderer>().material = _warningMirrorShader;
      //OpenDoor();

     }
    }
    
    
    
 
}