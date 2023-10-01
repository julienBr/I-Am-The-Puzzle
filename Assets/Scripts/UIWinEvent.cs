using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class UIWinEvent : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private SnapTurnProviderBase _xrOrigin; 
    [SerializeField] private ActionBasedControllerManager _leftHand;
    [SerializeField] private ActionBasedControllerManager _rightHand;
    
    [Header("Text")]
    [SerializeField] private TMP_Text _winMessage;
    
    private void OnEnable()
    {
        WinCondition.TriggerWinEvent += DisplayWinUI;
    }

    private void OnDisable()
    {
        WinCondition.TriggerWinEvent -= DisplayWinUI;
    }
    
    private void DisplayWinUI(int level)
    {
        _xrOrigin.enabled = false;
        _leftHand.enabled = false;
        _rightHand.enabled = false;
        _winMessage.text = $"Bravo ! Vous venez de finir le niveau {level}\nVous allez être téléporté dans le HUB";
        GetComponent<Animator>().SetTrigger("FadeUI");
    }
}