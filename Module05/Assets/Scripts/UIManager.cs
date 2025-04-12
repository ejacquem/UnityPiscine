using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _pointsText;

    [SerializeField] private GameObject _pauseMenu;

    void Awake()
    {
        SetActive(false);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent destruction on scene load
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex >= 2)
            PauseMenuSetAcrtive(!_pauseMenu.activeSelf);
    }

    public void DisplayPlayerHealth(float health)
    {
        _healthText.SetText($"Health: {health}");
    }

    public void DisplayPlayerPoints(float points)
    {
        _pointsText.SetText($"Points: {points}");
    }

    public void SetActive(bool b)
    {
        _pointsText.enabled = b;
        _healthText.enabled = b;
    }

    public void MainMenuButton()
    {
        GameManager.Instance.LoadMainMenu();
        SetActive(false);
        PauseMenuSetAcrtive(false);
    }

    public void ResumeButton()
    {
        PauseMenuSetAcrtive(false);
    }

    private void PauseMenuSetAcrtive(bool isPaused)
    {
        if (isPaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;

        _pauseMenu.SetActive(isPaused);
    }
}
