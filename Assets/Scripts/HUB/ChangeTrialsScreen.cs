using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ChangeTrialsScreen : MonoBehaviour
{
    [SerializeField] private VideoClip _screenTrials2;
   
    
    
    
     public void ChangeScreen ()
     {
         GetComponent<VideoPlayer>().clip = _screenTrials2;
     }

   
}
