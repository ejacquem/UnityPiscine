using System;
using System.Runtime.Serialization;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private EndGameMenu _endGameMenu;
    [SerializeField]
    private Spawner _spawner;
    [SerializeField]
    private Base _base;

    private float _energy;
    [SerializeField]
    private float _startEnergy;
    [SerializeField]
    private float _energyGainSpeed;

    private int _enemyDefeated = 0; // enemy killed by the player
    private int _enemyDead = 0; // enemy killed by the player and the base
    private bool _gameEnded = false;

    [SerializeField]
    private int _rankSMinScore;
    [SerializeField]
    private int _rankFMaxScore;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        _energy = _startEnergy;
    }

    void Update()
    {
        _energy += Time.deltaTime * _energyGainSpeed;

        if (_base.GetHealth() <= 0)
        {
            GameFinished(false);
        }
        // if(_spawner.IsEmpty()){
        //     Debug.Log($"_enemyDefeated: {_enemyDefeated}");
        //     Debug.Log($"_spawner.GetEnemyToSpawn(): {_spawner.GetEnemyToSpawn()}");
        //     Debug.Log("Spawner Empty");
        // }
        if (_spawner.IsEmpty() && _enemyDead == _spawner.GetEnemyToSpawn())
        {
            GameFinished(true);
        }
    }

    public float GetBaseHP()
    {
        return _base.GetHealth();
    }
    public float GetEnergy()
    {
        return _energy;
    }
    public bool IsGameEnded()
    {
        return _gameEnded;
    }

    private void GameFinished(bool gameWon)
    {
        if(_gameEnded)
            return;
        _gameEnded = true;
        Debug.Log($"Game Over");
        _spawner._isActive = false;
        int score = GetScore();
        _endGameMenu.LevelCompleted(gameWon, score, GetRank(score), GetTitle(score));
        DestroyAllEnemy();
    }

    void DestroyAllEnemy()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(obj);
        }
    }

    public void TurretPlaced(float price)
    {
        _energy -= price;
    }

    public void LogEnemyDefeated()
    {
        _enemyDefeated++;
    }

    public void LogEnemyDead()
    {
        _enemyDead++;
    }

    public enum Rank {F, E, D, C, B, A, S};
    public enum Title {IdiotOfTheVillage, Peasant, Apprentice, Knight, Champion, LegendaryWarrior, HeroOfTheVillage};

    public int GetScore()
    {
        return (int)GetBaseHP() * 20 + _enemyDefeated * 10 + (int)_energy;
    }

    public int GetScoreIndex(int score)
    {
        float F = _rankFMaxScore;
        float S = _rankSMinScore;
        float result = 6 * ((float)score - F) / (S - F);
        Debug.Log($"score: {score} -----------------------------------------");
        Debug.Log($"F: {F}");
        Debug.Log($"S: {S}");
        Debug.Log($"(score - F) / (S - F): {(score - F) / (S - F)}");
        Debug.Log($"result: {result}");
        Debug.Log($"Mathf.Clamp((int)result, 0, 6): {Mathf.Clamp((int)result, 0, 6)}");
        return Mathf.Clamp((int)result, 0, 6);
    }

    public String GetRank(int score)
    {
        return ((Rank)GetScoreIndex(score)).ToString();
    }

    public String GetTitle(int score)
    {
        return ((Title)GetScoreIndex(score)).ToString();
    }
}
