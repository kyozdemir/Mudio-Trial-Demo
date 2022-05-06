using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveManager : CustomBehaviour
{
    public int CurrentWaveNumber;
    public int StartingEnemyCount;
    public float StartingEnemySpeed;

    public bool isWaveStarted = false;

    private int mCurrentWaveEnemyCount;
    private float mCurrentWaveEnemySpeed;

    private int mMaxEnemyCount = 10;
    private float mMaxEnemySpeed = 0;

    public event Action OnWaveStarted;
    public event Action OnWaveFinished;

    private static float randX = 0f;
    private static float randZ = 0f;

    private List<Enemy> mActivatedEnemyList = new List<Enemy>();

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        InitializeCustomOptions();

        GameManager.OnStartGame += OnStartGame;
        GameManager.OnLevelFailed += OnLevelFailed;
        GameManager.OnResetToMainMenu += OnResetToMainMenu;
    }

    private void InitializeCustomOptions()
    {
        StartingEnemySpeed = GameManager.Player.MovementSpeed / 2f;

        mCurrentWaveEnemySpeed = StartingEnemySpeed;

        mMaxEnemySpeed = StartingEnemySpeed * 2f;

        mCurrentWaveEnemyCount = StartingEnemyCount;
    }

    private void UpdateEnemyCount()
    {
        if (mCurrentWaveEnemyCount < mMaxEnemyCount)
        {
            mCurrentWaveEnemyCount++;
        }
        else
        {
            mCurrentWaveEnemyCount = mMaxEnemyCount;
        }
    }

    private void UpdateEnemySpeed()
    {
        if (mCurrentWaveEnemySpeed < mMaxEnemySpeed)
        {
            mCurrentWaveEnemySpeed = mCurrentWaveEnemySpeed + (mCurrentWaveEnemySpeed * .1f);
        }
    }

    public void StartWave()
    {
        if (OnWaveStarted != null)
        {
            OnWaveStarted();
        }

        CurrentWaveNumber++;
        SpawnEnemies();
    }

    public void EndWave()
    {
        if (OnWaveFinished != null)
        {
            OnWaveFinished();
        }

        UpdateWaveProperties();
    }

    private void UpdateWaveProperties()
    {
        UpdateEnemyCount();
        UpdateEnemySpeed();

        Invoke("StartWave", 2f);
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < mCurrentWaveEnemyCount; i++)
        {
            randX = UnityEngine.Random.Range(19f, -19f);
            randZ = UnityEngine.Random.Range(19f, -19f);

            Vector3 spawnPos = new Vector3(randX,1f,randZ);

            Enemy enemy =  GameManager.PoolManager.SpawnEnemy(spawnPos);
            mActivatedEnemyList.Add(enemy);
        }

        isWaveStarted = true;
    }

    private void Update()
    {
        if (isWaveStarted)
        {
            CheckWave();
        }
    }

    private void CheckWave()
    {
        if (mActivatedEnemyList.Count <= 0)
        {
            isWaveStarted = false;
            EndWave();
        }
    }

    public void DestroyEnemy(Enemy enemy)
    {
        mActivatedEnemyList.Remove(enemy);
        GameManager.PoolManager.DespawnEnemy(enemy);
    }

    public void DestroyAllEnemies()
    {
        foreach (Enemy item in mActivatedEnemyList)
        {
            DestroyEnemy(item);
        }
    }

    public float GetCurrentEnemySpeed()
    {
        return mCurrentWaveEnemySpeed;
    }

    #region Events
    private void OnResetToMainMenu()
    {
        
    }

    private void OnLevelFailed()
    {
        
    }

    private void OnStartGame()
    {
        StartWave();
    }

    private void OnDestroy()
    {
        if (GameManager != null)
        {
            GameManager.OnStartGame -= OnStartGame;
            GameManager.OnLevelFailed -= OnLevelFailed;
            GameManager.OnResetToMainMenu -= OnResetToMainMenu;
        }
    }

    #endregion
}
