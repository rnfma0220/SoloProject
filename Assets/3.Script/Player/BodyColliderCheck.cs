using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Character
{
    public class BodyColliderCheck : MonoBehaviour
    {
        private Actor actor;

        private void Start()
        {
            actor = GetComponentInParent<Actor>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                if (gameObject.name == "actor_leftThigh_collider" || gameObject.name == "actor_leftLeg_collider" || gameObject.name == "actor_rightThigh_collider" || gameObject.name == "actor_rightLeg_collider")
                {
                    if (!actor.isGround)
                    {
                        actor.IsGroundedCheck();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            if (collision.gameObject.transform.root == transform.root)
            {
                return;
            }
            else
            {
                Vector3 relativeVelocity = collision.relativeVelocity;

                // �浹�� ��� �ӵ��� ������� �浹 ���� ��� (ũ�⸸ ���)
                float collisionForce = relativeVelocity.magnitude;

                Debug.Log("Collision Force: " + collisionForce);

                if (gameObject.name == "actor_rightHand_collider")
                {
                    actor.bodyType.OnRightbodyCollider(collision);
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.transform.root == transform.root)
            {
                return;
            }
            else
            {
                //actor.bodyType.ExitBodyCollider();
            }
        }

    }
}
