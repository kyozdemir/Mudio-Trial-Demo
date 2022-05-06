using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : CustomBehaviour
{
    public Transform InitialTarget;
    public float StartingRange;
    public int StrikeCount;

    private bool mCanFire = true;

    private float mCurrentRange;
    private Vector3 mStartingPos;

    private const float MAX_RANGE = 10f;
    private const int MAX_KILL_SCORE = 100;
    private const int MIN_KILL_SCORE = 20;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        mCurrentRange = StartingRange;

        gameManager.OnResetToMainMenu += OnResetToMainMenu;
        gameManager.WaveManager.OnWaveFinished += OnWaveFinished;
    }

    

    private IEnumerator StartFire()
    {
        mCanFire = false;
        mStartingPos = transform.position;

        transform.SetParent(null);

        Rigidbody.isKinematic = false;
        Rigidbody.velocity = GameManager.Player.transform.forward * (mCurrentRange*2);
        
        yield return new WaitForSeconds(.5f);
        Rigidbody.isKinematic = true;


        while (Vector3.Distance(transform.position,InitialTarget.position) > .1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, InitialTarget.position, .5f);
            yield return new WaitForEndOfFrame();
        }

        transform.SetParent(GameManager.Player.transform);
        StrikeCount = 0;
        mCanFire = true;
    }

    public void Fire()
    {
        if (mCanFire)
        {
            StartCoroutine(StartFire());
        }
    }

    private void UpdateRange()
    {
        if (mCurrentRange < MAX_RANGE)
        {
            mCurrentRange += .5f;
        }
    }

    private void ResetProperties()
    {
        mCurrentRange = StartingRange;
        StrikeCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (mCanFire)
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            StrikeCount++;
            other.GetComponent<Enemy>().DestroyEnemy();
            GameManager.TimeManager.IncreaseRemainingTime(UnityEngine.Random.Range(2, 5));
            GameManager.ScoreManager.UpdateScore(CalculateScore() * StrikeCount);
        }
    }

    private int CalculateScore()
    {
        float dist = Vector3.Distance(mStartingPos, transform.position);

        int score = Mathf.FloorToInt(((MAX_KILL_SCORE - MIN_KILL_SCORE) * dist) / mCurrentRange);

        score += MIN_KILL_SCORE;

        return score;
    }

    #region Events
    private void OnWaveFinished()
    {
        UpdateRange();
    }

    private void OnResetToMainMenu()
    {
        ResetProperties();
    }

    private void OnDestroy()
    {
        if (GameManager != null)
        {
            GameManager.OnResetToMainMenu -= OnResetToMainMenu;

            if (GameManager.WaveManager != null)
            {
                GameManager.WaveManager.OnWaveFinished -= OnWaveFinished;
            }
        }
    }
    #endregion
}
