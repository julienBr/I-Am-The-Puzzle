using UnityEngine;

public class DoorsLvl3 : MonoBehaviour
{
    [SerializeField] private int doorId;
    private Animator doorAnimator;

    private void Awake()
    {
        doorAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        ButtonDoors.OpenDoors += AnimateDoor;
    }

    private void OnDisable()
    {
        ButtonDoors.OpenDoors -= AnimateDoor;
    }

    private void AnimateDoor(int buttonId)
    {
        doorAnimator.SetBool("OpenDoor", buttonId == doorId);
    }
}