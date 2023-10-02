using UnityEngine;

public class TeleportObjects : MonoBehaviour
{
    [SerializeField] private GameObject _zonetoTeleport;
    [SerializeField] private bool _objectOn;
    private GameObject _objectToTeleport;

    public void TeleportObject()
    {
        if (_objectOn)
        {
            _objectToTeleport.transform.position = _zonetoTeleport.transform.position;
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

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("ObjectToTeleport"))
        {
            _objectOn = true;
            _objectToTeleport = collision.gameObject.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ObjectToTeleport"))
        {
            _objectOn = false;                      
            _objectToTeleport = null;
        }
    }*/
}