using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class Actor : MonoBehaviour
    {
        [HideInInspector]
        public BodyType bodyType;

        public enum ActorState
        {
            Dead = 0, // 사망
            Unconscious = 1, // 기절
            Stand = 2, // 서있는상태
            Run = 3, // 움직이는중
            Jump = 4, // 점프중
        }

        public ActorState actorState;

        public ActorState lastActorState;

        public float PlayerHP = 100f;

        public int PlayerDownCount = 0;

        [HideInInspector]
        public ActorState JumpCheck;

        public bool LeftAttack = false;
        public bool RightAttack = false;

        public MovementHandeler movementHandeler;

        [HideInInspector]
        public PlayerController player;

        public bool showForces = true; // 디버그 레이용

        [HideInInspector]
        public float applyedForce = 1f;

        [HideInInspector]
        public bool isGround = true;

        [HideInInspector]
        public float inputSpamForceModifier = 1f;


        private void Start()
        {
            bodyType = GetComponent<BodyType>();
            player = GetComponent<PlayerController>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            movementHandeler = new MovementHandeler_humanoid();
            movementHandeler.actor = this;
            movementHandeler.direction = bodyType.Chest.PartTransform.forward;
            movementHandeler.lookDirection = movementHandeler.direction + new Vector3(0f, 0.1f, 0f);
        }

        private void FixedUpdate()
        {
            UpdateState();
        }

        public void IsGroundedCheck() // 캐릭터가 바닥에있는지 체크
        {
            isGround = true;
            if(actorState != ActorState.Stand)
            {
                actorState = JumpCheck;
            }
        }

        private void UpdateState()
        {
            if (actorState != lastActorState)
            {
                movementHandeler.stateChange = true;
            }
            else
            {
                movementHandeler.stateChange = false;
            }

            switch (actorState)
            {
                case ActorState.Dead:
                    movementHandeler.Dead();
                    break;
                case ActorState.Unconscious:
                    movementHandeler.Unconscious();
                    break;
                case ActorState.Stand:
                    movementHandeler.Stand();
                    break;
                case ActorState.Run:
                    movementHandeler.Run();
                    break;
                case ActorState.Jump:
                    movementHandeler.Jump();
                    break;
            }

            lastActorState = actorState;

            if (actorState == ActorState.Dead || actorState == ActorState.Unconscious)
            {
                applyedForce = 0.1f;
                inputSpamForceModifier = 1f;
            }

            if (actorState == ActorState.Jump)
            {
                applyedForce = 0.5f;
                return;
            }

            applyedForce = Mathf.Clamp(applyedForce + Time.deltaTime / 2f, 0.01f, 1f);
            inputSpamForceModifier = Mathf.Clamp(inputSpamForceModifier + Time.deltaTime / 2f, 0.01f, 1f);
        }

    }
}
