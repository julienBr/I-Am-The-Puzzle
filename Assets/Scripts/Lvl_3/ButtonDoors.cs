using UnityEngine;

public class ButtonDoors : MonoBehaviour
{
    [SerializeField] private AudioSource doorSoundEffect; 
    public delegate void PushButtonEvent(int buttonId);
    public static event PushButtonEvent OpenDoors;

    public void PushButton(int buttonId)
    {
        doorSoundEffect.Play();
        OpenDoors?.Invoke(buttonId);
    }
}