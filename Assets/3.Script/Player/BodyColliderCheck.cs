using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


namespace Character
{
    public class BodyColliderCheck : MonoBehaviourPun
    {
        private Actor actor;

        private void Start()
        {
            if (!photonView.IsMine) return;

            actor = GetComponentInParent<Actor>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!photonView.IsMine) return;

            if (collision.gameObject.transform.root == transform.root)
            {
                return;
            }

            if (collision.gameObject.CompareTag("Ground")) // 바닥 체크용도
            {
                if (gameObject.name == "actor_leftThigh_collider" || gameObject.name == "actor_leftLeg_collider" || gameObject.name == "actor_rightThigh_collider" || gameObject.name == "actor_rightLeg_collider")
                {
                    if (!actor.isGround)
                    {
                        actor.isGround = true;
                        if (actor.actorState != Actor.ActorState.Stand)
                        {
                            actor.actorState = actor.JumpCheck;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }

            if (gameObject.name == "actor_leftHand_collider")
            {
                actor.movementHandeler.LeftHandObject = collision.gameObject;

                if (collision.gameObject.layer == LayerMask.NameToLayer("Actor") && actor.LeftAttack &&
                    collision.gameObject.transform.root.GetComponent<Actor>().actorState != Actor.ActorState.Unconscious &&
                    collision.gameObject.transform.root.GetComponent<Actor>().actorState != Actor.ActorState.Dead)
                {
                    float impactForce = collision.relativeVelocity.magnitude;

                    float Headdamage = impactForce * 3f;

                    GameObject test = collision.gameObject.transform.root.gameObject;

                    if (collision.gameObject.name == "actor_head_collider")
                    {
                        test.GetComponent<Actor>().PlayerHP -= Headdamage;
                        actor.LeftAttack = false;
                    }
                    else
                    {
                        test.GetComponent<Actor>().PlayerHP -= impactForce;
                        actor.LeftAttack = false;
                    }
                }
            }

            if (gameObject.name == "actor_rightHand_collider")
            {
                actor.movementHandeler.RightHandObject = collision.gameObject;

                if (collision.gameObject.layer == LayerMask.NameToLayer("Actor") && actor.RightAttack &&
                    collision.gameObject.transform.root.GetComponent<Actor>().actorState != Actor.ActorState.Unconscious &&
                    collision.gameObject.transform.root.GetComponent<Actor>().actorState != Actor.ActorState.Dead)
                {
                    float impactForce = collision.relativeVelocity.magnitude;

                    float Headdamage = impactForce * 3f;

                    GameObject test = collision.gameObject.transform.root.gameObject;

                    if (collision.gameObject.name == "actor_head_collider")
                    {
                        test.GetComponent<Actor>().PlayerHP -= Headdamage;
                        actor.RightAttack = false;
                    }
                    else
                    {
                        test.GetComponent<Actor>().PlayerHP -= impactForce;
                        actor.RightAttack = false;
                    }
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!photonView.IsMine) return;

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
