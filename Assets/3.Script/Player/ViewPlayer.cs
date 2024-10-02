using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class ViewPlayer : MonoBehaviour
    {
        public BodyType bodyType;
        private float applyedForce = 1f;

        [SerializeField] private Vector3 direction;
        private Vector3 lookDirection;
        private Vector3 counterForce = new Vector3(0f, 4f, 0f);

        private void Start()
        {
            bodyType = GetComponent<BodyType>();
        }

        private void FixedUpdate()
        {
            lookDirection = direction;

            AlignToVector(bodyType.Head.PartRigidbody, bodyType.Head.PartTransform.forward, lookDirection, 0.1f, 2.5f * applyedForce);
            AlignToVector(bodyType.Head.PartRigidbody, bodyType.Head.PartTransform.up, Vector3.up, 0.1f, 2.5f * applyedForce);
            AlignToVector(bodyType.Chest.PartRigidbody, bodyType.Chest.PartTransform.forward, direction, 0.1f, 4f * applyedForce);
            AlignToVector(bodyType.Chest.PartRigidbody, bodyType.Chest.PartTransform.up, Vector3.up, 0.1f, 4f * applyedForce);
            AlignToVector(bodyType.Waist.PartRigidbody, bodyType.Waist.PartTransform.forward, direction, 0.1f, 4f * applyedForce);
            AlignToVector(bodyType.Waist.PartRigidbody, bodyType.Waist.PartTransform.up, Vector3.up, 0.1f, 4f * applyedForce);

            bodyType.Chest.PartRigidbody.AddForce(counterForce * applyedForce, ForceMode.VelocityChange);

            AlignToVector(bodyType.Hips.PartRigidbody, bodyType.Hips.PartTransform.up, Vector3.up, 0.1f, 3f * applyedForce);
            AlignToVector(bodyType.LeftThigh.PartRigidbody, bodyType.LeftThigh.PartTransform.up, Vector3.up, 0.1f, 3f * applyedForce);
            AlignToVector(bodyType.LeftLeg.PartRigidbody, bodyType.LeftLeg.PartTransform.up, Vector3.up, 0.1f, 3f * applyedForce);
            bodyType.LeftFoot.PartRigidbody.AddForce(-counterForce * applyedForce, ForceMode.VelocityChange);

            AlignToVector(bodyType.RightThigh.PartRigidbody, bodyType.RightThigh.PartTransform.up, Vector3.up, 0.1f, 3f * applyedForce);
            AlignToVector(bodyType.RightLeg.PartRigidbody, bodyType.RightLeg.PartTransform.up, Vector3.up, 0.1f, 3f * applyedForce);
            bodyType.RightFoot.PartRigidbody.AddForce(-counterForce * applyedForce, ForceMode.VelocityChange);

            bodyType.Ball.PartRigidbody.angularVelocity = Vector3.zero;
        }

        private void AlignToVector(Rigidbody part, Vector3 alignmentVector, Vector3 targetVector, float stability, float speed)
        {
            if (part == null)
            {
                return;
            }
            Vector3 vector = Vector3.Cross(Quaternion.AngleAxis(part.angularVelocity.magnitude * 57.29578f * stability / speed, part.angularVelocity) * alignmentVector, targetVector * 10f);
            if (!float.IsNaN(vector.x) && !float.IsNaN(vector.y) && !float.IsNaN(vector.z))
            {
                part.AddTorque(vector * speed * speed);
            }
        }
    }
}
