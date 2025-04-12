using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ResumeButton()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentScene", 2)); // 2 is Stage1 scene
        UIManager.Instance.SetActive(true);
    }

    public void NewGameButton()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Stage1");
        UIManager.Instance.SetActive(true);
    }

    public void DiaryButton()
    {
        SceneManager.LoadScene("Diary");
        UIManager.Instance.SetActive(false);
    }
}
