using UnityEngine;

public class PrismeAnimation : MonoBehaviour
{
    public Color redColor;
    public Color blueColor;
    public float RotationSpeed = 2f;
    public bool _laserActivated;

    private void OnEnable()
    {
        UITripods.ChangeSphereColor += ChangeSource;
    }

    private void OnDisable()
    {
        UITripods.ChangeSphereColor -= ChangeSource;
    }
    
    /*private void FixedUpdate()
    {
        if (_laserActivated)
        {
            transform.Rotate(0f,RotationSpeed,0f);
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", redColor);
        }
    }*/

    private void ChangeSource(GameObject sphere, int id)
    {
        if (sphere == gameObject)
        {
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", id == 0 ? blueColor : redColor);
        }
    }
}