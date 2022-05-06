using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : CustomBehaviour
{
    public int HighScore = 0;
    private int mCurrentScore = 0;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        GameManager.OnStartGame += OnStartGame;
        GameManager.OnLevelFailed += OnLevelFailed;
        GameManager.OnResetToMainMenu += OnResetToMainMenu;
    }

    public void UpdateScore(int score)
    {
        mCurrentScore += score;
        (GameManager.UIManager.GetPanel(Panels.Hud) as HudPanel).SetScoreText(mCurrentScore);
    }

    public void CheckHighScore()
    {
        var finishedPanel = (GameManager.UIManager.GetPanel(Panels.LevelFinish) as LevelFinishPanel);
        
        if (mCurrentScore > HighScore)
        {
            HighScore = mCurrentScore;
            finishedPanel.ActivateHighScore(HighScore);
        }

        finishedPanel.SetScoreText(HighScore);

    }

    #region Events
    private void OnResetToMainMenu()
    {
        mCurrentScore = 0;
    }

    private void OnLevelFailed()
    {
        CheckHighScore();
    }

    private void OnStartGame()
    {
        
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
