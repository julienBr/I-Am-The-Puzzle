using UnityEngine;

public class ButtonDoors : MonoBehaviour
{
    public delegate void PushButtonEvent(int buttonId);
    public static event PushButtonEvent OpenDoors;

    public void PushButton(int buttonId)
    {
        OpenDoors?.Invoke(buttonId);
    }
}