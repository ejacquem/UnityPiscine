using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Diary : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _diaryText;

    void Start()
    {
        int unlocked = PlayerPrefs.GetInt("UnlockedStage", 0);
        _diaryText.SetText(
            $"Total leaf points: {PlayerPrefs.GetInt("TotalPoints", 0)}\n" + 
            $"Deaths: {PlayerPrefs.GetInt("Deaths", 0)}\n" + 
            $"Stage1: {(unlocked >= 1 ? "Unlocked" : "Locked")}\n" + 
            $"Stage2: {(unlocked >= 2 ? "Unlocked" : "Locked")}\n" + 
            $"Stage3: {(unlocked >= 3 ? "Unlocked" : "Locked")}\n");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
