using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class SpawnBots : MonoBehaviour
{
    public Action<Life[]> OnWaveSpawned;
    public Action OnWavesOver;
    public Action OnWaveEnd;

    [Title(label: "Spawn Setting")]
    [SerializeField] private SpawnBot[] spawnBots;
    [SerializeField] private SpawnBot spawnBoss;
    [SerializeField] private float plusEnemyWithLevel = 1;
    [SerializeField] private int howManyLevelsSpawnBoss = 5;

    [Title(label: "NO Wave Game Mode")]
    [SerializeField] private int noWaveGMValidUpToLevelNumber = 5;
    [SerializeField] private int countWaveInNOWaveGM = 1;

    [Title(label: "Wave Game Mode")]
    [SerializeField] private int countWaveInWaveGameMode = 3; 

    [Title(label: "Wave Setting")]
    [SerializeField] private int delayAfterEndWave = 6;

    private Life[] _currentEnemyLife;
    private bool _isAllEnemiesKilled;
    private Level _level;
    private int _countWave;

    private int _numberWave = 0;
    public int NumberWave { get { return _numberWave; } private set { _numberWave = value; } }

    private void Start()
    {
        _level = FindObjectOfType<Level>();
        _countWave = _level.CurrentLevel > noWaveGMValidUpToLevelNumber ? countWaveInWaveGameMode : countWaveInNOWaveGM;
        StartCoroutine(StartWaves());
    }

    private void Update()
    {
        if (_currentEnemyLife != null)
            _isAllEnemiesKilled = CheckForKilledEnemies();
    }

    private IEnumerator StartWaves()
    {
        for (var i = 0; i < _countWave; i++)
        {
            if (i != 0) OnWaveEnd?.Invoke();
            yield return new WaitUntil(() => StateGameManager.StateGame == StateGameManager.State.Game);
            NumberWave ++;
            _isAllEnemiesKilled = false;
            _currentEnemyLife = SpawnEnemies(spawnBots);
            OnWaveSpawned?.Invoke(_currentEnemyLife);
            yield return new WaitUntil(() => _isAllEnemiesKilled);
            yield return new WaitForSeconds(delayAfterEndWave);
        }
        OnWavesOver?.Invoke();
    }

    private Life[] SpawnEnemies(SpawnBot[] spawnEnemy)
    {
        
        var enemy = new List<Life>();
        for (var i = 0; i < spawnEnemy.Length; i ++)
        {   
            var countEnemy = (int)(spawnEnemy[i].Count + _level.CurrentLevel * plusEnemyWithLevel);
            for (var j = 0; j < countEnemy; j++)
            {
                var numberSpawnPoint = Random.Range(0, spawnEnemy[i].SpawnPoints.Length);
                var spawnPoint = spawnEnemy[i].SpawnPoints[numberSpawnPoint];
                enemy.Add(Instantiate(spawnEnemy[i].BotPrefs.gameObject, spawnPoint.position, spawnPoint.rotation)
                    .GetComponent<Life>());
            }
        }
        if (_level.CurrentLevel % howManyLevelsSpawnBoss == 0)
            enemy.Add(SpawnBoss());
        return enemy.ToArray();
    }

    private Life SpawnBoss()
    { 
        var spawnPoint = spawnBoss.SpawnPoints[0];
        return Instantiate(spawnBoss.BotPrefs, spawnPoint.position, spawnPoint.rotation)
                    .GetComponent<Life>();
    }

    private bool CheckForKilledEnemies()
    {
        foreach (var enemy in _currentEnemyLife)
            if (!enemy.IsDid) return false;
        return true;
    }

    [Serializable]
    private class SpawnBot
    {
        public AIBotController BotPrefs;
        public int Count;
        public Transform[] SpawnPoints;
    }
}