using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool _redIsOpen = false;
    private bool _blueIsOpen = false;
    [SerializeField] private List<GameObject> _redDoorlist;
    [SerializeField] private List<GameObject> _blueDoorlist;

    void Start()
    {

    }

    public void ok()
    {
        Debug.Log("OK");
    }


    void Update()
    {

    }


    public void OpenRedDoor()
    {
        foreach (var VARIABLE in _redDoorlist)
        {
            if ( _redIsOpen == false)
            {
                _redIsOpen = true;
                _blueIsOpen = false;
               gameObject.GetComponent<Animator>().SetBool("OpenRed", true);
            }
            else if (_redIsOpen == true)
            {
                _redIsOpen = false;
                _blueIsOpen = true;
                gameObject.GetComponent<Animator>().SetBool("OpenRed", false);
            }

        }

    }

    public void OpenBlueDoor()
    {
        foreach (var VARIABLE in _blueDoorlist)
        {
            if (gameObject.tag == "BlueDoor" && _blueIsOpen == false)
            {
                _redIsOpen = false;
                _blueIsOpen = true;
                gameObject.GetComponent<Animator>().SetBool("OpenRed", true);
            }
            else if (gameObject.tag == "BlueDoor" && _blueIsOpen == true)
            {
                _redIsOpen = true;
                _blueIsOpen = false;
                gameObject.GetComponent<Animator>().SetBool("OpenRed", false);
            }

        }


    }




}
