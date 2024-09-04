using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class MovementHandeler
    {
		public enum Side
		{
			Left = 0,
			Right = 1
		}

		public enum Pose
		{
			Forward = 0,
			Stand = 1,
			Behind = 2
		}

		public Transform localTransform;

        public Actor actor;

		public Vector3 direction;
		public Vector3 rawDirection;
		public Vector3 lookDirection;

		public float armCheeringForce = 10f;

        public bool stateChange = true;

		public virtual void Dead() { }

		public virtual void Unconscious() { }

		public virtual void Stand() { }

		public virtual void Run() { }

		public virtual void Jump() { }

		public void AlignToVector(Rigidbody part, Vector3 alignmentVector, Vector3 targetVector, float stability, float speed)
		{
			if (part == null)
			{
				return;
			}
			Vector3 vector = Vector3.Cross(Quaternion.AngleAxis(part.angularVelocity.magnitude * 57.29578f * stability / speed, part.angularVelocity) * alignmentVector, targetVector * 10f);
			if (!float.IsNaN(vector.x) && !float.IsNaN(vector.y) && !float.IsNaN(vector.z))
			{
				part.AddTorque(vector * speed * speed);

				if (actor.showForces)
				{
					Debug.DrawRay(part.position, alignmentVector * 0.2f, Color.red, 0f, false);
					Debug.DrawRay(part.position, targetVector * 0.2f, Color.green, 0f, false);
				}
			}
		}
	}
}
