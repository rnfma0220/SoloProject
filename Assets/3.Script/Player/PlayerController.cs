using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
        private Vector3 moveDirection;
        private float moveSpeed = 2f;
        private float RunSpeed = 2f;
        private float rotationSpeed = 3f; // ȸ�� �ӵ� ����
        private Actor actor;
        public Collider PunchGrabTarget;

        private bool LeftisHolding = false;
        private float LeftholdTimer = 0f;

        private bool RightisHolding = false;
        private float RightholdTimer = 0f;

        private CharacterController characterController; // ĳ���� ��Ʈ�ѷ� ����
        public float gravity = -9.81f; // �߷� ��
        private Vector3 velocity; // �߷� ������ ���� �ӵ� ����

        private Vector2 input;

        private void Awake()
        {
            TryGetComponent(out actor);
            characterController = GetComponent<CharacterController>(); // ĳ���� ��Ʈ�ѷ� �ʱ�ȭ
        }

        private void LateUpdate()
        {
            if (moveDirection != Vector3.zero)
            {
                // ī�޶��� ȸ�� ������ �ǽð����� ������ �̵� ���⿡ �ݿ�
                Vector3 forward = Camera.main.transform.forward;
                Vector3 right = Camera.main.transform.right;

                // Y���� �����Ͽ� ��� �̵��� ���
                forward.y = 0f;
                right.y = 0f;

                // ī�޶� ȸ�� �������� �̵� ������ �ٽ� ����
                moveDirection = (forward * input.y + right * input.x).normalized;

                // �̵� ó��
                Vector3 move = moveDirection * moveSpeed * Time.fixedDeltaTime;
                characterController.Move(move);

                // ĳ���� ȸ�� ó��
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

                // �߷� ó��
                if (!characterController.isGrounded)
                {
                    velocity.y += gravity * Time.fixedDeltaTime;
                }
                else
                {
                    velocity.y = -2f;
                }

                // �߷� ���� �̵�
                characterController.Move(velocity * Time.fixedDeltaTime);

                // �̵� ���� ������Ʈ
                actor.movementHandeler.direction = moveDirection;
            }

            if (LeftisHolding)
            {
                if (actor.actorState == Actor.ActorState.Unconscious) return;
                if (actor.actorState == Actor.ActorState.Dead) return;

                LeftholdTimer += Time.deltaTime;

                if (LeftholdTimer < 0.1f)
                {
                    actor.movementHandeler.ArmReadying(MovementHandeler.Side.Left);
                }
                if (LeftholdTimer < 0.2f)
                {
                    actor.movementHandeler.ArmPunching(MovementHandeler.Side.Left, PunchGrabTarget);
                }
            }

            if (RightisHolding)
            {
                if (actor.actorState == Actor.ActorState.Unconscious) return;
                if (actor.actorState == Actor.ActorState.Dead) return;

                RightholdTimer += Time.deltaTime;

                if (RightholdTimer < 0.1f)
                {
                    actor.movementHandeler.ArmReadying(MovementHandeler.Side.Right);
                }
                if (RightholdTimer < 0.2f)
                {
                    actor.movementHandeler.ArmPunching(MovementHandeler.Side.Right, PunchGrabTarget);
                }
            }

        }

        public void OnMove(InputAction.CallbackContext context)
        {
            input = context.ReadValue<Vector2>();

            if (input != Vector2.zero)
            {
                // ī�޶��� forward�� right�� �ǽð����� �ݿ��Ͽ� �̵� ���� ���
                Vector3 forward = Camera.main.transform.forward;
                Vector3 right = Camera.main.transform.right;

                // ī�޶� �ٶ󺸴� ���⿡ ���� 2D ��� �󿡼��� �̵� ���� ���
                forward.y = 0f;  // Y�� ����, ��� �̵��� ���
                right.y = 0f;

                // ī�޶� ������ �������� �̵� ���� ����
                moveDirection = (forward * input.y + right * input.x).normalized;

                // ĳ���� ���¸� Run���� ��ȯ
                actor.actorState = Actor.ActorState.Run;
            }
            else
            {
                // �Է��� ���� ���� �̵� ����
                moveDirection = Vector3.zero;
                actor.actorState = Actor.ActorState.Stand;
            }
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                moveSpeed = RunSpeed * 2f;
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                moveSpeed = RunSpeed;
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Canceled)
            {

                actor.actorState = Actor.ActorState.Jump;

                Debug.Log("����");

                if (actor.isGround)
                {
                    actor.actorState = Actor.ActorState.Jump;
                    // Jump�� �߰��ϰ� �ʹٸ� ���⿡ ĳ���� ��Ʈ�ѷ��� ����Ͽ� ����
                }
            }
        }

        public void OnLeftHand(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                LeftisHolding = true;
                LeftholdTimer = 0f;
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                LeftisHolding = false;
                LeftholdTimer = 0f;
            }
        }

        public void OnRightHand(InputAction.CallbackContext context)
        {

            if (context.phase == InputActionPhase.Started)
            {
                RightisHolding = true;
                RightholdTimer = 0f;
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                RightisHolding = false;
                RightholdTimer = 0f;
            }
        }
    }
}
