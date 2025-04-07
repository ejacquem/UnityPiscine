using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private Spawner _spawner;
    [SerializeField]
    private Base _base;

    void Start()
    {
    }

    void Update()
    {
        if (_base.GetHealth() <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log($"Game Over");
        _spawner._isActive = false;
        _uiManager.SetTitle("Game Over", Color.red);
        DestroyAllEnemy();
    }

    private void GameWon()
    {
        _uiManager.SetTitle("Game Won", Color.yellow);
        DestroyAllEnemy();
    }

    void DestroyAllEnemy()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(obj);
        }
    }
}
