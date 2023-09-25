using UnityEngine;

public class UITripods : MonoBehaviour
{
    [SerializeField] private GameObject _tripod;
    [SerializeField] private GameObject _sphere;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _transformUI;
    [SerializeField] private GameObject _uiObject;

    public delegate void UITripodsEvent(GameObject tripod, int id);
    public static event UITripodsEvent SelectSource;
    public delegate void UIPrismeEvent(GameObject sphere, int id);
    public static event UIPrismeEvent ChangeSphereColor;
    
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
        SelectSource?.Invoke(_tripod, 0);
        ChangeSphereColor?.Invoke(_sphere, 0);
        _uiObject.SetActive(false);
    }

    public void RedButton()
    {
        SelectSource?.Invoke(_tripod, 1);
        ChangeSphereColor?.Invoke(_sphere, 1);
        _uiObject.SetActive(false);
    }
}