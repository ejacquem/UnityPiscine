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

    private int _enemyDefeated = 0;
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
        if(_spawner.IsEmpty()){
            Debug.Log($"_enemyDefeated: {_enemyDefeated}");
            Debug.Log($"_spawner.GetEnemyToSpawn(): {_spawner.GetEnemyToSpawn()}");
            Debug.Log("Spawner Empty");
        }
        if (_spawner.IsEmpty() && _enemyDefeated == _spawner.GetEnemyToSpawn())
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

    public enum Rank {F, E, D, C, B, A, S};
    public enum Title {IdiotOfTheVillage, Peasant, Apprentice, Knight, Champion, LegendaryWarrior, HeroOfTheVillage};

    public int GetScore()
    {
        return _enemyDefeated * 10 + (int)_energy;
    }

    public int GetScoreIndex(int score)
    {
        int F = _rankFMaxScore;
        int S = _rankSMinScore;
        float result = (score - F) / (S - F) * 6;
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
