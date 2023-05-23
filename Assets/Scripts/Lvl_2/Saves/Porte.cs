using UnityEngine;

public class Porte : MonoBehaviour
{
    [SerializeField] private int _id;
    private Animator _animator;

    private void Awake() { _animator = GetComponent<Animator>(); }

    private void OpenDoor(int id, bool isConnected)
    {
        if (id == _id)
        {
            _animator.SetBool("OpenDoor", isConnected);
        }
    }
}