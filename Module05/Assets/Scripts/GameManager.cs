using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    private int _points;
    private int _totalPoints;
    private int _deathsCount;
    private int _unlockedStage = 1;

    void Start()
    {
        UIManager.Instance.DisplayPlayerPoints(_points);
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            AddUnlockStage(nextSceneIndex);
            PlayerPrefs.DeleteKey("Health");
            PlayerPrefs.DeleteKey("Points");
            _points = 0;
            PlayerPrefs.DeleteKey("PositionX");
            PlayerPrefs.DeleteKey("PositionY");
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
            UIManager.Instance.SetActive(false);
        }
    }

    public void LoadMainMenu()
    {
        PlayerPrefs.SetFloat("PositionX", GameObject.FindGameObjectWithTag("Player").transform.position.x);
        PlayerPrefs.SetFloat("PositionY", GameObject.FindGameObjectWithTag("Player").transform.position.y);
        SceneManager.LoadScene("MainMenu");
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int GetPoints()
    {
        return _points;
    }

    public void AddPoints(int points)
    {
        _points += points;
        _totalPoints += points;
        PlayerPrefs.SetInt("Points", _points);
        PlayerPrefs.SetInt("TotalPoints", _totalPoints);
        UIManager.Instance.DisplayPlayerPoints(_points);
    }

    public void AddDeaths()
    {
        _deathsCount++;
        PlayerPrefs.SetInt("Deaths", _deathsCount);
    }

    public void AddUnlockStage(int index)
    {
        _unlockedStage = Math.Max(index, PlayerPrefs.GetInt("UnlockedStage", 1));
        PlayerPrefs.SetInt("UnlockedStage", _unlockedStage);
    }
}
