using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DelayAnimation : MonoBehaviour
{
    [SerializeField] private GameObject cuveButton;
    



   public IEnumerator delayDoorButton()
   {
       cuveButton.GetComponent<Button>().interactable = false;
       yield return new WaitForSeconds(2.7f);
       cuveButton.GetComponent<Button>().interactable = true;
        
    }


   public void DelayDoor()
   {
       StartCoroutine(delayDoorButton());
   }
}
