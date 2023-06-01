using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LevelsData
{
   
}

[CreateAssetMenu(menuName = "New level")] 
public class LevelData : ScriptableObject
{
  public bool SpawnPistol1side;
  public bool SpawnPistol2side;
  public bool SpawnFlashLight;
  public bool SpawnBattery;
  public bool SpawnChest; 
  public bool SpawnAmmo; 
  public bool SpawnObstacle; 
  public bool BoutonObstacle; 
}
