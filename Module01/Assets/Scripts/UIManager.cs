using UnityEngine;
using TMPro;
// ReSharper disable All

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI titleText;

    private float deltaTime = 0.0f;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f; // Smooths FPS calculation
        SetFps(1.0f / deltaTime);
    }

    private void SetFps(float fps)
    {
        fpsText.SetText($"FPS: {Mathf.RoundToInt(fps)}");
    }

    public void StartTimer()
    {
        Debug.Log("Game Started");
    }

    public void GameOver()
    {
        titleText.SetText("Game Over");
        Debug.Log("Game Over");
    }

    public void GameWon()
    {
        titleText.SetText("Game Won");
        Debug.Log("Game Won");
    }
}