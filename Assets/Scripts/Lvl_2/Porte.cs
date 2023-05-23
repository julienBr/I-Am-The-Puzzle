using UnityEngine;

public class Porte : MonoBehaviour
{
    [SerializeField] private int _id;
    private Animator _animator;

    private void Awake() { _animator = GetComponent<Animator>(); }
    
    private void OnEnable() { Recepteur.ConnectDoor += Open; }
    private void OnDisable() { Recepteur.ConnectDoor -= Open; }

    private void Open(int id, bool isConnected)
    {
        if(id == _id) _animator.SetBool("OpenDoor", isConnected);
    }
}