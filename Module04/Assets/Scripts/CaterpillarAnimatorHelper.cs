using UnityEngine;

public class CaterpillarAnimatorHelper : MonoBehaviour
{
    public void Spawn()
    {
        transform.parent.GetComponent<PlayerController>().Spawn();
    }
}
