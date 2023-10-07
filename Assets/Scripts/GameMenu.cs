using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private List<GameObject> _buttons;
    [SerializeField] private List<TMP_Text> _textButtons;
    [SerializeField] private Material _newGlow;
    
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
        /*_buttons[idButton].GetComponent<Image>().color = Color.cyan;
        _textButtons[idButton].material = _newGlow;
        Debug.Log(idButton);*/
        if (idButton == 0)
        {
            _level.puzzle = 0;
            _level.levelactuelle = _level.tableauLevel[0];
            _gameManager.LoadLevel("2_Lvl_1");
        }
        else if(idButton == 1) _gameManager.LoadLevel("1_Hub");
        else _gameManager.LoadLevel("0_Menu");
    }
}