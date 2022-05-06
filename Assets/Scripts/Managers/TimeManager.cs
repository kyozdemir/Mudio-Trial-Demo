using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : CustomBehaviour
{
    public int Time = 60;
    private int mCurrentTime;
    private float TimeDecreaseInterval = 1f;

    private bool mTimerCanTick = true;


    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        mCurrentTime = Time;

        gameManager.OnStartGame += OnStartGame;
        gameManager.OnLevelFailed += OnLevelFailed;
        gameManager.OnResetToMainMenu += OnResetToMainMenu;

        gameManager.WaveManager.OnWaveStarted += OnWaveStarted;
        gameManager.WaveManager.OnWaveFinished += OnWaveFinished;

    }


    public void StartTimer()
    {
        mTimerCanTick = true;
        StartCoroutine(StartWaveTimer());
    }

    public void StopTimer()
    {
        mTimerCanTick = false;
    }

    public void ResetTimer()
    {
        mTimerCanTick=true;
        mCurrentTime = Time;
        TimeDecreaseInterval = 1f;
    }

    public void IncreaseRemainingTime(int time)
    {
        mCurrentTime += time;
        (GameManager.UIManager.GetPanel(Panels.Hud) as HudPanel).SetTimerText(mCurrentTime);
    }

    private void DecreaseInterval()
    {
        TimeDecreaseInterval *= .9f;
    }


    private IEnumerator StartWaveTimer()
    {
        while (mTimerCanTick)
        {
            mCurrentTime--;
            (GameManager.UIManager.GetPanel(Panels.Hud) as HudPanel).SetTimerText(mCurrentTime);
            yield return new WaitForSeconds(TimeDecreaseInterval);
        }
    }

    private void Update()
    {
        if (mTimerCanTick)
        {
            TimerCheck();
        }
    }

    private void TimerCheck()
    {
        if (mCurrentTime <= 0)
        {
            mCurrentTime = 0;
            mTimerCanTick=false;
            GameManager.LevelFailed();
        }
    }



    #region Events
    private void OnResetToMainMenu()
    {
        ResetTimer();
    }

    private void OnLevelFailed()
    {
       StopTimer();
    }

    private void OnWaveFinished()
    {
        StopTimer();
    }

    private void OnWaveStarted()
    {
        StartTimer();
        if (GameManager.WaveManager.CurrentWaveNumber > 1)
        {
            DecreaseInterval();
        }
    }

    private void OnStartGame()
    {
        //StartTimer();
    }

    private void OnDestroy()
    {
        if (GameManager != null)
        {
            GameManager.OnStartGame -= OnStartGame;
            GameManager.OnLevelFailed -= OnLevelFailed;
            GameManager.OnResetToMainMenu -= OnResetToMainMenu;

            if (GameManager.WaveManager != null)
            {
                GameManager.WaveManager.OnWaveStarted += OnWaveStarted;
                GameManager.WaveManager.OnWaveFinished += OnWaveFinished;
            }
        }
    }

    #endregion
}
