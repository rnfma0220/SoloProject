using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraController : MonoBehaviourPun
{
    public Transform target; // ī�޶� ���� ĳ����
    public float distanceFromTarget = 4f; // ĳ���Ϳ� ī�޶� ���� �Ÿ�
    public float sensitivityX = 3f; // ���콺 ���� �ΰ���
    public float sensitivityY = 2f; // ���콺 ���� �ΰ���
    public float minYAngle = 20f; // ī�޶� ���� �ּ� ����
    public float maxYAngle = 50f; // ī�޶� ���� �ִ� ����

    private float currentYaw = 0f;
    private float currentPitch = 0f;

    private void Start()
    {
        // ī�޶� �ʱ� ȸ�� �� ����
        currentYaw = -2f;
        currentPitch = 30f;
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        if (Camera.main == null) return; // ���� ī�޶� ���ٸ� ���� ����

        // ���콺 �Է��� �޾� ī�޶� ȸ�� ����
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // ���� ȸ�� (Yaw) ������Ʈ
        currentYaw += mouseX * sensitivityX;

        // ���� ȸ�� (Pitch) ������Ʈ
        currentPitch -= mouseY * sensitivityY;
        currentPitch = Mathf.Clamp(currentPitch, minYAngle, maxYAngle); // ���� ���� ����

        // ī�޶� ȸ�� �� ��ġ ���
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
        Vector3 offset = new Vector3(0, 0, -distanceFromTarget);

        // ���� ī�޶� ��ġ �� ȸ�� ����
        Camera.main.transform.position = target.position + rotation * offset;
        Camera.main.transform.LookAt(target);
    }
}

