using UnityEngine;

public class Porte : MonoBehaviour
{
    private Animator _animator;

    private void Awake() { _animator = GetComponent<Animator>(); }

    private void OnEnable()
    {
        Trepied.ConnectRecepteur1 += Connect1;
        Trepied.ConnectRecepteur2 += Connect2;
    }
    private void OnDisable()
    {
        Trepied.ConnectRecepteur1 -= Connect1;
        Trepied.ConnectRecepteur2 -= Connect2;
    }

    private void Connect1(GameObject recepteur, int id, bool isTouched)
    {
        if (isTouched)
            Debug.Log($"{recepteur.name} - {id} - {isTouched}");
    }
    
    private void Connect2(GameObject recepteur, int id, bool isTouched)
    {
        
    }
}