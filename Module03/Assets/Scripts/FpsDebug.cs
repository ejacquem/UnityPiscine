using UnityEngine;
using TMPro;

public class FpsDebug : MonoBehaviour
{
    private TextMeshProUGUI fpsText;
    private float deltaTime = 0.0f;

    [SerializeField]
    private bool smoothDisplay = false;

    private void Start()
    {
        fpsText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.deltaTime == 0)
            return;
        if (smoothDisplay){
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            fpsText.SetText($"FPS: {Mathf.RoundToInt(1.0f / deltaTime)}");
        }
        else
            fpsText.SetText($"FPS: {Mathf.RoundToInt(1.0f / Time.deltaTime)}");
    }
}
