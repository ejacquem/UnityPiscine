using System;
using System.Collections.Generic;
using UnityEngine;

public struct Door
{
    public Transform transform;
    public Color color;
    public Collider collider;
}

public class Button : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> doors;
    [SerializeField]
    private List<GameObject> platforms;

    [SerializeField]
    private Material defaultMat;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float buttonMinSize;
    [SerializeField]
    private float doorMinSize;

    private Material buttonMat;
    private bool buttonActivated;

    private List<Door> doorsList;

    void Start()
    {
        buttonMat = GetComponent<MeshRenderer>().material;
        doorsList = new List<Door>();
        foreach (var door in doors)
        {
            Door d;
            d.transform = door.transform; 
            d.color = door.GetComponentInChildren<MeshRenderer>().material.color;
            d.collider = door.GetComponentInChildren<Collider>();
            doorsList.Add(d);
        }
    }

    void Update()
    {
        Vector3 buttonScale = transform.parent.localScale;
        float delta = Time.deltaTime * speed;
        // change the button scale
        buttonScale.y = Mathf.Clamp(buttonScale.y + delta * (buttonActivated ? -1 : 1), buttonMinSize, 1f);
        transform.parent.localScale = buttonScale;

        // change the doors scale
        foreach (var door in doorsList)
        {
            Vector3 doorScale = door.transform.localScale;
            doorScale.y = Mathf.Clamp(doorScale.y + delta * (buttonActivated && buttonMat.color == door.color ? -1 : 1), doorMinSize, 1f);
            door.transform.localScale = doorScale;
            if (doorScale.y == doorMinSize)
                door.collider.enabled = false;
            else
                door.collider.enabled = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        buttonMat.color = collision.gameObject.GetComponent<MeshRenderer>().material.color;
        buttonActivated = true;

        UpdatePlatform(buttonMat.color, collision.gameObject.GetComponent<Player>().platformLayer);
    }

    void OnCollisionExit(Collision collision)
    {
        // buttonMat.color = defaultMat.color;
        buttonActivated = false;
    }

    private void UpdatePlatform(Color c, int layer)
    {
        foreach (var plat in platforms)
        {
            plat.GetComponent<MeshRenderer>().material.color = c;
            plat.layer = layer;
            Debug.Log($"Updated {plat.name} to layer {layer}");
        }
    }
}
