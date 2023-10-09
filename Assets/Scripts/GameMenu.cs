using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GameMenu : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AppData _data;
    [SerializeField] private DataLvl1 _level;

    [Header("Menu")]
    [SerializeField] private GameObject _menu;
    
    [Header("Controllers")]
    [SerializeField] private XRNode inputSource;
    [SerializeField] private InputHelpers.Button inputButton;
    
    private float InputThreshold = 0.1f;
    private bool _isDisplay, _isPressed;
    
    private void Update()
    {
        InputDevices.GetDeviceAtXRNode(inputSource).IsPressed(inputButton, out bool isPressed, InputThreshold);
        if (isPressed && !_isPressed)
        {
            _isPressed = true;
            if (!_isDisplay) DisplayBackMenu();
            else HideBackMenu();
        }
        if (!isPressed && _isPressed) _isPressed = false;
    }

    private void DisplayBackMenu()
    {
        _menu.SetActive(true);
        _isDisplay = true;
    }

    private void HideBackMenu()
    {
        _menu.SetActive(false);
        _isDisplay = false;
    }

    public void onClick(int idButton)
    {
        if (idButton == 0)
        {
            if (gameObject.scene.name == "2_Lvl_1")
            {
                _level.puzzle = 0;
                _level.levelactuelle = _level.tableauLevel[0];
                _gameManager.LoadLevel("2_Lvl_1");
            }
            else if (gameObject.scene.name == "3_Lvl_2") _gameManager.LoadLevel("3_Lvl_2");
            else _gameManager.LoadLevel("4_Lvl_3");
            _menu.SetActive(false);
        }
        else if (idButton == 1)
        {
            _gameManager.LoadLevel("1_Hub");
            _menu.SetActive(false);
        }
        else
        {
            _gameManager.LoadLevel("0_Menu");
            _menu.SetActive(false);
        }
    }
}