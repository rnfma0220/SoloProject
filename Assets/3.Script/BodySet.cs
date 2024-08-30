using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class BodySet : MonoBehaviour
    {
        public Transform PartTransform;

        public Rigidbody PartRigidbody;

		public Collider PartCollider;

		public Renderer PartRenderer;

		public BodySet(string transformName, Transform parent)
		{
			//Transform transform = BodyType.FindTransformViaName(parent.parent, transformName);
			if (transform == null)
			{
				Debug.LogErrorFormat("[BodyPart] Couldn't find transform: {0}", transformName);
			}
			else
			{
				Setup(transform, parent);
			}
		}

		public BodySet(Transform transform, Transform parent)
		{
			Setup(transform, parent);
		}

		public void UnregisterCallbacks()
		{
			//BodyType.OnToggleRenderers -= ToggleRenderer;
		}

		public void ToggleRenderer()
		{
			if (PartRenderer != null)
			{
				PartRenderer.enabled = !PartRenderer.enabled;
			}
		}

		private void Setup(Transform transform, Transform parent)
		{
			PartTransform = transform;
			if (!(PartTransform == null))
			{
				if (PartTransform.GetComponent<Joint>() != null)
				{
					PartTransform.parent = parent;
				}
				PartRigidbody = PartTransform.GetComponent<Rigidbody>();
				if (PartRigidbody != null)
				{
					PartRigidbody.maxAngularVelocity = 20f;
					PartRigidbody.drag = 1f;
					PartRigidbody.angularDrag = 1f;
				}
				PartCollider = PartTransform.GetComponent<Collider>();
				//PartCollisionHandeler = PartTransform.GetComponent<CollisionHandeler>();
				PartRenderer = PartTransform.GetComponent<Renderer>();
				if (PartRenderer != null)
				{
					//BodyHandeler.OnToggleRenderers += ToggleRenderer;
				}
			}
		}
	}
}
