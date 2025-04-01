using UnityEngine;
using TMPro;
// ReSharper disable All

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI timerText;
    public GameObject player;
    private float deltaTime = 0.0f;
    private bool timerStarted = false;
    private bool gameWon = false;
    private float timer = 0;
    
    void Start()
    {
        SetTimer(0f);
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f; // Smooths FPS calculation
        SetFps(1.0f / deltaTime);
        if (timerStarted){
            timer += Time.deltaTime;
            SetTimer(timer);
        }
    }

    private void SetFps(float fps)
    {
        fpsText.SetText($"FPS: {Mathf.RoundToInt(fps)}");
    }

    private void SetTimer(float timer)
    {
        timerText.SetText($"{timer:00.00}");
    }

    public void StartTimer()
    {
        timerStarted = true;
        Debug.Log("Game Started");
    }

    public void GameOver()
    {
        if (gameWon)
            return;
        titleText.SetText("Game Over");
        timerStarted = false;
        Destroy(player, 0.2f);
        Debug.Log("Game Over");
    }

    public void GameWon()
    {
        titleText.SetText("Game Won");
        timerStarted = false;
        gameWon = true;
        Debug.Log("Game Won");
    }
}
