using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    private Cinemachine.CinemachineVirtualCamera virtualCam;
    [SerializeField] private bool isInMenu = false;

    private void Start() {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        virtualCam.m_Lens.OrthographicSize = (isInMenu ? 5f : 5.7f) * Screen.height / Screen.width * 0.5f;
    }
}
