using UnityEngine;

public class UITripods : MonoBehaviour
{
    [SerializeField] private GameObject _tripod;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _transformUI;
    [SerializeField] private GameObject _uiObject;

    public delegate void UITripodsEvent(GameObject tripod, int id);
    public static event UITripodsEvent SelectSource;
    
    private void Update()
    {
        Vector3 targetPostition = new Vector3(_camera.position.x, _transformUI.position.y, _camera.position.z);
        _transformUI.LookAt(targetPostition);
    }

    public void DisplayUI()
    {
        _uiObject.SetActive(true);
    }

    public void HideUI()
    {
        _uiObject.SetActive(false);
    }
    
    public void BlueButton()
    {
        Debug.Log("Activate Blue Source");
        SelectSource?.Invoke(_tripod, 0);
        //_uiObject.SetActive(false);
    }

    public void RedButton()
    {
        Debug.Log("Activate Red Source");
        SelectSource?.Invoke(_tripod, 1);
        //_uiObject.SetActive(false);
    }
}