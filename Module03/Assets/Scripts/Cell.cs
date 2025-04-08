using UnityEngine;

public class Cell : MonoBehaviour
{
    private SpriteRenderer sprite;
    private bool occupied = false;
    private Color defaultColor;

    [SerializeField]
    private Color valid;
    [SerializeField]
    private Color invalid;

    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        defaultColor = sprite.color;
    }

    void Update()
    {
        // if(mouse)
        //     sprite.color = Color.black;
    }

    public void OnHover()
    {
        Debug.Log("OnHover");
        if (occupied)
            sprite.color = invalid;
        else
            sprite.color = valid;
    }

    public void OnHoverExit()
    {
        Debug.Log("OnHoverExit");
        sprite.color = defaultColor;
    }

    public void PlaceTurret(GameObject turret)
    {
        Turret t = turret.GetComponent<Turret>();
        if (!occupied && t != null)
        {
            Instantiate(turret, transform.position, Quaternion.identity);
            occupied = true;
            GameManager.Instance.TurretPlaced(t.GetPrice());
        }
    }

}
