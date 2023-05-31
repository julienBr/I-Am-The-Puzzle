using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Lvl1Manager : MonoBehaviour
{
    [SerializeField] private AppData levelLoad;
    [SerializeField] private GameObject pistolwithAmmo;
    [SerializeField] private GameObject pistolwithoutAmmo;
    [SerializeField] private GameObject flashLight;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject obstacleButton;
    [SerializeField] private GameObject ammo;
    [SerializeField] private GameObject chest;
    [SerializeField] private GameObject poster;
    [SerializeField] private GameObject battery;


    public Vector3 spawnpistol;
    void Start()
    {
        if (levelLoad.levelactuelle.SpawnPistolWithAmmo == true)
        {
            Instantiate(pistolwithAmmo, spawnpistol,quaternion.identity);
        }
    }

    
    void Update()
    {
        
    }



    public void FinishLvl1()
    {
        SceneManager.LoadScene("Lvl_1_test");
        levelLoad.levelactuelle = levelLoad.tableauLevel[0];
    }
}
