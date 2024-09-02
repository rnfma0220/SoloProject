using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
        private CharacterController characterController;
        private Vector3 moveDirection;
        private float moveSpeed = 4f;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            bool hasControl = (moveDirection != Vector3.zero);

            if (hasControl)
            {
                transform.rotation = Quaternion.LookRotation(moveDirection);

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
        }
    }
}