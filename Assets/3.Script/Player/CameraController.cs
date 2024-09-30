using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraController : MonoBehaviourPun
{
    public Transform target; // 카메라가 따라갈 캐릭터
    public float distanceFromTarget = 4f; // 캐릭터와 카메라 사이 거리
    public float sensitivityX = 3f; // 마우스 수평 민감도
    public float sensitivityY = 2f; // 마우스 수직 민감도
    public float minYAngle = 20f; // 카메라 수직 최소 각도
    public float maxYAngle = 50f; // 카메라 수직 최대 각도

    private float currentYaw = 0f;
    private float currentPitch = 0f;

    private void Start()
    {
        // 카메라 초기 회전 값 설정
        currentYaw = -2f;
        currentPitch = 30f;
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        if (Camera.main == null) return; // 메인 카메라가 없다면 실행 중지

        // 마우스 입력을 받아 카메라 회전 제어
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 수평 회전 (Yaw) 업데이트
        currentYaw += mouseX * sensitivityX;

        // 수직 회전 (Pitch) 업데이트
        currentPitch -= mouseY * sensitivityY;
        currentPitch = Mathf.Clamp(currentPitch, minYAngle, maxYAngle); // 수직 각도 제한

        // 카메라 회전 및 위치 계산
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
        Vector3 offset = new Vector3(0, 0, -distanceFromTarget);

        // 메인 카메라 위치 및 회전 적용
        Camera.main.transform.position = target.position + rotation * offset;
        Camera.main.transform.LookAt(target);
    }
}

