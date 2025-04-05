using System;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> doors;
    private List<Tuple<GameObject, Color>> doorsList;

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

    void Start()
    {
        buttonMat = GetComponent<MeshRenderer>().material;
        doorsList = new List<Tuple<GameObject, Color>>();
        foreach (var door in doors)
        {
            doorsList.Add(new Tuple<GameObject, Color>(door, door.GetComponentInChildren<MeshRenderer>().material.color));
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
            Vector3 doorScale = door.Item1.transform.localScale;
            doorScale.y = Mathf.Clamp(doorScale.y + delta * (buttonMat.color == door.Item2 ? -1 : 1), doorMinSize, 1f);
            door.Item1.transform.localScale = doorScale;
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
