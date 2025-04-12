using UnityEngine;

public class Portal : MonoBehaviour
{
    public enum PortalType {START, END};

    [SerializeField] private PortalType portalType;

    [SerializeField] private int _requiredPoint;
    [SerializeField] private float _rotationSpeed;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, Time.time * _rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (portalType == PortalType.END)
            {
                if(GameManager.Instance.GetPoints() >= _requiredPoint)
                    GameManager.Instance.LoadNextScene();
                else
                    Debug.Log("Not enough points :(");
            }
        }
    }
}
