using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : CustomBehaviour
{
    public CameraOption InGameCameraOption;
    private CameraOption mCurrentCameraOption;
    private Transform mTarget;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        mTarget = gameManager.Player.transform;
        mCurrentCameraOption = InGameCameraOption;
    }

    private void Update()
    {
        if (mTarget != null)
            FollowTarget();
    }


    private void FollowTarget()
    {
        MainCamera.transform.eulerAngles = mCurrentCameraOption.Rotation;
        MainCamera.fieldOfView = mCurrentCameraOption.Fov;

        MainCamera.transform.position = new Vector3(mTarget.position.x + mCurrentCameraOption.Position.x, mTarget.position.y + mCurrentCameraOption.Offsets.y, mTarget.position.z - mCurrentCameraOption.Offsets.z);
    }
}

[Serializable]
public struct CameraOption
{
    public Vector3 Position;
    public Vector3 Rotation;
    public float Fov;
    public Vector3 Offsets;
}
