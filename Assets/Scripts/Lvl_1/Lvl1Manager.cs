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
    [SerializeField] private GameObject pistolwithAmmo;
    [SerializeField] private GameObject pistolwithoutAmmo;
    [SerializeField] private GameObject flashLight;
    [SerializeField] private GameObject obstacle;
    //[SerializeField] private GameObject obstacleButton;
   // [SerializeField] private GameObject ammo;
    [SerializeField] private GameObject chest;
    //[SerializeField] private GameObject battery;
    [SerializeField] private GameManager _gameManager;
    
    
    
    [SerializeField] public Vector3 spawnpistol;
    [SerializeField] public Vector3 spawnflashLight;
    [SerializeField] public Vector3 spawnobstacle;
    [SerializeField] public Vector3 spawn;
    
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
       
        
            
        
        {
            
        }
        if (levelLoad.levelactuelle.SpawnPistolWithAmmo == true)
        {
            Instantiate(pistolwithAmmo, spawnpistol,quaternion.identity);
        }
    }

    
    void Update()
    {
        
    }
    
}
