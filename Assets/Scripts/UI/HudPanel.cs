using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class HudPanel : UIPanel
{
    public Text ScoreText;
    public Text TimeText;


    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);

        GameManager.OnStartGame += OnStartGame;
        GameManager.OnLevelFailed += OnLevelFailed;
        GameManager.OnResetToMainMenu += OnResetToMainMenu;
    }

    private void ResetProperties()
    {
        ScoreText.text = "0";
        TimeText.text = "0";
    }

    public void SetScoreText(int score)
    {
        ScoreText.text = score.ToString();
    }
    public void SetTimerText(int remainingTime)
    {
        TimeText.text = remainingTime.ToString();
    }

    #region Events
    private void OnResetToMainMenu()
    {
        ResetProperties();
    }

    private void OnLevelFailed()
    {
        HidePanel();
    }

    private void OnStartGame()
    {
        ShowPanel();
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
