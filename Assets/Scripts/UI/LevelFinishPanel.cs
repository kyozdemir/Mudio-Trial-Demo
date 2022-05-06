using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class LevelFinishPanel : UIPanel
{
    public GameObject ScoreGroup;
    public Text ScoreText;

    public GameObject HighScoreGroup;
    public Text HighScoreText;


    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);

        GameManager.OnLevelFailed += OnLevelFailed;
        GameManager.OnResetToMainMenu += OnResetToMainMenu;
    }

    public void SetScoreText(int score)
    {
        ScoreText.text = score.ToString();
    }

    public void SetHighScoreText(int highScore)
    {
        HighScoreText.text = highScore.ToString();
    }

    public void ActivateHighScore(int highScore)
    {
        ScoreGroup.SetActive(false);
        HighScoreGroup.SetActive(true);
        SetHighScoreText(highScore);
    }

    public void ResetProperties()
    {
        SetScoreText(0);
        ScoreGroup.SetActive(true);
        HighScoreGroup.SetActive(false);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();

        ResetProperties();
    }

    #region Events

    public void OnButtonClickedRestart()
    {
        GameManager.ResetToMainMenu();
    }

    private void OnResetToMainMenu()
    {
        HidePanel();
    }

    private void OnLevelFailed()
    {
        Invoke("ShowPanel", 1f);
    }

    private void OnDestroy()
    {
        if (GameManager != null)
        {
            GameManager.OnLevelFailed -= OnLevelFailed;
            GameManager.OnResetToMainMenu -= OnResetToMainMenu;
        }
    }

    #endregion
}
