using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace Character
{
    public class Actor : NetworkBehaviour
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

        private bool test;

        [HideInInspector]
        public PlayerController player;

        public bool showForces = true; // 디버그 레이용

        [HideInInspector]
        public float applyedForce { get; private set; }

        [HideInInspector]
        public bool isGround = true;

        [HideInInspector]
        public float inputSpamForceModifier { get; private set; }


        private void Start()
        {
            bodyType = GetComponent<BodyType>();
            player = GetComponent<PlayerController>();

            applyedForce = 1f;
            inputSpamForceModifier = 1f;

            movementHandeler = new MovementHandeler_humanoid();
            movementHandeler.actor = this;
            movementHandeler.direction = bodyType.Chest.PartTransform.forward;
            movementHandeler.lookDirection = movementHandeler.direction + new Vector3(0f, 0.1f, 0f);
        }

        private void FixedUpdate()
        {
            UpdateState();
            UnconsciousCheck();
        }

        private void UnconsciousCheck()
        {
            if (PlayerHP <= 0 && actorState != ActorState.Dead && actorState != ActorState.Unconscious)
            {
                if(PlayerDownCount == 2)
                {
                    actorState = ActorState.Dead;
                }
                else
                {
                    actorState = ActorState.Unconscious;
                    UnconsciousMass();
                    Debug.Log("호출카운");
                }
            }
        }

        private void UnconsciousMass()
        {
            bodyType.Head.PartRigidbody.mass = 1f;
            bodyType.Chest.PartRigidbody.mass = 1f;
            bodyType.Waist.PartRigidbody.mass = 1f;
            bodyType.Stomach.PartRigidbody.mass = 1f;
            bodyType.Hips.PartRigidbody.mass = 1f;
            bodyType.Crotch.PartRigidbody.mass = 1f;
            bodyType.LeftArm.PartRigidbody.mass = 1f;
            bodyType.LeftForarm.PartRigidbody.mass = 1f;
            bodyType.LeftHand.PartRigidbody.mass = 1f;
            bodyType.LeftThigh.PartRigidbody.mass = 1f;
            bodyType.LeftLeg.PartRigidbody.mass = 1f;
            bodyType.LeftFoot.PartRigidbody.mass = 1f;
            bodyType.RightArm.PartRigidbody.mass = 1f;
            bodyType.RightForarm.PartRigidbody.mass = 1f;
            bodyType.RightHand.PartRigidbody.mass = 1f;
            bodyType.RightThigh.PartRigidbody.mass = 1f;
            bodyType.RightLeg.PartRigidbody.mass = 1f;
            bodyType.RightFoot.PartRigidbody.mass = 1f;
            bodyType.Ball.PartRigidbody.mass = 1f;
            bodyType.Spring.PartRigidbody.mass = 1f;
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
