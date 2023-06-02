using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Lvl1Manager : MonoBehaviour
{
   
    private int _enigmeNumberFinished = 0;
    private int _enigmeTotal = 3;
    [SerializeField] private AppData levelLoad;
    [SerializeField] private GameManager _gameManager;
    
    
    [SerializeField] private GameObject pistolwithAmmo;
    [SerializeField] private GameObject pistolwithAmmoMirror;
    [SerializeField] private GameObject pistolTransparent;
    [SerializeField] private GameObject pistolwithoutAmmo;
    [SerializeField] private GameObject pistolwithoutAmmoMirror;
    [SerializeField] private GameObject flashLight;
    [SerializeField] private GameObject flashLightMirror;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject obstaclemirror;
    [SerializeField] private GameObject obstacleButton;
    [SerializeField] private GameObject ammo;
    [SerializeField] private GameObject ammoTransparent;
    [SerializeField] private GameObject chest;
    [SerializeField] private GameObject chestMirror;
    [SerializeField] private GameObject battery;
    [SerializeField] private GameObject batteryMirror;
    [SerializeField] private GameObject batterytransparent;
    
    
    
    public delegate void enigmeResult(int number, bool unlock);

    public static event enigmeResult OnLampColorChange;
    
    
    private void OnEnable()
    {
      FinishEnigme.OnActionFinished += EnigmeFinished;
      
    }
    private void OnDisable()
    {
        FinishEnigme.OnActionFinished  -= EnigmeFinished;
        
    }
   public void EnigmeFinished()
    
    {  
       
        _enigmeNumberFinished++;
        OnLampColorChange?.Invoke(_enigmeNumberFinished, true);
      
        // vérifier si toutes les enigmes sont finies
        
        if (_enigmeNumberFinished >= _enigmeTotal)
        {
           _gameManager.PressAnyKey();

        }
        // Sinon on allume une lampe pour dire qu'on a fini une enigme
       
    }
    
    
    
    
    
    
    
    
    
    
    
    void Start()
    {
        if (levelLoad.levelactuelle.SpawnPistolWithAmmo == true)
        {
            pistolwithAmmo.SetActive(true);
        }
        else if (levelLoad.levelactuelle.SpawnMirrorPistolWithAmmo == true)
        {
            pistolwithAmmoMirror.SetActive(true);
        }
        else if (levelLoad.levelactuelle.SpawnTransparentPistol == true)
        {
           pistolTransparent.SetActive(true);
        }
        else if (levelLoad.levelactuelle.SpawnPistolWithoutAmmo == true)
        {
           pistolwithoutAmmo.SetActive(true); 
        }
        else if (levelLoad.levelactuelle.SpawnMirrorPistolWithoutAmmo == true)
        {
            pistolwithoutAmmoMirror.SetActive(true);
        }
        else if (levelLoad.levelactuelle.SpawnObstacle == true)
        {
            obstacle.SetActive(true);
        }
        else if (levelLoad.levelactuelle.SpawnMirrorObstacle == true)
        {
            obstaclemirror.SetActive(true);
        }
        else if (levelLoad.levelactuelle.SpawnFlashLight == true)
        {
            flashLight.SetActive(true);
        }
        else if (levelLoad.levelactuelle.SpawnMirrorFlashLight == true)
        {
            flashLightMirror.SetActive(true);
        }
        else if (levelLoad.levelactuelle.SpawnMirrorBattery == true)
        {
            batterytransparent.SetActive(true);
            battery.SetActive(true);
        }
        else if (levelLoad.levelactuelle.Spawn1sideAmmo == true)
        {
            ammo.SetActive(true);
            ammoTransparent.SetActive(true);
            
        }
        else if (levelLoad.levelactuelle.Spawn2SideChest== true)
        { 
            chest.SetActive(true);
            chestMirror.SetActive(true);
        }
        
        
        
    }

    
    void Update()
    {
        
    }
    public void ChangeLvl ()
    {
       levelLoad.levelactuelle = levelLoad.tableauLevel[+1];
       SceneManager.LoadScene("Lvl_1_test");
    }
}
