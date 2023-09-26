using System.Collections;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private GameObject _receptor;
    private Animator _animator;

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
        if (receptorId == _id) StartCoroutine(ThrowAnimationDoor(true));
    }

    private void Close(int receptorId)
    {
        if (receptorId == _id)
            if (_receptor.GetComponent<Receptor>()._isAlreadyTouched == 0)
            {
                StartCoroutine(ThrowAnimationDoor(false));
            }
    }
    
    private IEnumerator ThrowAnimationDoor(bool connected)
    {
        if(connected)
            yield return new WaitForSeconds(0.75f);
        _animator.SetBool("OpenDoor", connected);
    }
}