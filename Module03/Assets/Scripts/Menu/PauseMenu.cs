using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject confirmMenu;
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Debug.Log("Game Paused");
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        confirmMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Debug.Log("Game Resumed");

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void QuitButton()
    {
        
    }

    public void ConfirmButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
