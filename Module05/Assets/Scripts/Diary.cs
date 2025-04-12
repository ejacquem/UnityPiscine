using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Diary : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _diaryText;

    [SerializeField] private GameObject _imageStage1;
    [SerializeField] private GameObject _imageStage2;
    [SerializeField] private GameObject _imageStage3;

    void Start()
    {
        int unlocked = PlayerPrefs.GetInt("UnlockedStage", 1);
        _diaryText.SetText(
            $"Total leaf points: {PlayerPrefs.GetInt("TotalPoints", 0)}\n" + 
            $"Deaths: {PlayerPrefs.GetInt("Deaths", 0)}");
        _imageStage1.SetActive(!(unlocked >= 1));
        _imageStage2.SetActive(!(unlocked >= 2));
        _imageStage3.SetActive(!(unlocked >= 3));

        Debug.Log($"!(unlocked >= 1) {!(unlocked >= 1)}");
        Debug.Log($"!(unlocked >= 2) {!(unlocked >= 2)}");
        Debug.Log($"!(unlocked >= 3) {!(unlocked >= 3)}");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
