using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class BoundsControl : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private SnapTurnProviderBase _xrOrigin; 
    [SerializeField] private ActionBasedControllerManager _leftHand;
    [SerializeField] private ActionBasedControllerManager _rightHand;
    [SerializeField] private LayerMask _collisionLayer;
    [SerializeField] private float _fadeSpeed = 5f;
    [SerializeField] private float _sphereCheckSize = .03f;

    private Material _cameraFadeMat;
    private bool _isCameraFadeOut;

    private void Awake() => _cameraFadeMat = GetComponent<Renderer>().material;

    private void FixedUpdate()
    {
        if (Physics.CheckSphere(transform.position, _sphereCheckSize, _collisionLayer, QueryTriggerInteraction.Ignore))
        {
            CameraFade(1f);
            _isCameraFadeOut = true;
            _xrOrigin.enabled = false;
            _leftHand.enabled = false;
            _rightHand.enabled = false;
        }
        else
        {
            if (!_isCameraFadeOut) return;
            CameraFade(0f);
            _xrOrigin.enabled = true;
            _leftHand.enabled = true;
            _rightHand.enabled = true;
        }
    }

    private void CameraFade(float targetAlpha)
    {
        float _fadeValue = Mathf.MoveTowards(_cameraFadeMat.GetFloat("_AlphaValue"), targetAlpha, Time.deltaTime * _fadeSpeed);
        _cameraFadeMat.SetFloat("_AlphaValue", _fadeValue);
        if (_fadeValue <= .01f) _isCameraFadeOut = false;
    }
}