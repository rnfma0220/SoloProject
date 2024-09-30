using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class MovementHandeler_humanoid : MovementHandeler
    {
        private Vector3 counterForce = new Vector3(0f, 4f, 0f);

        private Vector3 localCounterForce = new Vector3(0f, 4f, 1f);

        public float cycleTimer;

        public float cycleSpeed = 0.23f;

        private float DownTimer = 10f;

        private bool Walk = false;
        private float WalkTime;

        public override void Stand()
        {
            lookDirection = direction + new Vector3(0f, 0.1f, 0f);

            if (!Sit)
            {
                AlignToVector(actor.bodyType.Head.PartRigidbody, actor.bodyType.Head.PartTransform.forward, lookDirection, 0.1f, 2.5f * actor.applyedForce);
                AlignToVector(actor.bodyType.Head.PartRigidbody, actor.bodyType.Head.PartTransform.up, Vector3.up, 0.1f, 2.5f * actor.applyedForce);
                AlignToVector(actor.bodyType.Chest.PartRigidbody, actor.bodyType.Chest.PartTransform.forward, direction, 0.1f, 4f * actor.applyedForce);
                AlignToVector(actor.bodyType.Chest.PartRigidbody, actor.bodyType.Chest.PartTransform.up, Vector3.up, 0.1f, 4f * actor.applyedForce);
                AlignToVector(actor.bodyType.Waist.PartRigidbody, actor.bodyType.Waist.PartTransform.forward, direction, 0.1f, 4f * actor.applyedForce);
                AlignToVector(actor.bodyType.Waist.PartRigidbody, actor.bodyType.Waist.PartTransform.up, Vector3.up, 0.1f, 4f * actor.applyedForce);

                actor.bodyType.Chest.PartRigidbody.AddForce(counterForce * actor.applyedForce, ForceMode.VelocityChange);
            }
            else
            {
                actor.bodyType.Hips.PartRigidbody.AddForce(counterForce * actor.applyedForce, ForceMode.VelocityChange);
            }

            AlignToVector(actor.bodyType.Hips.PartRigidbody, actor.bodyType.Hips.PartTransform.up, Vector3.up, 0.1f, 3f * actor.applyedForce);
            AlignToVector(actor.bodyType.LeftThigh.PartRigidbody, actor.bodyType.LeftThigh.PartTransform.up, Vector3.up, 0.1f, 3f * actor.applyedForce);
            AlignToVector(actor.bodyType.LeftLeg.PartRigidbody, actor.bodyType.LeftLeg.PartTransform.up, Vector3.up, 0.1f, 3f * actor.applyedForce);
            actor.bodyType.LeftFoot.PartRigidbody.AddForce(-counterForce * actor.applyedForce, ForceMode.VelocityChange);

            AlignToVector(actor.bodyType.RightThigh.PartRigidbody, actor.bodyType.RightThigh.PartTransform.up, Vector3.up, 0.1f, 3f * actor.applyedForce);
            AlignToVector(actor.bodyType.RightLeg.PartRigidbody, actor.bodyType.RightLeg.PartTransform.up, Vector3.up, 0.1f, 3f * actor.applyedForce);
            actor.bodyType.RightFoot.PartRigidbody.AddForce(-counterForce * actor.applyedForce, ForceMode.VelocityChange);

            actor.bodyType.Ball.PartRigidbody.angularVelocity = Vector3.zero;
        }

        public override void Run()
        {
            lookDirection = direction + new Vector3(0f, 0.1f, 0f);

            WalkTime -= Time.deltaTime;

            if(!Sit)
            {
                AlignToVector(actor.bodyType.Head.PartRigidbody, actor.bodyType.Head.PartTransform.forward, lookDirection, 0.1f, 2.5f * actor.applyedForce);
                AlignToVector(actor.bodyType.Head.PartRigidbody, actor.bodyType.Head.PartTransform.up, Vector3.up, 0.1f, 2.5f * actor.applyedForce);
                AlignToVector(actor.bodyType.Chest.PartRigidbody, actor.bodyType.Chest.PartTransform.forward, direction, 0.1f, 4f * actor.applyedForce);
                AlignToVector(actor.bodyType.Chest.PartRigidbody, actor.bodyType.Chest.PartTransform.up, Vector3.up, 0.1f, 4f * actor.applyedForce);
                AlignToVector(actor.bodyType.Waist.PartRigidbody, actor.bodyType.Waist.PartTransform.forward, direction, 0.1f, 4f * actor.applyedForce);
                AlignToVector(actor.bodyType.Waist.PartRigidbody, actor.bodyType.Waist.PartTransform.up, Vector3.up, 0.1f, 4f * actor.applyedForce);

                actor.bodyType.Chest.PartRigidbody.AddForce(counterForce * actor.applyedForce, ForceMode.VelocityChange);
            }
            else
            {
                actor.bodyType.Hips.PartRigidbody.AddForce(counterForce * actor.applyedForce, ForceMode.VelocityChange);
            }
            AlignToVector(actor.bodyType.Head.PartRigidbody, actor.bodyType.Head.PartTransform.forward, lookDirection, 0.1f, 2.5f * actor.applyedForce);
            AlignToVector(actor.bodyType.Head.PartRigidbody, actor.bodyType.Head.PartTransform.up, Vector3.up, 0.1f, 2.5f * actor.applyedForce);

            AlignToVector(actor.bodyType.Hips.PartRigidbody, actor.bodyType.Hips.PartTransform.up, Vector3.up, 0.1f, 3f * actor.applyedForce);

            AlignToVector(actor.bodyType.LeftThigh.PartRigidbody, -actor.bodyType.LeftThigh.PartTransform.up, -Vector3.up, 0.1f, 3f * actor.applyedForce);
            AlignToVector(actor.bodyType.LeftLeg.PartRigidbody, -actor.bodyType.LeftLeg.PartTransform.up, -Vector3.up, 0.1f, 3f * actor.applyedForce);

            AlignToVector(actor.bodyType.RightThigh.PartRigidbody, -actor.bodyType.RightThigh.PartTransform.up, -Vector3.up, 0.1f, 3f * actor.applyedForce);
            AlignToVector(actor.bodyType.RightLeg.PartRigidbody, -actor.bodyType.RightLeg.PartTransform.up, -Vector3.up, 0.1f, 3f * actor.applyedForce);

            actor.bodyType.Ball.PartRigidbody.AddForce(-counterForce * actor.applyedForce, ForceMode.VelocityChange);

            if (!Walk)
            {
                if (WalkTime <= 0f) 
                {
                    actor.bodyType.RightThigh.PartRigidbody.AddForce(direction * 5f, ForceMode.VelocityChange);
                    actor.bodyType.RightLeg.PartRigidbody.AddForce(direction * 10f, ForceMode.VelocityChange);
                    actor.bodyType.RightFoot.PartRigidbody.AddForce(direction * 20f, ForceMode.VelocityChange);

                    actor.bodyType.LeftThigh.PartRigidbody.AddForce(-direction * 1f, ForceMode.VelocityChange);
                    actor.bodyType.LeftLeg.PartRigidbody.AddForce(-direction * 1f, ForceMode.VelocityChange);
                    actor.bodyType.LeftFoot.PartRigidbody.AddForce(-direction * 3f, ForceMode.VelocityChange);

                    WalkTime = 0.35f;
                    Walk = true;
                }
            }
            else
            {
                if (WalkTime <= 0f)
                {
                    actor.bodyType.LeftThigh.PartRigidbody.AddForce(direction * 5f, ForceMode.VelocityChange);
                    actor.bodyType.LeftLeg.PartRigidbody.AddForce(direction * 10f, ForceMode.VelocityChange);
                    actor.bodyType.LeftFoot.PartRigidbody.AddForce(direction * 20f, ForceMode.VelocityChange);

                    actor.bodyType.RightThigh.PartRigidbody.AddForce(-direction * 1f, ForceMode.VelocityChange);
                    actor.bodyType.RightLeg.PartRigidbody.AddForce(-direction * 1f, ForceMode.VelocityChange);
                    actor.bodyType.RightFoot.PartRigidbody.AddForce(-direction * 3f, ForceMode.VelocityChange);

                    WalkTime = 0.35f;
                    Walk = false;
                }
            }
        }

        public override void Jump()
        {
            AlignToVector(actor.bodyType.Head.PartRigidbody, actor.bodyType.Head.PartTransform.forward, lookDirection, 0.1f, 2.5f * actor.applyedForce);
            AlignToVector(actor.bodyType.Head.PartRigidbody, actor.bodyType.Head.PartTransform.up, Vector3.up, 0.1f, 2.5f * actor.applyedForce);
            AlignToVector(actor.bodyType.Chest.PartRigidbody, actor.bodyType.Chest.PartTransform.forward, direction, 0.1f, 4f * actor.applyedForce);
            AlignToVector(actor.bodyType.Chest.PartRigidbody, actor.bodyType.Chest.PartTransform.up, Vector3.up, 0.1f, 4f * actor.applyedForce);
            AlignToVector(actor.bodyType.Waist.PartRigidbody, actor.bodyType.Waist.PartTransform.forward, direction, 0.1f, 4f * actor.applyedForce);
            AlignToVector(actor.bodyType.Waist.PartRigidbody, actor.bodyType.Waist.PartTransform.up, Vector3.up, 0.1f, 4f * actor.applyedForce);
            AlignToVector(actor.bodyType.Hips.PartRigidbody, actor.bodyType.Hips.PartTransform.up, Vector3.up, 0.1f, 3f * actor.applyedForce);

            AlignToVector(actor.bodyType.LeftThigh.PartRigidbody, actor.bodyType.LeftThigh.PartTransform.up, Vector3.up, 0.1f, 3f * actor.applyedForce);
            AlignToVector(actor.bodyType.LeftLeg.PartRigidbody, actor.bodyType.LeftLeg.PartTransform.up, Vector3.up, 0.1f, 3f * actor.applyedForce);

            AlignToVector(actor.bodyType.RightThigh.PartRigidbody, actor.bodyType.RightThigh.PartTransform.up, Vector3.up, 0.1f, 3f * actor.applyedForce);
            AlignToVector(actor.bodyType.RightLeg.PartRigidbody, actor.bodyType.RightLeg.PartTransform.up, Vector3.up, 0.1f, 3f * actor.applyedForce);

            if (stateChange)
            {
                actor.bodyType.Chest.PartRigidbody.AddForce(Vector3.up * 50f, ForceMode.VelocityChange);
                actor.bodyType.Waist.PartRigidbody.AddForce(Vector3.up * 40f, ForceMode.VelocityChange);

                if (Sit) Sit = false;
            }
        }

        public override void Dead()
        {
            actor.bodyType.MeshHead.PartRigidbody.gameObject.SetActive(false); // 캐릭터 사망시 캐릭터 렌더 지워서 안보이는거처럼 변경
            actor.bodyType.MeshBody.PartRigidbody.gameObject.SetActive(false);
        }

        public override void Unconscious()
        {
            DownTimer -= Time.deltaTime;

            if (DownTimer <= 0)
            {
                WakeUP();
                actor.actorState = Actor.ActorState.Stand;
                actor.PlayerDownCount++;
                actor.PlayerHP = 100f;
                DownTimer = 5f;
            }
        }

        private void WakeUP()
        {
            actor.bodyType.Head.PartRigidbody.mass = actor.bodyType.HeadMass;
            actor.bodyType.Chest.PartRigidbody.mass = actor.bodyType.ChestMass;
            actor.bodyType.Waist.PartRigidbody.mass = actor.bodyType.WaistMass;
            actor.bodyType.Stomach.PartRigidbody.mass = actor.bodyType.StomachMass;
            actor.bodyType.Hips.PartRigidbody.mass = actor.bodyType.HipsMass;
            actor.bodyType.Crotch.PartRigidbody.mass = actor.bodyType.CrotchMass;
            actor.bodyType.LeftForarm.PartRigidbody.mass = actor.bodyType.LeftForarmMass;
            actor.bodyType.LeftHand.PartRigidbody.mass = actor.bodyType.LeftHandMass;
            actor.bodyType.LeftThigh.PartRigidbody.mass = actor.bodyType.LeftThighMass;
            actor.bodyType.LeftLeg.PartRigidbody.mass = actor.bodyType.LeftLegMass;
            actor.bodyType.LeftFoot.PartRigidbody.mass = actor.bodyType.LeftFootMass;
            actor.bodyType.RightArm.PartRigidbody.mass = actor.bodyType.RightArmMass;
            actor.bodyType.RightForarm.PartRigidbody.mass = actor.bodyType.RightForarmMass;
            actor.bodyType.RightHand.PartRigidbody.mass = actor.bodyType.RightHandMass;
            actor.bodyType.RightThigh.PartRigidbody.mass = actor.bodyType.RightThighMass;
            actor.bodyType.RightLeg.PartRigidbody.mass = actor.bodyType.RightFootMass;
            actor.bodyType.RightFoot.PartRigidbody.mass = actor.bodyType.RightFootMass;
            actor.bodyType.Ball.PartRigidbody.mass = actor.bodyType.BallMass;
            actor.bodyType.Spring.PartRigidbody.mass = actor.bodyType.SpringMass;
        }

        public override void ArmReadying(Side side)
        {
            Transform partTransform = actor.bodyType.Chest.PartTransform;
            Transform transform = null;
            Transform transform2 = null;
            Rigidbody part = null;
            Rigidbody part2 = null;
            switch (side)
            {
                case Side.Left:
                    transform = actor.bodyType.LeftArm.PartTransform;
                    transform2 = actor.bodyType.LeftForarm.PartTransform;
                    part = actor.bodyType.LeftArm.PartRigidbody;
                    part2 = actor.bodyType.LeftForarm.PartRigidbody;
                    break;
                case Side.Right:
                    transform = actor.bodyType.RightArm.PartTransform;
                    transform2 = actor.bodyType.RightForarm.PartTransform;
                    part = actor.bodyType.RightArm.PartRigidbody;
                    part2 = actor.bodyType.RightForarm.PartRigidbody;
                    break;
            }
            AlignToVector(part, transform.up, partTransform.forward + -partTransform.up, 0.01f, 10f);
            AlignToVector(part2, transform2.up, -partTransform.forward + -partTransform.up, 0.01f, 10f);
        }

        public override void HandUp()
        {
            Vector3 forceDirection = actor.bodyType.Chest.PartTransform.TransformDirection(localCounterForce);

            // 변환된 월드 좌표 방향으로 팔에 힘을 가함
            actor.bodyType.LeftForarm.PartRigidbody.AddForce(forceDirection * 50f, ForceMode.Force);
            actor.bodyType.LeftHand.PartRigidbody.AddForce(forceDirection * 50f, ForceMode.Force);
            actor.bodyType.RightForarm.PartRigidbody.AddForce(forceDirection * 50f, ForceMode.Force);
            actor.bodyType.RightHand.PartRigidbody.AddForce(forceDirection * 50f, ForceMode.Force);
        }

        public override void ArmPunching(Side side)
        {
            Rigidbody rigidbody = null;
            Rigidbody rigidbody2 = null;
            switch (side)
            {
                case Side.Left:
                    rigidbody = actor.bodyType.LeftArm.PartRigidbody;
                    rigidbody2 = actor.bodyType.LeftHand.PartRigidbody;
                    break;

                case Side.Right:
                    rigidbody = actor.bodyType.RightArm.PartRigidbody;
                    rigidbody2 = actor.bodyType.RightHand.PartRigidbody;
                    break;
            }

            Vector3 zero = Vector3.zero;

            zero = Camera.main.transform.forward; // 카메라의 중앙으로 펀치기점을 잡기위함
            rigidbody.AddForce(-(zero * 6f) * actor.inputSpamForceModifier, ForceMode.VelocityChange);
            rigidbody2.AddForce(zero * 6f * actor.inputSpamForceModifier, ForceMode.VelocityChange);
        }

        public override void ArmHolding(Side side)
        {
            Transform transform = null;
            Transform transform2 = null;
            Rigidbody part = null;
            Rigidbody part2 = null;

            switch (side)
            {
                case Side.Left:
                    transform = actor.bodyType.LeftArm.PartTransform;
                    transform2 = actor.bodyType.LeftForarm.PartTransform;
                    part = actor.bodyType.LeftArm.PartRigidbody;
                    part2 = actor.bodyType.LeftForarm.PartRigidbody;
                    break;
                case Side.Right:
                    transform = actor.bodyType.RightArm.PartTransform;
                    transform2 = actor.bodyType.RightForarm.PartTransform;
                    part = actor.bodyType.RightArm.PartRigidbody;
                    part2 = actor.bodyType.RightForarm.PartRigidbody;
                    break;
            }
            Vector3 zero = Vector3.zero;

            zero = Camera.main.transform.forward; // 카메라의 중앙으로 펀치기점을 잡기위함
            AlignToVector(part, transform.up, -zero, 0.01f, 10f);
            AlignToVector(part2, transform2.up, -zero, 0.01f, 10f);
        }

        public override void ArmHolding(Side side, GameObject target)
        {
            Transform transform = null;
            Transform transform2 = null;
            Transform Hand = null;
            Rigidbody part = null;
            Rigidbody part2 = null;
            FixedJoint HandJoint;

            switch (side)
            {
                case Side.Left:
                    transform = actor.bodyType.LeftArm.PartTransform;
                    transform2 = actor.bodyType.LeftForarm.PartTransform;
                    Hand = actor.bodyType.LeftHand.PartTransform;
                    part = actor.bodyType.LeftArm.PartRigidbody;
                    part2 = actor.bodyType.LeftForarm.PartRigidbody;

                    if (!Hand.gameObject.GetComponent<FixedJoint>())
                    {
                        HandJoint = Hand.gameObject.AddComponent<FixedJoint>();
                        HandJoint.connectedBody = target.GetComponent<Rigidbody>();
                        HandJoint.breakForce = 30000f;
                        HandJoint.breakTorque = 30000f;
                        HandJoint.enableCollision = false;
                        HandJoint.enablePreprocessing = true;
                    }
                    break;

                case Side.Right:
                    transform = actor.bodyType.RightArm.PartTransform;
                    transform2 = actor.bodyType.RightForarm.PartTransform;
                    Hand = actor.bodyType.RightHand.PartTransform;
                    part = actor.bodyType.RightArm.PartRigidbody;
                    part2 = actor.bodyType.RightForarm.PartRigidbody;

                    if (!Hand.gameObject.GetComponent<FixedJoint>())
                    {
                        HandJoint = Hand.gameObject.AddComponent<FixedJoint>();

                        HandJoint.connectedBody = target.GetComponent<Rigidbody>();
                        HandJoint.breakForce = 30000f;
                        HandJoint.breakTorque = 30000f;
                        HandJoint.enableCollision = false;
                        HandJoint.enablePreprocessing = true;
                    }
                    break;
            }
            Vector3 zero = Vector3.zero;

            zero = Camera.main.transform.forward; // 카메라의 중앙으로 펀치기점을 잡기위함
            AlignToVector(part, transform.up, -zero, 0.01f, 10f);
            AlignToVector(part2, transform2.up, -zero, 0.01f, 10f);
        }
    }
}
