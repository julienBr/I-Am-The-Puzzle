using UnityEngine;

public class FinalTP : MonoBehaviour
{
    [SerializeField] private GameObject _canvasButtonHub;
    private MeshRenderer _mesh;

    private void OnEnable() { GameManager.FinishGame += UpdateMaterial; }
    
    private void OnDisable() { GameManager.FinishGame -= UpdateMaterial; }

    private void Awake() { _mesh = GetComponent<MeshRenderer>(); }

    private void UpdateMaterial()
    {
        _canvasButtonHub.SetActive(false);
        _mesh.material.color = new Color32(0, 255, 0, 255);
        _mesh.material.EnableKeyword("_EMISSION");
        _mesh.material.SetColor("_EmissionColor", new Color(0f, 1f*60f, 0f, 1f));
        DynamicGI.UpdateEnvironment();
    }
}