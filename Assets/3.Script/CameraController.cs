using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    public Transform target; // 카메라가 따라갈 캐릭터
    public float distanceFromTarget = 4f; // 캐릭터와 카메라 사이 거리
    public float sensitivityX = 3f; // 마우스 수평 민감도
    public float sensitivityY = 2f; // 마우스 수직 민감도
    public float minYAngle = 20f; // 카메라 수직 최소 각도
    public float maxYAngle = 50f; // 카메라 수직 최대 각도
    public float minXAngle = -40f; // 카메라 수평 최소 각도
    public float maxXAngle = 40f;  // 카메라 수평 최대 각도
    private float currentYaw = 0f;
    private float currentPitch = 0f;

    private void Start()
    {
        currentYaw = -2f;
        currentPitch = 30f;
    }

    private void LateUpdate()
    {
        // 마우스 입력을 받아 카메라 회전 제어
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 수평 회전 (Yaw) - X축 회전에 대한 각도 제한 추가
        currentYaw += mouseX * sensitivityX;
        currentYaw = Mathf.Clamp(currentYaw, minXAngle, maxXAngle); // X축(Yaw) 각도 제한

        // 수직 회전 (Pitch)
        currentPitch -= mouseY * sensitivityY;
        currentPitch = Mathf.Clamp(currentPitch, minYAngle, maxYAngle); // Y축(Pitch) 각도 제한

        // 카메라 위치 및 회전 적용
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
        Vector3 offset = new Vector3(0, 0, -distanceFromTarget); // 타겟과의 거리 유지
        transform.position = target.position + rotation * offset; // 타겟 주변을 돌며 위치 설정
        transform.LookAt(target); // 카메라가 항상 캐릭터를 바라보도록 설정
    }
}

