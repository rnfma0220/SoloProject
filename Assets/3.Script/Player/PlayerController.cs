using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
        private CharacterController characterController;
        private Vector3 moveDirection;
        private float moveSpeed = 4f;
        private float rotationSpeed = 4f; // 회전 속도 변수
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

        private void OnMove(InputValue value)
        {
            Vector2 input = value.Get<Vector2>();
            if (input != Vector2.zero)
            {
                moveDirection = new Vector3(input.x, 0f, input.y);
            }
            else
            {
                moveDirection = Vector3.zero;
            }

            actor.actorState = Actor.ActorState.Stand;
        }
    }
}
