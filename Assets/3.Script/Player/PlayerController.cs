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
        private float moveSpeed = 40f;
        private float RunSpeed = 40f;
        private float rotationSpeed = 3f; // 회전 속도 변수
        private Actor actor;

        private bool LeftisHolding = false;
        private float LeftholdTimer = 0f;

        private bool RightisHolding = false;
        private float RightholdTimer = 0f;

        private Vector2 input;

        private void Awake()
        {
            TryGetComponent(out actor);
        }

        private void FixedUpdate()
        {
            if (moveDirection != Vector3.zero)
            {
                if (actor.actorState == Actor.ActorState.Unconscious || actor.actorState == Actor.ActorState.Dead) return;

                // 카메라의 회전 방향을 실시간으로 가져와 이동 방향에 반영
                Vector3 forward = Camera.main.transform.forward;
                Vector3 right = Camera.main.transform.right;

                // Y축을 제외하여 평면 이동만 고려
                forward.y = 0f;
                right.y = 0f;

                // 카메라 회전 기준으로 이동 방향을 다시 설정
                moveDirection = (forward * input.y + right * input.x).normalized;

                // 이동 처리
                Vector3 move = moveDirection * moveSpeed * Time.fixedDeltaTime;

                // XZ 평면 이동 처리 (ForceMode.Force 사용)
                actor.bodyType.Chest.PartRigidbody.AddForce(move, ForceMode.VelocityChange);
                actor.bodyType.Hips.PartRigidbody.AddForce(move, ForceMode.VelocityChange);
                actor.bodyType.Ball.PartRigidbody.AddForce(move, ForceMode.VelocityChange);

                // 캐릭터 회전 처리
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

                // 이동 방향 업데이트
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
                    actor.movementHandeler.ArmPunching(MovementHandeler.Side.Left);
                }
                else
                {
                    actor.movementHandeler.ArmHolding(MovementHandeler.Side.Left);
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
                    actor.movementHandeler.ArmPunching(MovementHandeler.Side.Right);
                }
                else
                {
                    actor.movementHandeler.ArmHolding(MovementHandeler.Side.Right);
                }
            }

        }

        public void OnMove(InputAction.CallbackContext context)
        {
            input = context.ReadValue<Vector2>();

            if (input != Vector2.zero)
            {
                // 카메라의 forward와 right를 실시간으로 반영하여 이동 방향 계산
                Vector3 forward = Camera.main.transform.forward;
                Vector3 right = Camera.main.transform.right;

                // 카메라가 바라보는 방향에 따라 2D 평면 상에서의 이동 방향 계산
                forward.y = 0f;  // Y축 제거, 평면 이동만 고려
                right.y = 0f;

                // 카메라 방향을 기준으로 이동 방향 설정
                moveDirection = (forward * input.y + right * input.x).normalized;

                actor.actorState = Actor.ActorState.Run;
            }
            else
            {
                // 입력이 없을 때는 이동 멈춤
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
            if (context.phase == InputActionPhase.Started)
            {
                if (actor.isGround)
                {
                    actor.JumpCheck = actor.actorState;
                    actor.actorState = Actor.ActorState.Jump;
                    actor.flytime = 0.7f;
                    actor.isGround = false;
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
