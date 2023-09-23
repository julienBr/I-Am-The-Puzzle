using UnityEngine;
using UnityEngine.UI;

public class SetOptionFromUI : MonoBehaviour
{
    public Scrollbar volumeSlider;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
    }

    private static void SetGlobalVolume(float value)
    {
        AudioListener.volume = value;
    }
}