using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaf : MonoBehaviour
{
    [SerializeField] private int _points;
    [SerializeField] private float _hoverSpeed;
    [SerializeField] private float _hoverSize;
    [SerializeField] private string _ID;
    private string _prefix;
    private Vector3 startPos;
    private Vector3 pos;

    private void Start()
    {
        _prefix = "leaf_" + SceneManager.GetActiveScene().buildIndex + "_";
        startPos = transform.position;
        if (PlayerPrefs.GetInt(_prefix + _ID, 0) == 1)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        pos = startPos;
        pos.y += Mathf.Sin(Time.time * _hoverSpeed) * _hoverSize;
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"Leaf Collected");
            PlayerPrefs.SetInt(_prefix + _ID, 1);
            GameManager.Instance.AddPoints(_points);
            Destroy(gameObject);
        }
    }
}
