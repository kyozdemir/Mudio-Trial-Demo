using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : CustomBehaviour
{

    public Player Player;
    public WaveManager WaveManager;
    public PoolManager PoolManager;
    public TimeManager TimeManager;
    public ScoreManager ScoreManager;
    public CameraManager CameraManager;
    public UIManager UIManager;

    public event Action OnStartGame;
    public event Action OnResetToMainMenu;
    public event Action OnLevelFailed;

    private void Awake()
    {
        Player.Initialize(this);
        PoolManager.Initialize(this);
        WaveManager.Initialize(this);
        TimeManager.Initialize(this);
        ScoreManager.Initialize(this);
        CameraManager.Initialize(this);
        UIManager.Initialize(this);
    }

    public void StartGame()
    {
        if (OnStartGame != null)
        {
            OnStartGame();
        }
    }

    public void ResetToMainMenu()
    {
        if (OnResetToMainMenu != null)
        {
            OnResetToMainMenu();
        }
    }

    public void LevelFailed()
    {
        if (OnLevelFailed != null)
        {
            OnLevelFailed();
        }
    }
}
