using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAnimation : MonoBehaviour
{
    [SerializeField] private GameObject plateButton;
    [SerializeField] private GameObject _PushButton;



   public IEnumerator delayDoorButton()
    {
        plateButton.GetComponent<Collider>().enabled = false;
       _PushButton.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(2.7f);
        plateButton.GetComponent<Collider>().enabled = true;
        _PushButton.GetComponent<Collider>().enabled = true;
      
    }


   public void DelayDoor()
   {
       StartCoroutine(delayDoorButton());
   }
}
