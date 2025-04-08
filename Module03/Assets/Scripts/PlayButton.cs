using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
