using System;
using UnityEngine;

public class TeleportObjects : MonoBehaviour
{
    [SerializeField] private GameObject _zonetoTeleport;
    [SerializeField] private bool _objectOn;
    private GameObject _objectToTeleport;
    [SerializeField] private Material Redshader;
    [SerializeField] private Material Blueshader;
    [SerializeField] private AudioSource teleportsoundeffect;
    [SerializeField] private AudioSource Onteleportsoundeffect;
    private void Update()
    {
        if (_objectOn)
        {
            Onteleportsoundeffect.Play();
            gameObject.GetComponent<Renderer>().material = Blueshader;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = Redshader;
        }
    }


    public void TeleportObject()
    {
        if (_objectOn)
        {
            _objectToTeleport.transform.position = _zonetoTeleport.transform.position;
            teleportsoundeffect.Play();
          //  _objectToTeleport.GetComponent<ObjectFollowMiror>().enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("ObjectToTeleport"))
        {
            _objectOn = true;
            _objectToTeleport = other.gameObject.gameObject;
        }
        else _objectOn = false;
    }

    private void OnTriggerExit(Collider other)
    {
        _objectOn = false;
    }
}