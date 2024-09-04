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
            Dead = 0, // ���
            Unconscious = 1, // ����
            Stand = 2, // ���ִ»���
            Run = 3, // �����̴���
            Jump = 4, // ������
        }

        public ActorState actorState;

        public ActorState lastActorState;

        public MovementHandeler movementHandeler;

        public PlayerController player;

        public float LeftholdThreshold = 0.5f;
        public bool LeftisHolding = false;
        public float LeftholdTimer = 0f;

        public float RightholdThreshold = 0.5f;
        public bool RightisHolding = false;
        public float RightholdTimer = 0f;

        public bool showForces = true; // ����� ���̿�

        public float applyedForce = 1f;

        public bool isGround = true;

        public float inputSpamForceModifier = 1f;

        private void Start()
        {
            bodyType = GetComponent<BodyType>();
            player = GetComponent<PlayerController>();

            movementHandeler = new MovementHandeler_humanoid();
            movementHandeler.actor = this;
            movementHandeler.direction = bodyType.Chest.PartTransform.forward;
            movementHandeler.lookDirection = movementHandeler.direction + new Vector3(0f, 0.1f, 0f);
        }

        private void FixedUpdate()
        {
            if (LeftisHolding)
            {
                LeftholdTimer += Time.deltaTime;

                if (LeftholdTimer >= LeftholdThreshold)
                {
                    Debug.Log("�޼� ���");
                }
                else
                {
                    Debug.Log("�޼� ��ġ�߻�");
                }
            }

            if (RightisHolding)
            {
                RightholdTimer += Time.deltaTime;

                if(RightholdTimer >= RightholdThreshold)
                {
                    Debug.Log("������ ���");
                }
                else
                {
                    Debug.Log("������ ��ġ�߻�");
                }
            }
            IsGroundedCustom();
            UpdateState();
        }

        public void IsGroundedCustom() // �����Ŀ� �ٴڿ� ��´ٸ� ���ִ� ���·� �ٲٱ� ����
        {
            if(actorState != ActorState.Jump) return; 

            RaycastHit hit;
            float rayDistance = 0.1f;
            if (Physics.Raycast(bodyType.LeftFoot.PartTransform.position, Vector3.down, out hit, rayDistance))
            {
                if(hit.transform.CompareTag("Ground"))
                {
                    actorState = ActorState.Stand;
                    isGround = true;
                }
            }
        }

        private void LeftPunch()
        {

        }

        private void RightPunch()
        {

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