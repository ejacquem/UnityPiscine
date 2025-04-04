using UnityEngine;

public class StickyFeet : MonoBehaviour
{
    private Transform player;
    private Transform ground;

    private Vector3 old_pos;

    void Start()
    {
        player = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{player.name} is on top of: {other.name}");
        ground = other.transform;
        if(ground == null)
            Debug.LogError("groudn NULL wtf");
        old_pos = ground.position;
    }

    private void OnTriggerExit(Collider other)
    {
        if(ground)
            Debug.Log($"{player.name} left top of: {ground.name}");
        ground = null;
    }

    void Update()
    {
        if(ground){
            player.position += ground.position - old_pos;
            old_pos = ground.position;
        }
    }

    // void FixedUpdate()
    // {
    //     if(ground){
    //         old_pos = ground.position;
    //         needUpdate = true;
    //     }
    // }

    // void LateUpdate()
    // {
    //     if(ground && needUpdate){
    //         player.position += ground.position - old_pos;
    //         needUpdate = false;
    //     }
    // }
}
