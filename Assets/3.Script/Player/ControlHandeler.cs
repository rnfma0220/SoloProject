//using System.Collections.Generic;
//using UnityEngine;

//namespace Character
//{
//	public class ControlHandeler
//	{
//		public Transform localTransform;

//		public Actor actor;

//		public Vector3 direction;

//		public Vector3 rawDirection;

//		public bool onGround;

//		public float offGroundDelay = 0.2f;

//		public float groundCheckDelay;

//		public bool onMovingPlatform;

//		public bool leftCanClimb;

//		public bool rightCanClimb;

//		public bool inWater;

//		public Vector3 velocity;

//		public float smoothTime = 0.2f;

//		public Vector3 lookDirection;

//		public Vector3 moveDirection;

//		public Vector3 lockedDirection;

//		public bool leftArmOverride;

//		public bool rightArmOverride;

//		public bool leftLegOverride;

//		public bool rightLegOverride;

//		public float jumpDelay;

//		public bool leftGrab;

//		public bool rightGrab;

//		public bool leftPunch;

//		public bool rightPunch;

//		public bool lift;

//		public bool liftSelf;

//		public bool jump;

//		public bool grabJump;

//		public bool duck;

//		public bool leftKick;

//		public bool rightKick;

//		public bool kickDuck;

//		public float grabDelay;

//		public float idleTimer;

//		public bool idle;

//		public float liftTimer;

//		public bool run;

//		public float runTimer;

//		public float jumpTimer;

//		public float fallTimer;

//		public Vector3 slideDirection = Vector3.zero;

//		public bool canJump = true;

//		public bool headbutt;

//		public float headbuttTimer;

//		public bool ForceUpdate;

//		private enum Arm
//		{
//			Left = 0,
//			Right = 1
//		}

//		private float duckTimer;

//		private float headbuttDelay;

//		private Transform[] _cachedHeadSubObjects;

//		private Transform[] _cachedVoiceBoxHeadSubObjects;

//		private float legActionDelay;

//		private float legActionTimer;

//		private Collider leftKickTarget;

//		private Collider rightKickTarget;

//		private float leftKickTimer;

//		private float rightKickTimer;

//		private float armActionDelay;

//		private float armActionTimer;

//		private float leftArmActionTimer;

//		private float rightArmActionTimer;

//		private Collider punchGrabTarget;

//		private Collider leftPunchGrabTarget;

//		private Collider rightPunchGrabTarget;

//		private bool grab;

//		private bool punch;

//		private ConfigurableJoint grabJoint;

//		private Rigidbody grabRigidbody;

//		private Transform grabTransform;

//		private MovementHandeler.Side side;

//		private float punchTimer;

//		private float leftPunchTimer;

//		private float rightPunchTimer;

//		protected float GetAnalogInput(string key)
//		{
//			if (actor.inputHandler != null)
//			{
//				return actor.inputHandler.GetAnalogueInput(key);
//			}
//			return 0f;
//		}

//		protected bool GetDigitalInput(string key)
//		{
//			if (actor.inputHandler != null)
//			{
//				return actor.inputHandler.GetDigitalInput(key);
//			}
//			return false;
//		}

//		protected bool GetDigitalInputJustDown(string key)
//		{
//			if (actor.inputHandler != null)
//			{
//				return actor.inputHandler.GetDigitalInputJustDown(key);
//			}
//			return false;
//		}

//		protected bool GetDigitalInputJustUp(string key)
//		{
//			if (actor.inputHandler != null)
//			{
//				return actor.inputHandler.GetDigitalInputJustUp(key);
//			}
//			return false;
//		}

//		public void GetInput()
//		{
//			if (actor.IgnoreNetworkSetup && !NetworkClient.active && !NetworkServer.active)
//			{
//				DirectionCheck();
//				LiftCheck();
//			}
//			else if (actor.actorState != 0 && actor.actorState != Actor.ActorState.Unconscious)
//			{
//				if (NetworkServer.active || actor._overrideNetworkAuthority || ForceUpdate)
//				{
//					DirectionCheck();
//					GroundCheck();
//					InputSpamCheck();
//					PunchGrabCheck();
//					KickStompCheck();
//					DuckCheck();
//					LiftCheck();
//					FallCheck();
//					IdleCheck();
//					actor.movementHandeler.Emote();
//					if (onGround)
//					{
//						RunCheck();
//						JumpRunCheck();
//					}
//					else if (leftCanClimb || rightCanClimb)
//					{
//						ClimbCheck();
//					}
//					else if (inWater)
//					{
//						actor.movementHandeler.Swim();
//					}
//					else
//					{
//						JumpRunCheck();
//					}
//				}
//			}
//			else if (NetworkServer.active || actor._overrideNetworkAuthority || ForceUpdate)
//			{
//				ResetVariables();
//				ReviveCheck();
//			}
//		}

//		// Method implementations from ControlHandeler_Human

//		public void DirectionCheck()
//		{
//			rawDirection = Vector3.zero;
//			if (GetAnalogInput("HoriMove") != 0f || GetAnalogInput("VertMove") != 0f)
//			{
//				Vector3 vector = GetAnalogInput("HoriMove") * Vector3.right;
//				Vector3 vector2 = GetAnalogInput("VertMove") * Vector3.forward;
//				Vector3 vector3 = Vector3.ClampMagnitude(vector + vector2, 1f);
//				rawDirection = new Vector3(vector3.x, 0f, vector3.z);
//				if (rawDirection != Vector3.zero)
//				{
//					direction = Vector3.Normalize(rawDirection);
//				}
//			}
//			if (actor.targetingHandeler.tUpperIntrest != null)
//			{
//				lookDirection = actor.targetingHandeler.tUpperIntrest.position - actor.bodyHandeler.Head.PartCollider.bounds.center;
//			}
//			else if (!idle)
//			{
//				lookDirection = direction + new Vector3(0f, 0.2f, 0f);
//			}
//		}

//		public void GroundCheck()
//		{
//			int num = 0;
//			if (actor.bodyHandeler.LeftLeg.PartCollisionHandeler.partOnGround)
//			{
//				num++;
//			}
//			if (actor.bodyHandeler.RightLeg.PartCollisionHandeler.partOnGround)
//			{
//				num++;
//			}
//			if (actor.bodyHandeler.Ball.PartCollisionHandeler.partOnGround)
//			{
//				num++;
//			}
//			if (num > 0)
//			{
//				if (groundCheckDelay <= 0f)
//				{
//					onGround = true;
//				}
//				else if (groundCheckDelay > 0f)
//				{
//					onGround = false;
//					groundCheckDelay -= Time.deltaTime;
//				}
//			}
//			else
//			{
//				onGround = false;
//			}
//		}

//		public void FallCheck()
//		{
//			if (!onGround)
//			{
//				if (fallTimer < 100f)
//				{
//					fallTimer += Time.deltaTime;
//				}
//				if (leftCanClimb || rightCanClimb)
//				{
//					fallTimer = 0f;
//				}
//				if (actor.actorState != Actor.ActorState.Fall)
//				{
//					if (fallTimer > 0.1f && fallTimer < 0.6f)
//					{
//						actor.actorState = Actor.ActorState.Jump;
//					}
//					if (fallTimer >= 0.6f)
//					{
//						actor.actorState = Actor.ActorState.Fall;
//					}
//				}
//			}
//			else
//			{
//				fallTimer = 0f;
//			}
//		}

//		public void DuckCheck()
//		{
//			if (GetDigitalInput("Duck"))
//			{
//				duckTimer += Time.deltaTime;
//				if (duckTimer >= 0.2f)
//				{
//					duck = true;
//				}
//			}
//			if (GetDigitalInputJustUp("Duck"))
//			{
//				duck = false;
//				if (headbuttDelay <= 0f && duckTimer < 0.5f)
//				{
//					headbutt = true;
//					headbuttDelay = 1f;
//				}
//				duckTimer = 0f;
//			}
//			if (headbuttDelay >= 0f)
//			{
//				headbuttDelay -= Time.deltaTime;
//			}
//			if (headbutt)
//			{
//				headbuttTimer += Time.deltaTime;
//				if (headbuttTimer <= 0.2f)
//				{
//					actor.movementHandeler.HeadActionReadying();
//				}
//				else if (headbuttTimer > 0.2f && headbuttTimer <= 0.3f)
//				{
//					actor.movementHandeler.HeadActionHeadbutting();
//				}
//				else if (headbuttTimer > 0.5f)
//				{
//					actor.movementHandeler.HeadActionHeadbutting();
//					headbutt = false;
//					headbuttTimer = 0f;
//				}
//				// Tag handling as in ControlHandeler_Human
//			}
//		}

//		public void JumpRunCheck()
//		{
//			if (jumpDelay > 0f)
//			{
//				jumpDelay -= Time.deltaTime;
//			}
//			if (GetDigitalInputJustDown("Jump"))
//			{
//				jumpTimer = 0f;
//			}
//			if ((actor.statusHandeler.stamina <= 10f || (actor.lastActorState == Actor.ActorState.Stand && actor.controlHandeler.rawDirection == Vector3.zero)) && !idle)
//			{
//				run = false;
//			}
//			if (((!duck && !kickDuck) || (actor.bodyHandeler.leftGrabJoint != null && actor.bodyHandeler.rightGrabJoint != null)) && GetDigitalInput("Jump"))
//			{
//				if (jumpTimer > 0.4f && actor.statusHandeler.stamina >= 10f)
//				{
//					run = true;
//					runTimer = 1f;
//					slideDirection = direction;
//				}
//				jumpTimer += Time.deltaTime;
//			}
//			else if (runTimer >= 0f)
//			{
//				runTimer -= Time.deltaTime;
//			}
//			else
//			{
//				runTimer = 0f;
//				if (!idle)
//				{
//					run = false;
//				}
//			}
//			if (((!duck && !kickDuck) || actor.bodyHandeler.leftGrabJoint != null || actor.bodyHandeler.rightGrabJoint != null) && GetDigitalInputJustUp("Jump"))
//			{
//				if (jumpTimer <= 0.8f && !duck && !kickDuck)
//				{
//					jump = true;
//					if (jumpDelay <= 0f && actor.actorState != Actor.ActorState.Jump && actor.actorState != Actor.ActorState.Fall)
//					{
//						fallTimer -= 0.4f;
//						jumpDelay = 0.8f;
//						groundCheckDelay = 0.1f;
//						actor.actorState = Actor.ActorState.Jump;
//					}
//				}
//				canJump = true;
//			}
//			else
//			{
//				jump = false;
//			}
//			if (GetDigitalInput("Jump") && (GetDigitalInput("Duck") || GetDigitalInput("Kick")) && canJump && jumpTimer <= 0.4f)
//			{
//				jump = true;
//				if (jumpDelay <= 0f && actor.actorState != Actor.ActorState.Jump && actor.actorState != Actor.ActorState.Fall)
//				{
//					jumpDelay = 0.4f;
//					groundCheckDelay = 0.1f;
//					canJump = false;
//					actor.actorState = Actor.ActorState.Jump;
//				}
//			}
//		}

//		public void KickStompCheck()
//		{
//			if (legActionDelay > 0f)
//			{
//				legActionDelay -= Time.deltaTime;
//			}
//			if (GetDigitalInput("Kick"))
//			{
//				legActionTimer += Time.deltaTime;
//				if (actor.actorState == Actor.ActorState.Jump || actor.actorState == Actor.ActorState.Fall || actor.actorState == Actor.ActorState.Climb)
//				{
//					if (legActionTimer >= 0.2f)
//					{
//						kickDuck = true;
//					}
//				}
//				else if (legActionTimer >= 0.5f)
//				{
//					kickDuck = true;
//				}
//			}
//			if (GetDigitalInputJustUp("Kick"))
//			{
//				if (legActionTimer < 0.5f && actor.targetingHandeler.cLower.bounds.center.y > actor.bodyHandeler.Hips.PartTransform.position.y - 0.5f && legActionDelay <= 0f)
//				{
//					if (actor.actorState != Actor.ActorState.Jump && actor.actorState != Actor.ActorState.Fall)
//					{
//						float num = Random.Range(0f, 1f);
//						float num2 = Random.Range(0f, 1f);
//						if (actor.targetingHandeler.cLowerIntrest != null)
//						{
//							num = Vector3.Distance(actor.targetingHandeler.cLowerIntrest.bounds.center, actor.bodyHandeler.RightThigh.PartTransform.position);
//							num2 = Vector3.Distance(actor.targetingHandeler.cLowerIntrest.bounds.center, actor.bodyHandeler.LeftThigh.PartTransform.position);
//						}
//						if (num < num2)
//						{
//							leftKick = true;
//							legActionDelay = 0.4f;
//						}
//						else
//						{
//							rightKick = true;
//							legActionDelay = 0.4f;
//						}
//					}
//					else
//					{
//						leftKick = true;
//						rightKick = true;
//						legActionDelay = 0.4f;
//					}
//				}
//				kickDuck = false;
//				legActionTimer = 0f;
//			}
//			// Code for handling kicking
//		}

//		public void LiftCheck()
//		{
//			lift = GetDigitalInput("Lift");
//			if (GetDigitalInputJustDown("Lift") && onGround && (leftGrab || rightGrab))
//			{
//				float num = Random.Range(0f, 4f);
//				if (num <= 1f)
//				{
//					actor.effectsHandeler.Vert();
//					VoiceTag.SetVoiceTag(actor.effectsHandeler.gameObject, VoiceTag.VoiceType.VoiceVert);
//					actor.effectsHandeler.Invoke("ResetVoiceBoxTag", 0.1f);
//				}
//			}
//			if (!leftPunch && !leftGrab && !rightPunch && !rightGrab && lift)
//			{
//				actor.controlHandeler.leftArmOverride = true;
//				actor.controlHandeler.rightArmOverride = true;
//				actor.movementHandeler.ArmActionCheering();
//			}
//		}

//		public void PunchGrabCheck()
//		{
//			if (armActionDelay > 0f)
//			{
//				armActionDelay -= Time.deltaTime;
//			}
//			if (grabDelay > 0f)
//			{
//				grabDelay -= Time.deltaTime;
//			}
//			PunchGrab(Arm.Left);
//			PunchGrab(Arm.Right);
//		}

//		private void PunchGrab(Arm arm)
//		{
//			switch (arm)
//			{
//				case Arm.Left:
//					punchTimer = leftPunchTimer;
//					armActionTimer = leftArmActionTimer;
//					punch = leftPunch;
//					grab = leftGrab;
//					grabJoint = actor.bodyHandeler.leftGrabJoint;
//					grabRigidbody = actor.bodyHandeler.leftGrabRigidbody;
//					grabTransform = actor.bodyHandeler.leftGrabTransform;
//					side = MovementHandeler.Side.Left;
//					break;
//				case Arm.Right:
//					punchTimer = rightPunchTimer;
//					armActionTimer = rightArmActionTimer;
//					punch = rightPunch;
//					grab = rightGrab;
//					grabJoint = actor.bodyHandeler.rightGrabJoint;
//					grabRigidbody = actor.bodyHandeler.rightGrabRigidbody;
//					grabTransform = actor.bodyHandeler.rightGrabTransform;
//					side = MovementHandeler.Side.Right;
//					break;
//			}
//			// Punching and grabbing logic
//		}

//		private bool IsThisArmHeldDown(Arm arm)
//		{
//			if (arm == Arm.Left)
//			{
//				return GetDigitalInput("LeftGrab");
//			}
//			return GetDigitalInput("RightGrab");
//		}

//		private bool IsThisArmJustUp(Arm arm)
//		{
//			if (arm == Arm.Left)
//			{
//				return GetDigitalInputJustUp("LeftGrab");
//			}
//			return GetDigitalInputJustUp("RightGrab");
//		}

//		private bool IsThisArmJustDown(Arm arm)
//		{
//			if (arm == Arm.Left)
//			{
//				return GetDigitalInputJustDown("LeftGrab");
//			}
//			return GetDigitalInputJustDown("RightGrab");
//		}

//		public void ResetVariables()
//		{
//			leftArmOverride = false;
//			rightArmOverride = false;
//			leftLegOverride = false;
//			rightLegOverride = false;
//			jumpDelay = 1f;
//			leftGrab = false;
//			rightGrab = false;
//			leftPunch = false;
//			rightPunch = false;
//			lift = false;
//			liftSelf = false;
//			grabJump = false;
//			duck = false;
//			kickDuck = false;
//			// Reset grabbing and punching variables
//		}

//		public void ReviveCheck()
//		{
//			if (!(actor == null) && actor.RewiredPlayer != null && (GetDigitalInputJustDown("Jump") || GetDigitalInputJustDown("Duck") || GetDigitalInputJustDown("Kick") || GetDigitalInputJustDown("Lift") || GetDigitalInputJustDown("LeftGrab") || GetDigitalInputJustDown("RightGrab")))
//			{
//				actor.statusHandeler.healthDamage -= 5f;
//				actor.statusHandeler.unconsciousTime -= 0.05f;
//			}
//		}

//		public void RunCheck()
//		{
//			if (actor.applyedForce > 0.5f)
//			{
//				if (rawDirection.x != 0f || rawDirection.z != 0f)
//				{
//					actor.actorState = Actor.ActorState.Run;
//				}
//				else
//				{
//					actor.actorState = Actor.ActorState.Stand;
//				}
//			}
//			else
//			{
//				actor.actorState = Actor.ActorState.Stand;
//			}
//		}

//		public void ClimbCheck()
//		{
//			if (!actor.bodyHandeler.leftGrabJoint && leftCanClimb)
//			{
//				leftCanClimb = false;
//			}
//			if (!actor.bodyHandeler.rightGrabJoint && rightCanClimb)
//			{
//				rightCanClimb = false;
//			}
//			if ((!actor.bodyHandeler.leftGrabJoint || !leftCanClimb) && (!actor.bodyHandeler.rightGrabJoint || !rightCanClimb))
//			{
//				return;
//			}
//			liftSelf = GetDigitalInput("Jump");
//			if (GetDigitalInputJustDown("DoubleJump"))
//			{
//				grabDelay = 0.1f;
//				if ((bool)actor.bodyHandeler.leftGrabJoint)
//				{
//					Object.Destroy(actor.bodyHandeler.leftGrabJoint);
//					actor.effectsHandeler.AudioEvent(EffectsHandeler.SoundType.GrabExit, actor.bodyHandeler.LeftHand, Random.Range(0.9f, 1.1f), 0.2f);
//				}
//				if ((bool)actor.bodyHandeler.rightGrabJoint)
//				{
//					Object.Destroy(actor.bodyHandeler.rightGrabJoint);
//					actor.effectsHandeler.AudioEvent(EffectsHandeler.SoundType.GrabExit, actor.bodyHandeler.RightHand, Random.Range(0.9f, 1.1f), 0.2f);
//				}
//				grabJump = true;
//				actor.actorState = Actor.ActorState.Jump;
//			}
//			else
//			{
//				actor.actorState = Actor.ActorState.Climb;
//			}
//		}

//		public void IdleCheck()
//		{
//			if (actor.inputHandler.GetAnyInputDown() || actor.actorState == Actor.ActorState.Fall || actor.statusHandeler.health <= actor.statusHandeler.lastHealth)
//			{
//				if (idle)
//				{
//					idleTimer = 1f + Random.Range(0f, 3f);
//				}
//				idle = false;
//				return;
//			}
//			if (idleTimer < 30f)
//			{
//				idleTimer = Mathf.Clamp(idleTimer + Time.deltaTime, -60f, 30f);
//			}
//			if (idleTimer >= 10f && idleTimer < 20f)
//			{
//				idle = true;
//				if (Random.Range(1, 400) == 1)
//				{
//					lookDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.2f, 1f), Random.Range(-1f, 1f));
//				}
//			}
//			if (idleTimer >= 20f)
//			{
//				run = true;
//				idle = true;
//				if (Random.Range(1, 400) == 1)
//				{
//					lookDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.2f, 1f), Random.Range(-1f, 1f));
//				}
//			}
//		}

//		public void InputSpamCheck()
//		{
//			// Implement input spam check logic if needed
//		}
//	}
//}
