using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Character
{
    public class BodyColliderCheck : MonoBehaviourPun
    {
        [SerializeField] private Actor actor;

        private void Start()
        {
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

                    // 데미지 계산
                    float Headdamage = impactForce * 3f;
                    float BodyDamage = impactForce;

                    // 데미지 받을 객체 가져오기
                    GameObject target = collision.gameObject.transform.root.gameObject;

                    if (collision.gameObject.name == "actor_head_collider")
                    {
                        actor.AttackDamage(target, Headdamage);
                        actor.RightAttack = false;
                    }
                    else
                    {
                        actor.AttackDamage(target, BodyDamage);
                        actor.RightAttack = false;
                    }
                }
            }

            if (gameObject.name == "actor_rightHand_collider")
            {
                actor.movementHandeler.RightHandObject = collision.gameObject;

                if (collision.gameObject.layer == LayerMask.NameToLayer("Actor") && actor.LeftAttack &&
                    collision.gameObject.transform.root.GetComponent<Actor>().actorState != Actor.ActorState.Unconscious &&
                    collision.gameObject.transform.root.GetComponent<Actor>().actorState != Actor.ActorState.Dead)
                {
                    float impactForce = collision.relativeVelocity.magnitude;

                    // 데미지 계산
                    float Headdamage = impactForce * 3f;
                    float BodyDamage = impactForce;

                    // 데미지 받을 객체 가져오기
                    GameObject target = collision.gameObject.transform.root.gameObject;

                    if (collision.gameObject.name == "actor_head_collider")
                    {
                        actor.AttackDamage(target, Headdamage);
                        actor.RightAttack = false;
                    }
                    else
                    {
                        actor.AttackDamage(target, BodyDamage);
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
