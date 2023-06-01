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
    [SerializeField] private GameObject pistolwithAmmo1side;
    [SerializeField] private GameObject pistolwithAmmo2side;
    [SerializeField] private GameObject flashLight;
    [SerializeField] private GameObject obstacle;
    //[SerializeField] private GameObject obstacleButton;
   // [SerializeField] private GameObject ammo1side;
    [SerializeField] private GameObject chest;
    //[SerializeField] private GameObject battery2side;
    [SerializeField] private GameManager _gameManager;
    
    
    
    [SerializeField] public Vector3 spawnPistol;
    [SerializeField] public Vector3 spawnClonePistol;
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
        if (levelLoad.levelactuelle.SpawnPistol1side == true)
        {
            Instantiate(pistolwithAmmo1side, spawnClonePistol,quaternion.identity);
          
        }

        else if (levelLoad.levelactuelle.SpawnPistol2side == true)
        {
            Instantiate(pistolwithAmmo2side, spawnPistol,quaternion.identity);
            Instantiate(pistolwithAmmo1side, spawnClonePistol,quaternion.identity);
        }
        else if (levelLoad.levelactuelle.SpawnObstacle == true)
        {
            Instantiate(obstacle, spawnobstacle,quaternion.identity);
        }
        else if (levelLoad.levelactuelle.SpawnObstacle == true)
        {
            Instantiate(pistolwithAmmo1side, spawnClonePistol,quaternion.identity);
        }
        
        
    }

    
    void Update()
    {
        
    }
    public void ChangeLvl ()
    {
       levelLoad.levelactuelle = levelLoad.tableauLevel[1];
       SceneManager.LoadScene("Lvl_1_test");
    }
}
