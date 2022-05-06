using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBehaviour : MonoBehaviour
{
    public GameManager GameManager { get; set; }

    private CharacterController mCharacterController;
    public CharacterController CharacterController
    {
        get
        {
            if (mCharacterController == null)
            {
                mCharacterController = base.GetComponent<CharacterController>();
            }
        
            return mCharacterController;
        } 
    }

    private Camera mMainCamera;

    public Camera MainCamera
    {
        get
        {
            if (mMainCamera == null)
            {
                mMainCamera = Camera.main;
            }

            return mMainCamera;
        }
    }

    private CanvasGroup mCanvasGroup;

    public CanvasGroup CanvasGroup
    {
        get
        {
            if (mCanvasGroup == null)
            {
                mCanvasGroup = base.GetComponent<CanvasGroup>();
            }
            return mCanvasGroup;
        }
    }

    private Rigidbody mRigidbody;

    public Rigidbody Rigidbody
    {
        get
        {
            if (mRigidbody == null)
            {
                mRigidbody = base.GetComponent<Rigidbody>();
            }
            return mRigidbody;
        }
    }

    public virtual void Initialize(GameManager gameManager)
    {
        GameManager = gameManager;
    }

    
}
