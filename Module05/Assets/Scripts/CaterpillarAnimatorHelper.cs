using UnityEngine;

public class CaterpillarAnimatorHelper : MonoBehaviour
{
    [SerializeField] private GameObject _cameraFade;

    public void Spawn()
    {
        transform.parent.GetComponent<PlayerController>().Spawn();
    }

    public void Fade()
    {
        Debug.Log("Set Fade");
        _cameraFade.GetComponent<Animator>().SetTrigger("Fade");
    }
}
