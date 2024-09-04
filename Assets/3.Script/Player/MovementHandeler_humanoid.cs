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
                
            }
        }
    }
}
