using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class BodyType : MonoBehaviour
{
    public Material headMaterial;
    public Material bodyMaterial;
    public string HexColor;
    public BodySet Head;
	public BodySet Chest;
	public BodySet Waist;
	public BodySet Stomach;
	public BodySet Hips;
	public BodySet Crotch;
	public BodySet LeftArm;
	public BodySet LeftForarm;
	public BodySet LeftHand;
	public BodySet LeftThigh;
	public BodySet LeftLeg;
	public BodySet LeftFoot;
	public BodySet RightArm;
	public BodySet RightForarm;
	public BodySet RightHand;
	public BodySet RightThigh;
	public BodySet RightLeg;
	public BodySet RightFoot;
	public BodySet Ball;
	public BodySet Spring;
	public BodySet MeshHead;
	public BodySet MeshBody;
	public BodySet Agent;
	public BodySet CameraTarget;
	public BodySet LeftGrip;
	public BodySet RightGrip;
	public BodySet Root;

    private void OnEnable()
    {
        SetUp();
    }

    public void SetUp()
    {
        SetupParts();
        SetupRenderers();
        SetupRigidbodys();
        SetupColliders();
        SetupColliderCheck();
    }

    private void SetupParts()
    {
        Transform parent = base.transform.Find("colliders");
        Root = new BodySet(base.transform, parent);
        Head = new BodySet("actor_head_collider", parent);
        Chest = new BodySet("actor_chest_collider", parent);
        Waist = new BodySet("actor_waist_collider", parent);
        Stomach = new BodySet("actor_stomach_collider", parent);
        Hips = new BodySet("actor_hips_collider", parent);
        Crotch = new BodySet("actor_crotch_collider", parent);
        LeftArm = new BodySet("actor_leftArm_collider", parent);
        LeftForarm = new BodySet("actor_leftForarm_collider", parent);
        LeftHand = new BodySet("actor_leftHand_collider", parent);
        LeftThigh = new BodySet("actor_leftThigh_collider", parent);
        LeftLeg = new BodySet("actor_leftLeg_collider", parent);
        LeftFoot = new BodySet("actor_leftFoot_helper", parent);
        RightArm = new BodySet("actor_rightArm_collider", parent);
        RightForarm = new BodySet("actor_rightForarm_collider", parent);
        RightHand = new BodySet("actor_rightHand_collider", parent);
        RightThigh = new BodySet("actor_rightThigh_collider", parent);
        RightLeg = new BodySet("actor_rightLeg_collider", parent);
        RightFoot = new BodySet("actor_rightFoot_helper", parent);
        Ball = new BodySet("actor_ball_collider", parent);
        Spring = new BodySet("actor_spring_helper", parent);
        MeshHead = new BodySet("actor_head_skinnedMesh", parent);
        MeshBody = new BodySet("actor_body_skinnedMesh", parent);
        Agent = new BodySet("helper_agent", parent);
        CameraTarget = new BodySet("helper_cameraTarget", parent);
    }

    private  void SetupRigidbodys()
    {
        Ball.PartRigidbody = Ball.PartTransform.GetComponent<Rigidbody>();
        Spring.PartRigidbody = Spring.PartTransform.GetComponent<Rigidbody>();
        Hips.PartRigidbody = Hips.PartTransform.GetComponent<Rigidbody>();
        Crotch.PartRigidbody = Crotch.PartTransform.GetComponent<Rigidbody>();
        Waist.PartRigidbody = Waist.PartTransform.GetComponent<Rigidbody>();
        Stomach.PartRigidbody = Stomach.PartTransform.GetComponent<Rigidbody>();
        Chest.PartRigidbody = Chest.PartTransform.GetComponent<Rigidbody>();
        Head.PartRigidbody = Head.PartTransform.GetComponent<Rigidbody>();
        LeftArm.PartRigidbody = LeftArm.PartTransform.GetComponent<Rigidbody>();
        LeftForarm.PartRigidbody = LeftForarm.PartTransform.GetComponent<Rigidbody>();
        LeftHand.PartRigidbody = LeftHand.PartTransform.GetComponent<Rigidbody>();
        LeftThigh.PartRigidbody = LeftThigh.PartTransform.GetComponent<Rigidbody>();
        LeftLeg.PartRigidbody = LeftLeg.PartTransform.GetComponent<Rigidbody>();
        LeftFoot.PartRigidbody = LeftFoot.PartTransform.GetComponent<Rigidbody>();
        RightArm.PartRigidbody = RightArm.PartTransform.GetComponent<Rigidbody>();
        RightForarm.PartRigidbody = RightForarm.PartTransform.GetComponent<Rigidbody>();
        RightHand.PartRigidbody = RightHand.PartTransform.GetComponent<Rigidbody>();
        RightThigh.PartRigidbody = RightThigh.PartTransform.GetComponent<Rigidbody>();
        RightLeg.PartRigidbody = RightLeg.PartTransform.GetComponent<Rigidbody>();
        RightFoot.PartRigidbody = RightFoot.PartTransform.GetComponent<Rigidbody>();
        RigidbodySettings(Ball.PartRigidbody);
        RigidbodySettings(Spring.PartRigidbody);
        RigidbodySettings(Hips.PartRigidbody);
        RigidbodySettings(Crotch.PartRigidbody);
        RigidbodySettings(Waist.PartRigidbody);
        RigidbodySettings(Stomach.PartRigidbody);
        RigidbodySettings(Chest.PartRigidbody);
        RigidbodySettings(Head.PartRigidbody);
        RigidbodySettings(LeftArm.PartRigidbody);
        RigidbodySettings(LeftForarm.PartRigidbody);
        RigidbodySettings(LeftHand.PartRigidbody);
        RigidbodySettings(RightArm.PartRigidbody);
        RigidbodySettings(RightForarm.PartRigidbody);
        RigidbodySettings(RightHand.PartRigidbody);
        RigidbodySettings(LeftThigh.PartRigidbody);
        RigidbodySettings(LeftLeg.PartRigidbody);
        RigidbodySettings(LeftFoot.PartRigidbody);
        RigidbodySettings(RightThigh.PartRigidbody);
        RigidbodySettings(RightLeg.PartRigidbody);
        RigidbodySettings(RightFoot.PartRigidbody);
    }

    private void RigidbodySettings(Rigidbody rigidbody, bool useGravity = true)
    {
        rigidbody.drag = 1.5f;
        rigidbody.angularDrag = 1f;
        rigidbody.maxDepenetrationVelocity = 0.01f;
        rigidbody.maxAngularVelocity = 20f;
        rigidbody.useGravity = useGravity;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
    }

    private void SetupRenderers()
    {
        headMaterial = MeshHead.PartRenderer.material;
        bodyMaterial = MeshBody.PartRenderer.material;
        headMaterial.color = HexToColor(HexColor);
        bodyMaterial.color = HexToColor(HexColor);
        MeshHead.PartRenderer.material = headMaterial;
        MeshBody.PartRenderer.material = bodyMaterial;
    }

    Color HexToColor(string hex)
    {
        // 헥사 색상 코드를 RGB로 변환
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        // 0-255 범위의 RGB 값을 0.0-1.0 범위로 변환하여 Color 객체 생성
        return new Color32(r, g, b, 255);
    }

    private void SetupColliderCheck()
    {     
        AddColliderCheck(LeftHand.PartCollider);
        AddColliderCheck(LeftLeg.PartCollider);
        AddColliderCheck(LeftFoot.PartCollider);
        AddColliderCheck(RightHand.PartCollider);
        AddColliderCheck(RightLeg.PartCollider);
        AddColliderCheck(RightFoot.PartCollider);
    }

    private void AddColliderCheck(Collider partCollider)
    {
        if (partCollider != null)
        {
            partCollider.gameObject.AddComponent<BodyColliderCheck>();
        }
    }

    private void SetupColliders()
    {
        Physics.IgnoreCollision(Chest.PartCollider, Head.PartCollider);
        Physics.IgnoreCollision(Chest.PartCollider, LeftArm.PartCollider);
        Physics.IgnoreCollision(Chest.PartCollider, LeftForarm.PartCollider);
        Physics.IgnoreCollision(Chest.PartCollider, RightArm.PartCollider);
        Physics.IgnoreCollision(Chest.PartCollider, RightForarm.PartCollider);
        Physics.IgnoreCollision(Chest.PartCollider, Stomach.PartCollider);
        Physics.IgnoreCollision(Chest.PartCollider, Waist.PartCollider);
        Physics.IgnoreCollision(Chest.PartCollider, Crotch.PartCollider);
        Physics.IgnoreCollision(Waist.PartCollider, Stomach.PartCollider);
        Physics.IgnoreCollision(Waist.PartCollider, Crotch.PartCollider);
        Physics.IgnoreCollision(Stomach.PartCollider, Crotch.PartCollider);
        Physics.IgnoreCollision(Hips.PartCollider, Chest.PartCollider);
        Physics.IgnoreCollision(Hips.PartCollider, Waist.PartCollider);
        Physics.IgnoreCollision(Hips.PartCollider, Stomach.PartCollider);
        Physics.IgnoreCollision(Hips.PartCollider, Crotch.PartCollider);
        Physics.IgnoreCollision(Hips.PartCollider, LeftThigh.PartCollider);
        Physics.IgnoreCollision(Hips.PartCollider, LeftLeg.PartCollider);
        Physics.IgnoreCollision(Hips.PartCollider, RightThigh.PartCollider);
        Physics.IgnoreCollision(Hips.PartCollider, RightLeg.PartCollider);
        Physics.IgnoreCollision(LeftForarm.PartCollider, LeftArm.PartCollider);
        Physics.IgnoreCollision(LeftForarm.PartCollider, LeftHand.PartCollider);
        Physics.IgnoreCollision(LeftArm.PartCollider, LeftHand.PartCollider);
        Physics.IgnoreCollision(RightForarm.PartCollider, RightArm.PartCollider);
        Physics.IgnoreCollision(RightForarm.PartCollider, RightHand.PartCollider);
        Physics.IgnoreCollision(RightArm.PartCollider, RightHand.PartCollider);
        Physics.IgnoreCollision(LeftThigh.PartCollider, Stomach.PartCollider);
        Physics.IgnoreCollision(LeftThigh.PartCollider, Crotch.PartCollider);
        Physics.IgnoreCollision(LeftThigh.PartCollider, LeftLeg.PartCollider);
        Physics.IgnoreCollision(RightThigh.PartCollider, Stomach.PartCollider);
        Physics.IgnoreCollision(RightThigh.PartCollider, Crotch.PartCollider);
        Physics.IgnoreCollision(RightThigh.PartCollider, RightLeg.PartCollider);
    }
    
    public static Transform FindTransformViaName(Transform trans, string transformName)
    {
        if (trans.name == transformName)
        {
            return trans;
        }
        Transform transform = null;
        foreach (Transform tran in trans)
        {
            if (tran.name == transformName)
            {
                return tran;
            }
            transform = FindTransformViaName(tran, transformName);
            if (transform != null)
            {
                break;
            }
        }
        return transform;
    }
}
