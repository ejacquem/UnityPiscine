using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI titleText;

    private float deltaTime = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        SetFps();
    }

    private void SetFps()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        fpsText.SetText($"FPS: {Mathf.RoundToInt(1.0f / deltaTime)}");
    }

    public void SetTitle(String text, Color textColor)
    {
        titleText.SetText(text);
        titleText.color = textColor;
    }
}
