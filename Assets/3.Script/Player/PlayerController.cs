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
        private float rotationSpeed = 3f; // 회전 속도 변수
        private Actor actor;
        public Collider PunchGrabTarget;

        public bool LeftisHolding = false;
        public float LeftholdTimer = 0f;

        public bool RightisHolding = false;
        public float RightholdTimer = 0f;

        private void Awake()
        {
            TryGetComponent(out actor);
        }

        private void Update()
        {
            bool hasControl = (moveDirection != Vector3.zero);

            if (hasControl)
            {
                actor.actorState = Actor.ActorState.Run;

                // 이동 방향에 따른 회전
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                actor.movementHandeler.direction = Vector3.Slerp(actor.movementHandeler.direction, moveDirection, rotationSpeed * Time.deltaTime);

                // Transform을 사용한 이동
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
            }
        }

        private void LateUpdate()
        {
            if (LeftisHolding)
            {
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
            Vector2 input = context.ReadValue<Vector2>();

            if (input != Vector2.zero)
            {
                moveDirection = new Vector3(input.x, 0f, input.y);
                actor.actorState = Actor.ActorState.Run;
            }
            else
            {
                moveDirection = Vector3.zero;
                actor.actorState = Actor.ActorState.Stand;
            }
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                moveSpeed = moveSpeed * 1.5f;
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                moveSpeed = 2f;
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Canceled)
            {
                if (actor.isGround)
                {
                    actor.actorState = Actor.ActorState.Jump;
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
