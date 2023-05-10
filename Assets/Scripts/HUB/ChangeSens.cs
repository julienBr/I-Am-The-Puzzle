using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChangeSens : MonoBehaviour
{
    [SerializeField] private VolumeProfile _postprocessing;
    private ColorAdjustments _colorAdjustments;
    

    private void Awake()
    {
        if (_postprocessing.TryGet(out ColorAdjustments cA))
        {
            _colorAdjustments = cA;
            _colorAdjustments.saturation.overrideState = true;
            _colorAdjustments.saturation.value = -70f;
        }
    }

    public void ChangeVu()
    {
        if (_postprocessing.TryGet(out ColorAdjustments cA))
        {
            _colorAdjustments = cA;
            _colorAdjustments.saturation.overrideState = true;
            _colorAdjustments.saturation.value = 0f;
        }
    }

    public void ChangeOuie()
    {
        
    }
}