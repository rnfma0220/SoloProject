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
            if (collision.gameObject.transform.root == transform.root)
            {
                return;
            }
            else
            {
                if (collision.gameObject.CompareTag("Ground")) // �ٴ� üũ�뵵
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
                }

                if (gameObject.name == "actor_leftHand_collider") // ������Ʈ�� ������� �κ�
                {
                    actor.movementHandeler.LeftHandObject = collision.gameObject;

                    if (collision.gameObject.layer == LayerMask.NameToLayer("Actor") && actor.LeftAttack)
                    {
                        float impactForce = collision.relativeVelocity.magnitude;

                        float damage = impactForce * 10f;

                        if (collision.gameObject.name == "actor_head_collider")
                        {
                            Debug.Log("�浹�� ���� ��: " + damage);
                        }
                        else
                        {
                            Debug.Log("����������");
                            Debug.Log("�浹�� ���� ��: " + impactForce);
                        }
                    }
                }

                if (gameObject.name == "actor_rightHand_collider")
                {
                    actor.movementHandeler.RightHandObject = collision.gameObject;

                    if (collision.gameObject.layer == LayerMask.NameToLayer("Actor") && actor.RightAttack)
                    {
                        Debug.Log("������ ��ġ");
                    }
                }
                
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (gameObject.name == "actor_leftHand_collider")
            {
                actor.movementHandeler.LeftHandObject = null;
            }

            if (gameObject.name == "actor_rightHand_collider")
            {
                actor.movementHandeler.RightHandObject = null;
            }
        }

    }
}
