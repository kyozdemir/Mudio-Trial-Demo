using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class MainMenuPanel : UIPanel
{
    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);

        GameManager.OnResetToMainMenu += OnResetToMainMenu;
    }


    #region Events
    public void OnClickedButtonPlay()
    {
        GameManager.StartGame();
        HidePanel();
    }

    private void OnResetToMainMenu()
    {
        ShowPanel();
    }

    private void OnDestroy()
    {
        if (GameManager != null)
        {
            GameManager.OnResetToMainMenu -= OnResetToMainMenu;
        }
    }

    #endregion
}
