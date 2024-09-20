using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class CameraController : MonoBehaviour
{
    public Transform target; // ī�޶� ���� ĳ����
    public float distanceFromTarget = 4f; // ĳ���Ϳ� ī�޶� ���� �Ÿ�
    public float sensitivityX = 3f; // ���콺 ���� �ΰ���
    public float sensitivityY = 2f; // ���콺 ���� �ΰ���
    public float minYAngle = 20f; // ī�޶� ���� �ּ� ����
    public float maxYAngle = 50f; // ī�޶� ���� �ִ� ����

    private float currentYaw = 0f;
    private float currentPitch = 0f;

    private Actor actor;

    private void Start()
    {

        currentYaw = -2f;
        currentPitch = 30f;
    }

    private void LateUpdate()
    {
        // ���콺 �Է��� �޾� ī�޶� ȸ�� ����
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // ���� ȸ�� (Yaw) - ���� ����
        currentYaw += mouseX * sensitivityX;

        // ���� ȸ�� (Pitch)
        currentPitch -= mouseY * sensitivityY;
        currentPitch = Mathf.Clamp(currentPitch, minYAngle, maxYAngle); // Y��(Pitch) ���� ����

        // ī�޶� ��ġ �� ȸ�� ����
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
        Vector3 offset = new Vector3(0, 0, -distanceFromTarget); // Ÿ�ٰ��� �Ÿ� ����
        transform.position = target.position + rotation * offset; // Ÿ�� �ֺ��� ���� ��ġ ����
        transform.LookAt(target); // ī�޶� �׻� ĳ���͸� �ٶ󺸵��� ����
    }
}