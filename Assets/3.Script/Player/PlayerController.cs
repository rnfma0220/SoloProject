using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
        private CharacterController characterController;
        private Vector3 moveDirection;
        private float moveSpeed = 2f;
        private float rotationSpeed = 3f; // 회전 속도 변수
        private Actor actor;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            actor = GetComponent<Actor>();
        }

        private void Update()
        {
            bool hasControl = (moveDirection != Vector3.zero);

            if (hasControl)
            {
                actor.actorState = Actor.ActorState.Run;
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                actor.movementHandeler.direction = Vector3.Slerp(actor.movementHandeler.direction, moveDirection, rotationSpeed * Time.deltaTime);

                characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
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
                if(actor.isGround)
                {
                    actor.actorState = Actor.ActorState.Jump;
                }
            }
        }

        public void OnLeftHand(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                actor.LeftisHolding = true;
                actor.LeftholdTimer = 0f;
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                actor.LeftisHolding = false;
                actor.LeftholdTimer = 0f;
            }
        }

        public void OnRightHand(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                actor.RightisHolding = true;
                actor.RightholdTimer = 0f;
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                actor.RightisHolding = false;
                actor.RightholdTimer = 0f;
            }
        }
    }
}
