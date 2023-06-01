using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishEnigme : MonoBehaviour
{
    public delegate void ActionController();
    public static event ActionController OnActionFinished;
  

    public void FinishingAction()
    {
        OnActionFinished?.Invoke();
    }
   
}
