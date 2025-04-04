using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private Transform destination;

    private Vector3 tpPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tpPos = transform.parent.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        other.transform.position = destination.position + (other.transform.position.y - tpPos.y) * Vector3.up;
        Player player = other.transform.GetComponent<Player>();
        if (player)
            StartCoroutine(player.DisableFeet(0.2f));
    }

}
