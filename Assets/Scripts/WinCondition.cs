using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField] private int _idLevel;
    private bool _alreadyChecked;
    
    public delegate void WinEvent(int level);
    public static event WinEvent TriggerWinEvent;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_alreadyChecked)
        {
            _alreadyChecked = true;
            TriggerWinEvent?.Invoke(_idLevel);
            Debug.Log($"Vous venez de réussir le niveau {gameObject.scene.name}, soit le niveau {_idLevel}.");
        }
    }
}