using UnityEngine;

public class BoundsControl : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private GameObject _leftHand;
    [SerializeField] private GameObject _rightHand;
    [SerializeField] private LayerMask _collisionLayer;
    [SerializeField] private float _fadeSpeed;
    [SerializeField] private float _sphereCheckSize = .15f;

    private Material _cameraFadeMat;
    private bool _isCameraFadeOut;

    private void Awake() => _cameraFadeMat = GetComponent<Renderer>().material;

    private void FixedUpdate()
    {
        if (Physics.CheckSphere(transform.position, _sphereCheckSize, _collisionLayer, QueryTriggerInteraction.Ignore))
        {
            CameraFade(1f);
            _isCameraFadeOut = true;
            _leftHand.SetActive(false);
            _rightHand.SetActive(false);
        }
        else
        {
            if (!_isCameraFadeOut) return;
            CameraFade(0f);
            _leftHand.SetActive(true);
            _rightHand.SetActive(true);
        }
    }

    private void CameraFade(float targetAlpha)
    {
        float _fadeValue = Mathf.MoveTowards(_cameraFadeMat.GetFloat("_AlphaValue"), targetAlpha, Time.deltaTime * _fadeSpeed);
        _cameraFadeMat.SetFloat("_AlphaValue", _fadeValue);
        if (_fadeValue <= .01f) _isCameraFadeOut = false;
    }
}