using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class MovementHandeler_humanoid : MovementHandeler
    {
        private Vector3 counterForce = new Vector3(0f, 4f, 0f);
        private Vector3 test = new Vector3(0f, 0f, 10f);
        private float headSwingSpeed = 1f; // 머리 움직임 속도
        private float headSwingAngle = 3f; // 머리 움직임 각도

        public Pose LeftLeg = Pose.Forward;
        public Pose RightLeg = Pose.Behind;

        public float cycleTimer;

        public float cycleSpeed = 0.23f;

        public override void Stand()
        {
            float swing = Mathf.Sin(Time.time * headSwingSpeed) * headSwingAngle;
            lookDirection = direction + new Vector3(0f, 0.1f, 0f);

            AlignToVector(actor.bodyType.Head.PartRigidbody, actor.bodyType.Head.PartTransform.forward, lookDirection, 0.1f, 2.5f * actor.applyedForce);
            AlignToVector(actor.bodyType.Head.PartRigidbody, actor.bodyType.Head.PartTransform.up, Vector3.up, 0.1f, 2.5f * actor.applyedForce);
            AlignToVector(actor.bodyType.Chest.PartRigidbody, actor.bodyType.Chest.PartTransform.forward, direction, 0.1f, 4f * actor.applyedForce);
            AlignToVector(actor.bodyType.Chest.PartRigidbody, actor.bodyType.Chest.PartTransform.up, Vector3.up, 0.1f, 4f * actor.applyedForce);
            AlignToVector(actor.bodyType.Waist.PartRigidbody, actor.bodyType.Waist.PartTransform.forward, direction, 0.1f, 4f * actor.applyedForce);
            AlignToVector(actor.bodyType.Waist.PartRigidbody, actor.bodyType.Waist.PartTransform.up, Vector3.up, 0.1f, 4f * actor.applyedForce);
            AlignToVector(actor.bodyType.Hips.PartRigidbody, actor.bodyType.Hips.PartTransform.up, Vector3.up, 0.1f, 3f * actor.applyedForce);

            AlignToVector(actor.bodyType.LeftThigh.PartRigidbody, actor.bodyType.LeftThigh.PartTransform.up, Vector3.up, 0.1f, 3f * actor.applyedForce);
            AlignToVector(actor.bodyType.LeftLeg.PartRigidbody, actor.bodyType.LeftLeg.PartTransform.up, Vector3.up, 0.1f, 3f * actor.applyedForce);
            actor.bodyType.LeftFoot.PartRigidbody.AddForce(-counterForce * actor.applyedForce, ForceMode.VelocityChange);

            AlignToVector(actor.bodyType.RightThigh.PartRigidbody, actor.bodyType.RightThigh.PartTransform.up, Vector3.up, 0.1f, 3f * actor.applyedForce);
            AlignToVector(actor.bodyType.RightLeg.PartRigidbody, actor.bodyType.RightLeg.PartTransform.up, Vector3.up, 0.1f, 3f * actor.applyedForce);
            actor.bodyType.RightFoot.PartRigidbody.AddForce(-counterForce * actor.applyedForce, ForceMode.VelocityChange);

            actor.bodyType.Chest.PartRigidbody.AddForce(counterForce * actor.applyedForce, ForceMode.VelocityChange);

            actor.bodyType.Ball.PartRigidbody.angularVelocity = Vector3.zero;
        }

        public override void Run()
        {
            lookDirection = direction + new Vector3(0f, 0.1f, 0f);

            AlignToVector(actor.bodyType.Head.PartRigidbody, actor.bodyType.Head.PartTransform.forward, lookDirection, 0.1f, 2.5f * actor.applyedForce);
            AlignToVector(actor.bodyType.Head.PartRigidbody, actor.bodyType.Head.PartTransform.up, Vector3.up, 0.1f, 2.5f * actor.applyedForce);
            AlignToVector(actor.bodyType.Chest.PartRigidbody, actor.bodyType.Chest.PartTransform.forward, direction, 0.1f, 4f * actor.applyedForce);
            AlignToVector(actor.bodyType.Chest.PartRigidbody, actor.bodyType.Chest.PartTransform.up, Vector3.up, 0.1f, 4f * actor.applyedForce);
            AlignToVector(actor.bodyType.Waist.PartRigidbody, actor.bodyType.Waist.PartTransform.forward, direction, 0.1f, 4f * actor.applyedForce);
            AlignToVector(actor.bodyType.Waist.PartRigidbody, actor.bodyType.Waist.PartTransform.up, Vector3.up, 0.1f, 4f * actor.applyedForce);
            AlignToVector(actor.bodyType.Hips.PartRigidbody, actor.bodyType.Hips.PartTransform.up, Vector3.up, 0.1f, 3f * actor.applyedForce);

            //발 움직이는 부분을 넣어야하는데 방법을 모르겠다 개빡췬다@#@#23


            actor.bodyType.Chest.PartRigidbody.AddForce(counterForce * actor.applyedForce, ForceMode.VelocityChange);
        }

        public override void Jump()
        {
            if (stateChange)
            {
                // 점프는 또 어떻게해아할까~!~!~!
            }
        }

        public override void Dead()
        {

        }

        public override void Unconscious()
        {

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

        public override void ArmPunching(Side side, Collider punchTarget)
        {
            Transform partTransform = actor.bodyType.Chest.PartTransform;
            Transform transform = null;
            Transform transform2 = null;
            Rigidbody rigidbody = null;
            Rigidbody rigidbody2 = null;
            switch (side)
            {
                case Side.Left:
                    transform = actor.bodyType.LeftArm.PartTransform;
                    transform2 = actor.bodyType.LeftHand.PartTransform;
                    rigidbody = actor.bodyType.LeftArm.PartRigidbody;
                    rigidbody2 = actor.bodyType.LeftHand.PartRigidbody;
                    break;

                case Side.Right:
                    transform = actor.bodyType.RightArm.PartTransform;
                    transform2 = actor.bodyType.RightHand.PartTransform;
                    rigidbody = actor.bodyType.RightArm.PartRigidbody;
                    rigidbody2 = actor.bodyType.RightHand.PartRigidbody;
                    break;
            }
            transform2.tag = "Body (Harmful)";
            Vector3 zero = Vector3.zero;
            if (punchTarget == null)
            {
                zero = Vector3.Normalize(partTransform.position + partTransform.forward + partTransform.up / 2f - transform2.position);
                rigidbody.AddForce(-(zero * 3f) * actor.inputSpamForceModifier, ForceMode.VelocityChange);
                rigidbody2.AddForce(zero * 3f * actor.inputSpamForceModifier, ForceMode.VelocityChange);
                actor.bodyType.Hips.PartRigidbody.constraints = RigidbodyConstraints.None;
                return;
            }
            zero = Vector3.Normalize(punchTarget.bounds.center - transform2.position);
            if (Vector3.Distance(punchTarget.bounds.center, transform.position) > 1f)
            {
                rigidbody.AddForce(-(zero * 3f) * actor.inputSpamForceModifier, ForceMode.VelocityChange);
                rigidbody2.AddForce(zero * 3f * actor.inputSpamForceModifier, ForceMode.VelocityChange);
            }
            else
            {
                rigidbody.AddForce(-(zero * 3f) * actor.inputSpamForceModifier, ForceMode.VelocityChange);
                rigidbody2.AddForce(zero * 3f * actor.inputSpamForceModifier, ForceMode.VelocityChange);
            }
        }
    }
}
