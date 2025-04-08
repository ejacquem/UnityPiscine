using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private CellManager cellManager;
    [SerializeField]
    private GameObject turretPrefab;
    [SerializeField]
    private TextMeshProUGUI infoText;

    private RectTransform rect;
    private CanvasGroup canvasGroup;
    private Vector3 originalPos;
    private float turretPrice;
    private Image image;
    private bool isDrag = false;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        originalPos = rect.anchoredPosition;

        Turret turret = turretPrefab.GetComponent<Turret>();
        turretPrice = turret.GetPrice();
        infoText.SetText($"Damage: {turret.GetDamage()}\nPrice: {turret.GetPrice()}\nFireRate: {turret.GetFireRate()}\n");
    }

    private void Update()
    {
        if (turretPrice <= GameManager.Instance.GetEnergy())
        {
            image.color = Color.white;
        }
        else
            image.color = new Color(0.3f, 0.3f, 0.3f);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        if (turretPrice > GameManager.Instance.GetEnergy())
            return;
        isDrag = true;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.5f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        if(isDrag == false)
            return;
        isDrag = false;
        rect.anchoredPosition = originalPos;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        cellManager.PlaceTurretOnCurrentCell(turretPrefab);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isDrag == false)
            return;
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
}
