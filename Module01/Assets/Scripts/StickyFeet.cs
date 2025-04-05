using System.Linq.Expressions;
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

    private void OnTriggerStay(Collider other)
    {
        ground = other.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{player.name} is on top of: {other.name}");
        ground = other.transform;
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
            Vector3 diff = ground.position - old_pos;
            if (Mathf.Abs(diff.y) > 1)
                player.position += diff;
            else
                player.position += new Vector3(diff.x, 0, 0);
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
