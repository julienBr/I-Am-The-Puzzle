using UnityEngine;

public class PrismeAnimation : MonoBehaviour
{
    public Color redColor;
    public Color blueColor;

    private void OnEnable()
    {
        UITripods.ChangeSphereColor += ChangeSource;
    }

    private void OnDisable()
    {
        UITripods.ChangeSphereColor -= ChangeSource;
    }

    private void ChangeSource(GameObject sphere, int id)
    {
        if (sphere == gameObject)
        {
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", id == 0 ? blueColor : redColor);
        }
    }
}