using UnityEngine;

public class Receptor : MonoBehaviour
{
    [SerializeField] private int _id;
    public int _isAlreadyTouched;
    
    /*public delegate void ReceptorDoor();
    public static event ReceptorDoor UpdateLaser;
    
    private void OnEnable()
    {
        Transmitter.OpenDoor += Open;
        Transmitter.CloseDoor += Close;
    }

    private void OnDisable()
    {
        Transmitter.OpenDoor -= Open;
        Transmitter.CloseDoor -= Close;
    }

    private void Open(int receptorId)
    {
        if (receptorId == _id) _isAlreadyTouched ++;
    }

    private void Close(int receptorId)
    {
        if (receptorId == _id)
        {
            _isAlreadyTouched --;
            if (_isAlreadyTouched == 0) UpdateLaser?.Invoke();
        }
    }*/
}