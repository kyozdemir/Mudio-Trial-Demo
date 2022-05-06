using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : CustomBehaviour
{

    public float MovementSpeed;
    public Bullet Bullet;

    private bool mMovementEnabled = false;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        Bullet.Initialize(gameManager);

        gameManager.OnStartGame += OnStartGame;
        gameManager.OnResetToMainMenu += OnResetToMainMenu;
        gameManager.OnLevelFailed += OnLevelFailed;
    }

    private void Activate()
    {
        mMovementEnabled = true;
    }

    private void Deactivate()
    {
        mMovementEnabled = false;
        gameObject.SetActive(false);
    }

    private void ResetPlayer()
    {
        transform.SetPositionAndRotation(Vector3.zero + Vector3.up ,new Quaternion(0f,0f,0f,0f));
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (mMovementEnabled)
        {
            MovePlayer();
            AimPlayer();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Bullet.Fire();
        }

    }

    private void AimPlayer()
    {
        Vector2 mousePos = Input.mousePosition;
        
        Vector3 objectPos = MainCamera.WorldToScreenPoint(transform.position);

        mousePos.x = objectPos.x - mousePos.x;
        mousePos.y = objectPos.y - mousePos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, -angle-90, 0));
    }

    private void MovePlayer()
    {
        float horSpeed = Input.GetAxis("Horizontal");
        float verSpeed = Input.GetAxis("Vertical");

        Vector3 vel = new Vector3(horSpeed, 0f,verSpeed);
        if (vel.magnitude > 1)
        {
            vel.Normalize();
        }

        CharacterController.SimpleMove(vel * MovementSpeed);
    }



    #region Events
    private void OnResetToMainMenu()
    {
        ResetPlayer();
    }

    private void OnStartGame()
    {
        Activate();
    }

    private void OnLevelFailed()
    {
        Deactivate();
    }

    private void OnDestroy()
    {
        if (GameManager != null)
        {
            GameManager.OnStartGame -= OnStartGame;
            GameManager.OnResetToMainMenu -= OnResetToMainMenu;
        }
    }
    #endregion
}
