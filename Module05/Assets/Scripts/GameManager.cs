using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    private int _points;
    private int _deathsCount;

    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _pointsText;

    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        
    }

    public void DisplayPlayerHealth(float health)
    {
        _healthText.SetText($"Health: {health}");
    }

    public void DisplayPlayerPoints()
    {
        _pointsText.SetText($"Points: {_points}");
    }

    public void AddPoints(int points)
    {
        _points += points;
        DisplayPlayerPoints();
    }

    public void AddDeaths()
    {
        _deathsCount++;
    }
}
