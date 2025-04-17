using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    void Start()
    {

    }

    public void TellGameManagerToRestartLevel()
    {
        GameManager.Instance.RestartLevel();
    }
}
