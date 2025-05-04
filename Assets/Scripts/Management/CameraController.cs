using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraController : Singleton<CameraController>
{
    private void Start()
    {
        SetPlayerCameraFollow();
    }

    public void SetPlayerCameraFollow(){
        var gameObj = GameObject.FindGameObjectWithTag("camera");
        var requiredComponent = gameObj.GetComponent<CinemachineCamera>();
        requiredComponent.Follow = PlayerController.Instance.transform;
    }
}
