using System.Collections;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private GameObject _receptor;
    private Animator _animator;
    
    public delegate void ReceptorDoor();
    public static event ReceptorDoor UpdateLaser;
    
    private void Awake() { _animator = GetComponent<Animator>(); }

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
        if (receptorId == _id)
        {
            _receptor.GetComponent<Receptor>()._isAlreadyTouched++;
            StartCoroutine(ThrowAnimationDoor(true));
        }
    }

    private void Close(int receptorId)
    {
        if (receptorId == _id)
        {
            _receptor.GetComponent<Receptor>()._isAlreadyTouched--;
            if (_receptor.GetComponent<Receptor>()._isAlreadyTouched == 0)
            {
                StartCoroutine(ThrowAnimationDoor(false));
                UpdateLaser?.Invoke();
            }
        }
    }
    
    private IEnumerator ThrowAnimationDoor(bool connected)
    {
        if(connected)
            yield return new WaitForSeconds(0.75f);
        _animator.SetBool("OpenDoor", connected);
    }
}