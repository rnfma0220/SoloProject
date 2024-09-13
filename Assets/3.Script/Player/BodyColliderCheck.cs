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
                if (collision.gameObject.CompareTag("Ground")) // 바닥 체크용도
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

                if (gameObject.name == "actor_leftHand_collider") // 오브젝트를 잡기위한 부분
                {
                    actor.movementHandeler.LeftHandObject = collision.gameObject;

                    if (collision.gameObject.layer == LayerMask.NameToLayer("Actor") && actor.LeftAttack)
                    {
                        float impactForce = collision.relativeVelocity.magnitude;

                        float damage = impactForce * 10f;

                        if (collision.gameObject.name == "actor_head_collider")
                        {
                            Debug.Log("충돌로 인한 힘: " + damage);
                        }
                        else
                        {
                            Debug.Log("나머지부위");
                            Debug.Log("충돌로 인한 힘: " + impactForce);
                        }
                    }
                }

                if (gameObject.name == "actor_rightHand_collider")
                {
                    actor.movementHandeler.RightHandObject = collision.gameObject;

                    if (collision.gameObject.layer == LayerMask.NameToLayer("Actor") && actor.RightAttack)
                    {
                        Debug.Log("오른손 펀치");
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
