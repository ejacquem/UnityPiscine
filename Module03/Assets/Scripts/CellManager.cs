using UnityEngine;

public class CellManager : MonoBehaviour
{
    private Cell currentCell;

    [SerializeField]
    private LayerMask raycastLayerMask;

    public GameObject selectedTurret;

    void Update()
    {
        if(Time.deltaTime == 0 || GameManager.Instance.IsGameEnded())
            return;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 10f, raycastLayerMask);
        Cell cell;

        if (hit.collider != null && (cell = hit.collider.GetComponentInChildren<Cell>()))
        {
            if (currentCell != cell)
            {
                if (currentCell)
                    currentCell.OnHoverExit();
                currentCell = cell;
                currentCell.OnHover();
            }
            // Debug.Log("Mouse is over: " + hit.collider.gameObject.name);
        }
        else if (currentCell){
            currentCell.OnHoverExit();
            currentCell = null;
        }
    }

    public void PlaceTurretOnCurrentCell(GameObject turret)
    {
        if (currentCell)
        {
            currentCell.PlaceTurret(turret);
        }
    }
}
