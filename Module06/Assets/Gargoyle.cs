using UnityEditor;
using UnityEngine;

public class Gargoyle : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private string _flashLightMatName;
    private Ghost[] _allGhost;
    private Material _flashLightMat;

    [SerializeField] private Color _flashLightIdle;
    [SerializeField] private Color _flashLightTrigger;

    void Start()
    {
        _allGhost = FindObjectsByType<Ghost>(FindObjectsSortMode.None);
        _flashLightMat = _model.GetComponent<SkinnedMeshRenderer>().materials[0];

        _flashLightMat.SetColor("_EmissionColor", _flashLightIdle);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _flashLightMat.color = _flashLightTrigger;
            _flashLightMat.SetColor("_EmissionColor", _flashLightTrigger);
            foreach (var ghost in _allGhost)
            {
                ghost.SetPlayerTarget();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        _flashLightMat.color = _flashLightIdle;
        _flashLightMat.SetColor("_EmissionColor", _flashLightIdle);
    }

}
